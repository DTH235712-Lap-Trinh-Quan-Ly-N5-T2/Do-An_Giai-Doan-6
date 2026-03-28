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
            var header = UIHelper.CreateHeaderPanel("Dashboard Báo Cáo", "Xem số liệu thống kê tổng quan, tiến độ và ngân sách");
            pnlHeader.Controls.Add(header);

            pnlToolbar.BackColor = UIHelper.ColorBackground;
            lblProjectFilter.Font = UIHelper.FontLabel;
            UIHelper.StyleFilterCombo(cboProject);

            // Style các panel
            pnlPieChart.BackColor = UIHelper.ColorSurface;
            pnlPieChart.BorderStyle = BorderStyle.FixedSingle;

            pnlProgressChart.BackColor = UIHelper.ColorSurface;
            pnlProgressChart.BorderStyle = BorderStyle.FixedSingle;

            pnlBudgetChart.BackColor = UIHelper.ColorSurface;
            pnlBudgetChart.BorderStyle = BorderStyle.FixedSingle;

            // Wire event
            cboProject.SelectedIndexChanged += async (s, e) => await LoadDashboardDataAsync();

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
            pnlCards.Controls.Add(CreateCard("Tổng Công Việc", stats.TotalTasks.ToString(), UIHelper.ColorPrimary));
            pnlCards.Controls.Add(CreateCard("Đã Hoàn Thành", stats.CompletedTasks.ToString(), UIHelper.ColorSuccess));
            pnlCards.Controls.Add(CreateCard("Sự Cố (Quá Hạn)", stats.OverdueTasks.ToString(), UIHelper.ColorDanger));
            pnlCards.Controls.Add(CreateCard("Tới Hạn (7 ngày)", stats.DueSoonTasks.ToString(), UIHelper.ColorWarning));
        }

        private Panel CreateCard(string title, string value, Color accentColor)
        {
            var pnl = new Panel
            {
                Width = 260, Height = 110,
                BackColor = UIHelper.ColorSurface,
                Margin = new Padding(0, 0, 20, 0),
            };

            var pnlAccent = new Panel { BackColor = accentColor, Dock = DockStyle.Left, Width = 6 };

            var lblTitle = new Label { Text = title, ForeColor = UIHelper.ColorMuted, Font = UIHelper.FontGridHeader, Location = new Point(20, 20), AutoSize = true };
            var lblValue = new Label { Text = value, ForeColor = UIHelper.ColorHeaderBg, Font = UIHelper.FontHeaderLarge, Location = new Point(16, 45), AutoSize = true };

            pnl.Controls.Add(lblTitle); pnl.Controls.Add(lblValue); pnl.Controls.Add(pnlAccent);
            
            pnl.Paint += (s, e) => {
                var rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using var pen = new Pen(UIHelper.ColorBorderLight);
                e.Graphics.DrawRectangle(pen, rect);
            };

            return pnl;
        }

        private void PnlPieChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawPanelTitle(g, pnlPieChart.Width, "Thống kê trạng thái Công việc (" + (_currentOverview?.TotalTasks ?? 0) + " tasks)");

            if (_currentOverview == null || !_currentOverview.StatusSummaries.Any(x => x.Count > 0))
            {
                DrawNoData(g, pnlPieChart.Width, pnlPieChart.Height);
                return;
            }

            var totalTasks = _currentOverview.StatusSummaries.Sum(s => s.Count);
            if (totalTasks == 0) return;

            int padding = 40;
            int legendAreaHeight = 150;
            int chartSize = Math.Min(pnlPieChart.Width - padding * 2, pnlPieChart.Height - (60 + legendAreaHeight));
            if (chartSize < 50) chartSize = 50; 

            var rect = new Rectangle((pnlPieChart.Width - chartSize) / 2, 60, chartSize, chartSize);
            float currentAngle = -90f; 
            
            foreach (var status in _currentOverview.StatusSummaries)
            {
                if (status.Count == 0) continue;
                float sweepAngle = (status.Count / (float)totalTasks) * 360f;
                using (var brush = new SolidBrush(ColorTranslator.FromHtml(status.ColorHex)))
                    g.FillPie(brush, rect, currentAngle, sweepAngle);
                currentAngle += sweepAngle;
            }

            int holeSize = chartSize / 2;
            var holeRect = new Rectangle(rect.X + holeSize / 2, rect.Y + holeSize / 2, holeSize, holeSize);
            using (var brush = new SolidBrush(UIHelper.ColorSurface)) g.FillEllipse(brush, holeRect);
            
            // Draw Legend
            int ledgY = rect.Bottom + 25;
            int ledgX = 20;
            float currentColumnMaxWidth = 0;

            foreach (var status in _currentOverview.StatusSummaries.Where(s => s.Count > 0))
            {
                string legendText = $"{status.StatusName} ({status.Count})";
                SizeF textSize = g.MeasureString(legendText, UIHelper.FontBase);
                
                float entryWidth = 15 + 10 + textSize.Width + 20;
                if (entryWidth > currentColumnMaxWidth) currentColumnMaxWidth = entryWidth;

                using (var brush = new SolidBrush(ColorTranslator.FromHtml(status.ColorHex)))
                    g.FillRectangle(brush, ledgX, ledgY, 15, 15);

                using (var textBrush = new SolidBrush(UIHelper.ColorHeaderBg))
                    g.DrawString(legendText, UIHelper.FontBase, textBrush, ledgX + 25, ledgY);

                ledgY += 25;
                if (ledgY > pnlPieChart.Height - 25)
                {
                    ledgY = rect.Bottom + 25;
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

            DrawPanelTitle(g, pnlProgressChart.Width, "Tiến độ Thực tế Dự án (%)");

            if (_currentProgress == null || !_currentProgress.Any())
            {
                DrawNoData(g, pnlProgressChart.Width, pnlProgressChart.Height);
                return;
            }

            int startY = 80;
            int margin = 30;
            int barHeight = 25;
            int maxWidth = pnlProgressChart.Width - margin * 2 - 200; // Đừa 200px cho tiêu đề dự án

            foreach(var proj in _currentProgress)
            {
                // Text Dự án
                using (var fontBrush = new SolidBrush(UIHelper.ColorHeaderBg))
                {
                    string pName = proj.ProjectName.Length > 25 ? proj.ProjectName.Substring(0, 22) + "..." : proj.ProjectName;
                    g.DrawString(pName, UIHelper.FontGridHeader, fontBrush, margin, startY);
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

                // Vẽ Lõi Progress
                int fillWidth = (int)(maxWidth * (proj.AvgProgress / 100));
                if (fillWidth > 0)
                {
                    var fillRect = new Rectangle(barX, startY, fillWidth, barHeight);
                    using (var fillBrush = new SolidBrush(progressColor))
                    {
                        g.FillRectangle(fillBrush, fillRect);
                    }
                }

                // Vẽ Nhãn % vào trên thanh
                using (var textBrush = new SolidBrush(UIHelper.ColorDark))
                {
                    string pctText = Math.Round(proj.AvgProgress, 1) + "%";
                    g.DrawString(pctText, UIHelper.FontLabel, textBrush, barX + maxWidth + 10, startY + 4);
                }
                
                // Vẽ số task bên dưới một chút
                using (var muteBrush = new SolidBrush(UIHelper.ColorMuted))
                {
                    g.DrawString($"{proj.CompletedTasks}/{proj.TotalTasks} tasks xong", UIHelper.FontSmall, muteBrush, margin, startY + 15);
                }

                startY += 60; // Row spacing
                if (startY > pnlProgressChart.Height - 50) break; // Cắt bỏ nếu tràn màn hình
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
                // Cột Ngân sách định mức (Xanh biếc)
                int hBudget = (int)(chartHeight * (b.Budget / maxVal));
                var rectBudget = new Rectangle(currentX, startY - hBudget, barWidth, hBudget);
                using (var brush = new SolidBrush(Color.FromArgb(59, 130, 246))) // Blue 500
                    g.FillRectangle(brush, rectBudget);

                // Cột Chi phí thực tế (Xanh lá, hoặc Đỏ nếu vượt)
                int hExpense = (int)(chartHeight * (b.TotalExpense / maxVal));
                var rectExpense = new Rectangle(currentX + barWidth + 2, startY - hExpense, barWidth, hExpense);
                Color expColor = b.TotalExpense > b.Budget ? UIHelper.ColorDanger : UIHelper.ColorSuccess;
                using (var brush = new SolidBrush(expColor))
                    g.FillRectangle(brush, rectExpense);

                // Thông báo % dùng
                using (var strBrush = new SolidBrush(UIHelper.ColorMuted))
                {
                    string uPct = $"{b.UsagePercentage}%";
                    g.DrawString(uPct, UIHelper.FontLabel, strBrush, currentX, startY - Math.Max(hBudget, hExpense) - 20);
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

            // Legend
            int legX = pnlBudgetChart.Width - 250;
            using (var b1 = new SolidBrush(Color.FromArgb(59, 130, 246))) g.FillRectangle(b1, legX, 20, 15, 15);
            using (var b2 = new SolidBrush(UIHelper.ColorSuccess)) g.FillRectangle(b2, legX, 45, 15, 15);
            using (var b3 = new SolidBrush(UIHelper.ColorDanger)) g.FillRectangle(b3, legX, 70, 15, 15);
            
            using (var textBrush = new SolidBrush(UIHelper.ColorHeaderBg))
            {
                g.DrawString("Ngân sách (Budget)", UIHelper.FontBase, textBrush, legX + 25, 20);
                g.DrawString("Chi phí thực tế (An toàn)", UIHelper.FontBase, textBrush, legX + 25, 45);
                g.DrawString("Chi phí thực tế (Vượt quỹ)", UIHelper.FontBase, textBrush, legX + 25, 70);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // HELPERS
        // ══════════════════════════════════════════════════════════════════
        private void DrawPanelTitle(Graphics g, int width, string title)
        {
            using (var brush = new SolidBrush(UIHelper.ColorHeaderBg))
                g.DrawString(title, UIHelper.FontHeaderLarge, brush, 20, 20);
            using (var pen = new Pen(UIHelper.ColorBorderLight))
                g.DrawLine(pen, 0, 50, width, 50);
        }

        private void DrawNoData(Graphics g, int width, int height)
        {
            using (var brush = new SolidBrush(UIHelper.ColorMuted))
            {
                string msg = "Không có dữ liệu...";
                var size = g.MeasureString(msg, UIHelper.FontBase);
                g.DrawString(msg, UIHelper.FontBase, brush, (width - size.Width) / 2, (height - size.Height) / 2);
            }
        }
    }
}
