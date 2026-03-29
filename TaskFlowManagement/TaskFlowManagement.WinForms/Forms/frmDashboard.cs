using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskFlowManagement.Core.DTOs;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmDashboard : BaseForm
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        
        private DashboardStatsDto? _currentOverview = null;
        private List<ProgressReportDto> _currentProgress = new();
        private List<BudgetReportDto> _currentBudget = new();
        private int _hoverProgressIndex = -1;

        public frmDashboard(ITaskService taskService, IProjectService projectService)
        {
            InitializeComponent();
            _taskService = taskService;
            _projectService = projectService;

            SetupUI();
        }

        private void SetupUI()
        {
            // Thiết lập Header & Toolbar
            pnlHeader.Controls.Clear();
            var header = UIHelper.CreateHeaderPanel("BÁO CÁO TỔNG QUAN", "Cập nhật lúc: " + DateTime.Now.ToString("HH:mm dd/MM/yyyy"));
            
            // Đường kẻ Accent mỏng màu xanh phía dưới
            var pnlAccent = new Panel { Dock = DockStyle.Bottom, Height = 3, BackColor = UIHelper.ColorPrimary };
            header.Controls.Add(pnlAccent);
            
            pnlHeader.Controls.Add(header);

            pnlToolbar.BackColor = UIHelper.ColorBackground;
            lblProjectFilter.Font = UIHelper.FontLabel;
            UIHelper.StyleFilterCombo(cboProject);

            // Style các panel
            pnlPieChart.BackColor = UIHelper.ColorSurface;
            pnlPieChart.BorderStyle = BorderStyle.FixedSingle;

            pnlProgressChart.BackColor = UIHelper.ColorSurface;
            pnlProgressChart.BorderStyle = BorderStyle.FixedSingle;
            pnlProgressChart.AutoScroll = true;

            pnlBudgetChart.BackColor = UIHelper.ColorSurface;
            pnlBudgetChart.BorderStyle = BorderStyle.FixedSingle;

            // Wire event
            cboProject.SelectedIndexChanged += async (s, e) => await LoadDashboardDataAsync();
            pnlProgressChart.MouseMove += PnlProgressChart_MouseMove;
            pnlProgressChart.MouseLeave += (s, e) => { _hoverProgressIndex = -1; pnlProgressChart.Invalidate(); };

            // Set DoubleBuffered
            EnableDoubleBuffer(pnlPieChart);
            EnableDoubleBuffer(pnlProgressChart);
            EnableDoubleBuffer(pnlBudgetChart);

            // Phân quyền Tab Ngân Quỹ (Developer không được xem thẻ Budget)
            if (!AppSession.IsManager && !AppSession.IsAdmin)
            {
                tabControlDashboard.TabPages.Remove(tabBudget);
            }
        }

        private void EnableDoubleBuffer(Control ctrl)
        {
            var method = typeof(Control).GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (method != null)
            {
                method.Invoke(ctrl, new object[] { ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true });
            }
        }

        private void PnlProgressChart_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_currentProgress == null || !_currentProgress.Any()) return;
            
            int unadjustedY = e.Y - pnlProgressChart.AutoScrollPosition.Y;
            int startY = 80;
            int newHoverMode = -1;
            
            for (int i = 0; i < _currentProgress.Count; i++)
            {
                if (unadjustedY >= startY && unadjustedY < startY + 60)
                {
                    newHoverMode = i;
                    break;
                }
                startY += 60;
            }

            if (_hoverProgressIndex != newHoverMode)
            {
                _hoverProgressIndex = newHoverMode;
                pnlProgressChart.Invalidate();
            }
        }

        private async void frmDashboard_Load(object sender, EventArgs e)
        {
            await LoadProjectsAsync();
            await LoadDashboardDataAsync();
        }

        public void SelectTab(int tabIndex)
        {
            if (tabIndex >= 0 && tabIndex < tabControlDashboard.TabPages.Count)
            {
                tabControlDashboard.SelectedIndex = tabIndex;
            }
            else if (tabIndex == 2 && tabControlDashboard.TabPages.Count < 3)
            {
                MessageBox.Show("Bạn không có quyền truy cập tab Ngân sách (chỉ dành cho Admin/Manager).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task LoadProjectsAsync()
        {
            var projects = await _projectService.GetProjectsForUserAsync(AppSession.UserId, AppSession.IsManager);
            
            cboProject.Items.Clear();
            cboProject.Items.Add(new { Id = 0, Name = "-- Toàn bộ hệ thống --" });
            
            foreach (var p in projects)
            {
                cboProject.Items.Add(new { Id = p.Id, Name = p.Name });
            }

            cboProject.DisplayMember = "Name";
            cboProject.ValueMember = "Id";
            cboProject.SelectedIndex = 0;
            
            if (!AppSession.IsManager && !AppSession.IsAdmin)
            {
                cboProject.Enabled = false; // Chỉ xem dữ liệu dự án mình đc chỉ định
            }
        }

        private async Task LoadDashboardDataAsync()
        {
            if (cboProject.SelectedItem == null) return;
            var selectedId = (int)((dynamic)cboProject.SelectedItem).Id;
            int? projectId = selectedId == 0 ? null : selectedId;

            try
            {
                // Gọi song song cho nhanh
                var task1 = _taskService.GetDashboardStatsAsync(projectId);
                var task2 = _taskService.GetProgressReportAsync(projectId);
                
                Task<List<BudgetReportDto>>? task3 = null;
                if (AppSession.IsManager || AppSession.IsAdmin)
                {
                    task3 = _taskService.GetBudgetReportAsync(projectId);
                }

                _currentOverview = await task1;
                _currentProgress = await task2;
                if (task3 != null) _currentBudget = await task3;
                
                RenderStatCards(_currentOverview);
                
                pnlPieChart.Invalidate();
                pnlProgressChart.Invalidate();
                if (task3 != null) pnlBudgetChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 1: OVERVIEW & PIE CHART
        // ══════════════════════════════════════════════════════════════════
        
        private void RenderStatCards(DashboardStatsDto stats)
        {
            pnlCards.Controls.Clear();
            pnlCards.Controls.Add(CreateCard("Tổng Công Việc", stats.TotalTasks.ToString(), "📁", UIHelper.ColorPrimary));
            pnlCards.Controls.Add(CreateCard("Đã Hoàn Thành", stats.CompletedTasks.ToString(), "✅", UIHelper.ColorSuccess));
            pnlCards.Controls.Add(CreateCard("Sự Cố (Quá Hạn)", stats.OverdueTasks.ToString(), "🚩", Color.FromArgb(185, 28, 28))); // Đỏ đậm theo yêu cầu
            pnlCards.Controls.Add(CreateCard("Tới Hạn (7 ngày)", stats.DueSoonTasks.ToString(), "⚠️", UIHelper.ColorWarning));
        }

        private Panel CreateCard(string title, string value, string icon, Color accentColor)
        {
            var pnl = new Panel
            {
                Width = 260, Height = 110,
                BackColor = UIHelper.ColorSurface,
                Margin = new Padding(0, 0, 20, 0),
            };

            var pnlAccent = new Panel { BackColor = accentColor, Dock = DockStyle.Left, Width = 6 };

            var lblTitle = new Label { Text = title, ForeColor = UIHelper.ColorSubtitle, Font = UIHelper.FontSmall, Location = new Point(20, 20), AutoSize = true };
            var lblValue = new Label { Text = value, ForeColor = UIHelper.ColorHeaderBg, Font = UIHelper.FontHeaderLarge, Location = new Point(16, 45), AutoSize = true };
            var lblIcon = new Label { Text = icon, ForeColor = accentColor, Font = new Font("Segoe UI", 24F), Location = new Point(190, 30), AutoSize = true };

            pnl.Controls.Add(lblTitle); pnl.Controls.Add(lblValue); pnl.Controls.Add(lblIcon); pnl.Controls.Add(pnlAccent);
            
            pnl.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using var path = GetRoundedRect(rect, 8);
                using var pen = new Pen(UIHelper.ColorBorderLight);
                e.Graphics.DrawPath(pen, path);
            };

            return pnl;
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void PnlPieChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            DrawPanelTitle(g, pnlPieChart.Width, "Thống kê trạng thái Công việc (" + (_currentOverview?.TotalTasks ?? 0) + " tasks)");

            if (_currentOverview == null || !_currentOverview.StatusSummaries.Any(x => x.Count > 0))
            {
                DrawNoData(g, pnlPieChart.Width, pnlPieChart.Height);
                return;
            }

            var totalTasks = _currentOverview.StatusSummaries.Sum(s => s.Count);
            if (totalTasks == 0) return;

            int padding = 40;
            // Draw Legend at the right if layout allows, otherwise at bottom
            int legendAreaHeight = 150;
            int chartSize = Math.Min(pnlPieChart.Width / 2 - padding, pnlPieChart.Height - 80);
            if (chartSize < 100) chartSize = Math.Min(pnlPieChart.Width - padding * 2, pnlPieChart.Height - (60 + legendAreaHeight));
            if (chartSize < 50) chartSize = 50; 

            // Canh trái một chút để nhường chỗ cho Legend bên phải
            var rect = new Rectangle(padding + 20, 80, chartSize, chartSize);
            float currentAngle = -90f; 
            
            foreach (var status in _currentOverview.StatusSummaries)
            {
                if (status.Count == 0) continue;
                float sweepAngle = (status.Count / (float)totalTasks) * 360f;
                using (var brush = new SolidBrush(ColorTranslator.FromHtml(status.ColorHex)))
                    g.FillPie(brush, rect, currentAngle, sweepAngle);
                currentAngle += sweepAngle;
            }

            // Donut Chart: Lỗ ở giữa lớn hơn để tạo phong cách hiện đại
            int holeSize = (int)(chartSize * 0.65);
            var holeRect = new Rectangle(rect.X + (chartSize - holeSize) / 2, rect.Y + (chartSize - holeSize) / 2, holeSize, holeSize);
            using (var brush = new SolidBrush(UIHelper.ColorSurface)) g.FillEllipse(brush, holeRect);
            
            // Draw Legend (Bên phải biểu đồ)
            int ledgY = 80;
            int ledgX = rect.Right + 50;
            float currentColumnMaxWidth = 0;

            foreach (var status in _currentOverview.StatusSummaries.Where(s => s.Count > 0))
            {
                string legendText = $"{status.StatusName} ({status.Count})";
                SizeF textSize = g.MeasureString(legendText, UIHelper.FontBase);
                
                float entryWidth = 15 + 10 + textSize.Width + 20;
                if (entryWidth > currentColumnMaxWidth) currentColumnMaxWidth = entryWidth;

                // Box chú thích bo góc
                var legendPathRect = new Rectangle(ledgX, ledgY, 16, 16);
                using (var brush = new SolidBrush(ColorTranslator.FromHtml(status.ColorHex)))
                using (var path = GetRoundedRect(legendPathRect, 4))
                {
                    g.FillPath(brush, path);
                }

                using (var textBrush = new SolidBrush(UIHelper.ColorDark))
                    g.DrawString(legendText, UIHelper.FontBase, textBrush, ledgX + 25, ledgY - 1);

                ledgY += 28;
                if (ledgY > pnlPieChart.Height - 40)
                {
                    ledgY = 80;
                    ledgX += (int)currentColumnMaxWidth;
                    currentColumnMaxWidth = 0;
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 2: PROGRESS BARS 
        // ══════════════════════════════════════════════════════════════════
        private void PnlProgressChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.TranslateTransform(pnlProgressChart.AutoScrollPosition.X, pnlProgressChart.AutoScrollPosition.Y);
            DrawPanelTitle(g, Math.Max(pnlProgressChart.Width, pnlProgressChart.AutoScrollMinSize.Width), "Tiến độ Thực tế Dự án (%)");

            if (_currentProgress == null || !_currentProgress.Any())
            {
                DrawNoData(g, pnlProgressChart.Width, pnlProgressChart.Height);
                return;
            }

            int startY = 80;
            int margin = 30;
            int barHeight = 25;
            // Sử dụng ClientSize.Width thay vì Width để trừ đi thanh cuộn (Scrollbar)
            // Trừ 200px tên dự án, trừ margin*2, và trừ thêm 60px dự phòng cho chữ số % (Padding & Kích thước text lớn nhất 100%)
            int maxWidth = pnlProgressChart.ClientSize.Width - margin * 2 - 200 - 60; 
            if (maxWidth < 100) maxWidth = 100;

            int rowIndex = 0;
            foreach(var proj in _currentProgress)
            {
                // Hover highlight
                if (rowIndex == _hoverProgressIndex)
                {
                    using var hoverBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
                    g.FillRectangle(hoverBrush, margin - 15, startY - 5, pnlProgressChart.ClientSize.Width - margin * 2 + 30, 60);
                }
                
                // Border line under
                using (var borderPen = new Pen(UIHelper.ColorBorderLight))
                {
                    g.DrawLine(borderPen, margin, startY + 50, pnlProgressChart.ClientSize.Width - margin, startY + 50);
                }

                // Text Dự án
                using (var fontBrush = new SolidBrush(UIHelper.ColorHeaderBg))
                {
                    string pName = proj.ProjectName.Length > 25 ? proj.ProjectName.Substring(0, 22) + "..." : proj.ProjectName;
                    g.DrawString(pName, UIHelper.FontGridHeader, fontBrush, margin + 15, startY);
                }

                // Vẽ Khung Bar nền
                int barX = margin + 200;
                var bgRect = new Rectangle(barX, startY, maxWidth, barHeight);
                using (var bgBrush = new SolidBrush(UIHelper.ColorBorderLight))
                {
                    g.FillRectangle(bgBrush, bgRect);
                }

                // Màu Progress phụ thuộc vào trạng thái
                Color progressColor = UIHelper.ColorPrimary;
                if (proj.AvgProgress >= 100) progressColor = UIHelper.ColorSuccess;
                else if (proj.Status == "OnHold" || proj.Status == "Delayed") progressColor = UIHelper.ColorWarning;

                // Vẽ Lõi Progress bằng LinearGradientBrush (Gradient fill)
                int fillWidth = (int)(maxWidth * (proj.AvgProgress / 100));
                if (fillWidth > 0)
                {
                    var fillRect = new Rectangle(barX, startY, fillWidth, barHeight);
                    Color lightColor = ControlPaint.Light(progressColor, 0.4f);
                    using (var fillBrush = new LinearGradientBrush(fillRect, progressColor, lightColor, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(fillBrush, fillRect);
                    }
                }

                // Vẽ Nhãn % vào trên thanh với Bounding Box và Padding >= 5px, luôn hiển thị ngay cả khi progress == 0
                using (var textBrush = new SolidBrush(UIHelper.ColorDark))
                {
                    string pctText = Math.Round(proj.AvgProgress, 1) + "%";
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    
                    // Sử dụng UIHelper.FontBase theo yêu cầu để nhất quán
                    SizeF textSize = g.MeasureString(pctText, UIHelper.FontBase); 
                    
                    // Tính tọa độ động dựa trên MeasureString và đảm bảo text thụt cách lõi/ba 5px (Padding = 5px)
                    float textX = barX + fillWidth + 5; 
                    float textY = startY + (barHeight - textSize.Height) / 2;
                    g.DrawString(pctText, UIHelper.FontBase, textBrush, textX, textY);
                }
                
                // Vẽ số task bên dưới một chút
                using (var muteBrush = new SolidBrush(UIHelper.ColorMuted))
                {
                    g.DrawString($"{proj.CompletedTasks}/{proj.TotalTasks} tasks xong", UIHelper.FontSmall, muteBrush, margin + 15, startY + 20);
                }

                startY += 60; // Row spacing
                rowIndex++;
            }
            
            int totalHeight = startY + 20;
            if (pnlProgressChart.AutoScrollMinSize.Height != totalHeight)
            {
                pnlProgressChart.AutoScrollMinSize = new Size(0, totalHeight);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 3: BUDGET BAR CHART (So sánh Actual vs Budget)
        // ══════════════════════════════════════════════════════════════════
        private void PnlBudgetChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawPanelTitle(g, pnlBudgetChart.Width, "Thống kê Ngân sách Thực tế (Budget vs Expenses)");

            if (_currentBudget == null || !_currentBudget.Any())
            {
                DrawNoData(g, pnlBudgetChart.Width, pnlBudgetChart.Height);
                return;
            }

            int paddingSide = 80;
            int startY = pnlBudgetChart.Height - 50;
            int chartHeight = pnlBudgetChart.Height - 150;
            
            decimal maxVal = _currentBudget.Max(b => Math.Max(b.Budget, b.TotalExpense));
            if (maxVal == 0) maxVal = 1000; // Tránh chia 0
            
            // Vẽ Grid mờ (Lines)
            using (var pen = new Pen(UIHelper.ColorBorderLight))
            using (var brush = new SolidBrush(UIHelper.ColorMuted))
            {
                g.DrawLine(pen, paddingSide, startY, pnlBudgetChart.Width - 40, startY); // Trục X
                g.DrawLine(pen, paddingSide, startY, paddingSide, startY - chartHeight); // Trục Y
                
                int steps = 5;
                for(int i = 0; i <= steps; i++)
                {
                    int y = startY - (chartHeight * i / steps);
                    decimal val = maxVal * i / steps;
                    string lbl = val >= 1000000 ? (val/1000000).ToString("0.#M") : (val/1000).ToString("0.#k");
                    
                    var sf = new StringFormat{ Alignment = StringAlignment.Far };
                    g.DrawString(lbl, UIHelper.FontSmall, brush, new RectangleF(0, y - 8, paddingSide - 10, 20), sf);
                    if (i > 0) g.DrawLine(pen, paddingSide, y, pnlBudgetChart.Width - 40, y); 
                }
            }

            int pairWidth = Math.Min(100, (pnlBudgetChart.Width - paddingSide - 40) / _currentBudget.Count);
            int barWidth = pairWidth / 2 - 10;
            int currentX = paddingSide + 20;

            foreach (var b in _currentBudget)
            {
                // Cột Ngân sách định mức (Xanh biếc với Gradient)
                int hBudget = (int)(chartHeight * (b.Budget / maxVal));
                var rectBudget = new Rectangle(currentX, startY - hBudget, barWidth, hBudget);
                Color blueColor = Color.FromArgb(59, 130, 246);
                if (hBudget > 0)
                {
                    using (var brush = new LinearGradientBrush(rectBudget, ControlPaint.Light(blueColor, 0.3f), blueColor, LinearGradientMode.Vertical))
                        g.FillRectangle(brush, rectBudget);
                }

                // Cột Chi phí thực tế (Xanh lá, Đỏ nếu vượt - với Gradient)
                int hExpense = (int)(chartHeight * (b.TotalExpense / maxVal));
                var rectExpense = new Rectangle(currentX + barWidth + 2, startY - hExpense, barWidth, hExpense);
                Color expColor = b.TotalExpense > b.Budget ? Color.FromArgb(185, 28, 28) : UIHelper.ColorSuccess;
                if (hExpense > 0)
                {
                    using (var brush = new LinearGradientBrush(rectExpense, ControlPaint.Light(expColor, 0.3f), expColor, LinearGradientMode.Vertical))
                        g.FillRectangle(brush, rectExpense);
                }

                // Thông báo % dùng
                using (var strBrush = new SolidBrush(UIHelper.ColorMuted))
                {
                    string uPct = $"{b.UsagePercentage}%";
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    SizeF textSize = g.MeasureString(uPct, UIHelper.FontLabel);
                    
                    float xPos = currentX + (pairWidth * 2 / 2 - textSize.Width) / 2;
                    float yPos = startY - Math.Max(hBudget, hExpense) - textSize.Height - 5;
                    g.DrawString(uPct, UIHelper.FontLabel, strBrush, xPos, yPos);
                }

                // Trục X (Tên Dự án)
                using (var strBrush = new SolidBrush(UIHelper.ColorMuted))
                {
                    string label = b.ProjectName;
                    if (label.Length > 12) label = label.Substring(0, 10) + "...";
                    var sf = new StringFormat { Alignment = StringAlignment.Center };
                    var labelRect = new RectangleF(currentX - 5, startY + 10, pairWidth, 30);
                    g.DrawString(label, UIHelper.FontSmall, strBrush, labelRect, sf);
                }

                currentX += pairWidth;
            }

            // Legend cho Budget (cũng vuông góc bo tròn)
            int legX = pnlBudgetChart.Width - 250;
            
            using (var b1 = new SolidBrush(Color.FromArgb(59, 130, 246))) 
            using (var path1 = GetRoundedRect(new Rectangle(legX, 20, 16, 16), 4)) g.FillPath(b1, path1);
            
            using (var b2 = new SolidBrush(UIHelper.ColorSuccess)) 
            using (var path2 = GetRoundedRect(new Rectangle(legX, 45, 16, 16), 4)) g.FillPath(b2, path2);
            
            using (var b3 = new SolidBrush(Color.FromArgb(185, 28, 28))) 
            using (var path3 = GetRoundedRect(new Rectangle(legX, 70, 16, 16), 4)) g.FillPath(b3, path3);
            
            using (var textBrush = new SolidBrush(UIHelper.ColorHeaderBg))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.DrawString("Ngân sách (Budget)", UIHelper.FontBase, textBrush, legX + 25, 19);
                g.DrawString("Chi phí thực tế (An toàn)", UIHelper.FontBase, textBrush, legX + 25, 44);
                g.DrawString("Chi phí thực tế (Vượt quỹ)", UIHelper.FontBase, textBrush, legX + 25, 69);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // HELPERS
        // ══════════════════════════════════════════════════════════════════
        private void DrawPanelTitle(Graphics g, int width, string title)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (var brush = new SolidBrush(UIHelper.ColorHeaderBg))
                g.DrawString(title, UIHelper.FontHeaderLarge, brush, 20, 20);
            using (var pen = new Pen(UIHelper.ColorBorderLight))
                g.DrawLine(pen, 0, 50, width, 50);
        }

        private void DrawNoData(Graphics g, int width, int height)
        {
            using (var brush = new SolidBrush(UIHelper.ColorMuted))
            {
                string msg = "Chưa có dữ liệu thống kê";
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                var size = g.MeasureString(msg, UIHelper.FontBase);
                g.DrawString(msg, UIHelper.FontBase, brush, (width - size.Width) / 2, (height - size.Height) / 2);
            }
        }
    }
}
