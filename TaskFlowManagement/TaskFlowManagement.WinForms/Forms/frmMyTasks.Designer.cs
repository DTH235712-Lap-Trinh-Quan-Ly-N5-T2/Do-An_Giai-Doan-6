// ============================================================
//  frmMyTasks.Designer.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
//
//  THAY ĐỔI SO VỚI PHIÊN BẢN CŨ:
//  ─────────────────────────────────────────────────────────
//  [Cấu trúc mới — nhất quán với frmUsers / frmTaskList]
//   • panelHeader  — dark banner (#0F172A) + accent line xanh [THÊM MỚI]
//   • panelFilter  — toolbar với lblUser + btnRefresh [SỬA TỪ panelTop]
//   • panelStatus  — status bar bottom dark [SỬA TỪ panelBottom]
//   • TabControl   — styling font, ItemSize chuẩn
//
//  [UIHelper áp dụng]
//   • UIHelper.StyleDataGridView() cho cả 3 DataGridView [THÊM MỚI]
//   • UIHelper.ApplyAlternateRowColors() cho cả 3 grid [THÊM MỚI]
//   • UIHelper.StyleToolButton() cho btnRefresh [SỬA]
//   • UIHelper.ColorHeaderBg / ColorBackground / ColorMuted / ColorSubtitle
//   • UIHelper.FontHeaderLarge / FontBody / FontSmall
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmMyTasks   // BaseForm declared in frmMyTasks.cs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Instantiation ─────────────────────────────────────────────────
            panelHeader = new Panel();
            panelAccentLine = new Panel();
            lblHeader = new Label();

            panelFilter = new Panel();
            lblUser = new Label();
            btnRefresh = new Button();

            tabControl = new TabControl();
            tabMyTasks = new TabPage();
            tabReview = new TabPage();
            tabTesting = new TabPage();

            dgvMyTasks = new DataGridView();
            dgvReview = new DataGridView();
            dgvTesting = new DataGridView();

            panelStatus = new Panel();
            lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelStatus.SuspendLayout();
            tabControl.SuspendLayout();
            tabMyTasks.SuspendLayout();
            tabReview.SuspendLayout();
            tabTesting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyTasks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTesting).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelHeader — Dark banner (THÊM MỚI: trước không có)
            // ════════════════════════════════════════════════════
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Controls.Add(panelAccentLine);

            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(18, 0, 0, 4);
            lblHeader.Text = "📋  Công việc của tôi";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // panelFilter — Toolbar: thông tin user + nút Làm mới
            // (SỬA: trước là panelTop không có styling)
            // ════════════════════════════════════════════════════
            panelFilter.BackColor = UIHelper.ColorBackground;
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";
            panelFilter.Controls.Add(lblUser);
            panelFilter.Controls.Add(btnRefresh);

            lblUser.AutoSize = false;
            lblUser.Font = UIHelper.FontBase;
            lblUser.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblUser.Location = new Point(14, 13);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(660, 22);
            lblUser.Text = "Đang tải...";

            UIHelper.StyleToolButton(btnRefresh, "🔄  Làm mới", UIHelper.ButtonVariant.Secondary, 686, 9, 110, 30);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Click += btnRefresh_Click;

            // ════════════════════════════════════════════════════
            // panelStatus — Status bar (SỬA: trước panelBottom không có dark bg)
            // ════════════════════════════════════════════════════
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Height = 28;
            panelStatus.Name = "panelStatus";
            panelStatus.Controls.Add(lblStatus);

            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(12, 0, 0, 0);
            lblStatus.Text = "Sẵn sàng";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // DataGridViews — áp dụng UIHelper (THÊM MỚI)
            // ════════════════════════════════════════════════════
            ConfigureTaskGrid(dgvMyTasks);
            ConfigureTaskGrid(dgvReview);
            ConfigureTaskGrid(dgvTesting);

            dgvMyTasks.CellDoubleClick += dgv_CellDoubleClick;
            dgvReview.CellDoubleClick += dgv_CellDoubleClick;
            dgvTesting.CellDoubleClick += dgv_CellDoubleClick;

            // ════════════════════════════════════════════════════
            // Tab Pages
            // ════════════════════════════════════════════════════
            tabMyTasks.BackColor = System.Drawing.Color.White;
            tabMyTasks.Text = "📋  Được giao (0)";
            tabMyTasks.Padding = new Padding(3);
            tabMyTasks.Name = "tabMyTasks";
            tabMyTasks.Controls.Add(dgvMyTasks);
            dgvMyTasks.Dock = DockStyle.Fill;

            tabReview.BackColor = System.Drawing.Color.White;
            tabReview.Text = "🔍  Review (0)";
            tabReview.Padding = new Padding(3);
            tabReview.Name = "tabReview";
            tabReview.Controls.Add(dgvReview);
            dgvReview.Dock = DockStyle.Fill;

            tabTesting.BackColor = System.Drawing.Color.White;
            tabTesting.Text = "🧪  Testing (0)";
            tabTesting.Padding = new Padding(3);
            tabTesting.Name = "tabTesting";
            tabTesting.Controls.Add(dgvTesting);
            dgvTesting.Dock = DockStyle.Fill;

            // ════════════════════════════════════════════════════
            // TabControl
            // ════════════════════════════════════════════════════
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            tabControl.ItemSize = new System.Drawing.Size(175, 32);
            tabControl.Name = "tabControl";
            tabControl.TabPages.AddRange(new TabPage[] { tabMyTasks, tabReview, tabTesting });

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.Text = "📋  Công việc của tôi";
            this.Size = new System.Drawing.Size(1000, 660);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "frmMyTasks";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = UIHelper.FontBase;

            // Thứ tự Add: Fill trước → Bottom → Top (ngược chiều Dock)
            this.Controls.Add(tabControl);    // DockStyle.Fill
            this.Controls.Add(panelStatus);   // DockStyle.Bottom
            this.Controls.Add(panelFilter);   // DockStyle.Top (thứ hai)
            this.Controls.Add(panelHeader);   // DockStyle.Top (trên cùng)

            panelHeader.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabMyTasks.ResumeLayout(false);
            tabReview.ResumeLayout(false);
            tabTesting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMyTasks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReview).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTesting).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Cấu hình DataGridView theo chuẩn chung — gọi UIHelper thay vì hard-code.
        /// Gọi cho cả 3 grid để tránh lặp code.
        /// </summary>
        private static void ConfigureTaskGrid(DataGridView dgv)
        {
            // [REFACTOR] Dùng UIHelper thay vì set từng thuộc tính thủ công
            UIHelper.StyleDataGridView(dgv);
            UIHelper.ApplyAlternateRowColors(dgv);

            dgv.RowTemplate.Height = 32;

            // ── Định nghĩa cột ────────────────────────────────────
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            };
            var colTitle = new DataGridViewTextBoxColumn
            {
                Name = "colTitle",
                HeaderText = "Tiêu đề công việc",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            };
            var colProject = new DataGridViewTextBoxColumn
            {
                Name = "colProject",
                HeaderText = "Dự án",
                Width = 150
            };
            var colPriority = new DataGridViewTextBoxColumn
            {
                Name = "colPriority",
                HeaderText = "Ưu tiên",
                Width = 85
            };
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                Width = 120
            };
            var colProgress = new DataGridViewTextBoxColumn
            {
                Name = "colProgress",
                HeaderText = "%",
                Width = 55,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colDueDate = new DataGridViewTextBoxColumn
            {
                Name = "colDueDate",
                HeaderText = "Hạn chót",
                Width = 100
            };

            dgv.Columns.AddRange(new DataGridViewColumn[]
            { colId, colTitle, colProject, colPriority, colStatus, colProgress, colDueDate });
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;
        private Panel panelFilter;
        private Label lblUser;
        private Button btnRefresh;
        private TabControl tabControl;
        private TabPage tabMyTasks;
        private TabPage tabReview;
        private TabPage tabTesting;
        private DataGridView dgvMyTasks;
        private DataGridView dgvReview;
        private DataGridView dgvTesting;
        private Panel panelStatus;
        private Label lblStatus;
    }
}