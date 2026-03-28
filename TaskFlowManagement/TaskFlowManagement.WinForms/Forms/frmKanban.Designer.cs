using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmKanban   // BaseForm declared in frmKanban.cs
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
            btnRefresh = new Button();
            lblFilterHint = new Label();

            tlpBoard = new TableLayoutPanel();

            pnlTodo = new Panel();
            lblTodo = new Label();
            flpTodo = new DoubleBufferedFlowLayoutPanel();

            pnlInProgress = new Panel();
            lblInProgress = new Label();
            flpInProgress = new DoubleBufferedFlowLayoutPanel();

            pnlReview = new Panel();
            lblReview = new Label();
            flpReview = new DoubleBufferedFlowLayoutPanel();

            pnlTesting = new Panel();
            lblTesting = new Label();
            flpTesting = new DoubleBufferedFlowLayoutPanel();

            pnlFailed = new Panel();
            lblFailed = new Label();
            flpFailed = new DoubleBufferedFlowLayoutPanel();

            pnlDone = new Panel();
            lblDone = new Label();
            flpDone = new DoubleBufferedFlowLayoutPanel();

            panelStatus = new Panel();
            lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelStatus.SuspendLayout();
            tlpBoard.SuspendLayout();
            pnlTodo.SuspendLayout();
            pnlInProgress.SuspendLayout();
            pnlReview.SuspendLayout();
            pnlTesting.SuspendLayout();
            pnlFailed.SuspendLayout();
            pnlDone.SuspendLayout();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelHeader — Dark banner (THÊM MỚI)
            // ════════════════════════════════════════════════════
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Controls.Add(panelAccentLine);

            // Accent line xanh — nhất quán với frmUsers / frmTaskList
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
            lblHeader.Text = "🗂️  Kanban Board";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // panelFilter — Toolbar (THÊM MỚI)
            // ════════════════════════════════════════════════════
            panelFilter.BackColor = UIHelper.ColorBackground;
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";
            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Controls.Add(lblFilterHint);

            UIHelper.StyleToolButton(btnRefresh, "🔄  Làm mới", UIHelper.ButtonVariant.Secondary, 14, 9, 110, 30);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Click += btnRefresh_Click;

            lblFilterHint.AutoSize = false;
            lblFilterHint.Font = UIHelper.FontSmall;
            lblFilterHint.ForeColor = UIHelper.ColorMuted;
            lblFilterHint.Location = new Point(134, 14);
            lblFilterHint.Name = "lblFilterHint";
            lblFilterHint.Size = new Size(380, 18);
            lblFilterHint.Text = "Double-click vào thẻ để mở chi tiết · Kéo thẻ để đổi trạng thái";

            // ════════════════════════════════════════════════════
            // panelStatus — Status bar (THÊM MỚI)
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
            // tlpBoard — 6-column Kanban layout
            // ════════════════════════════════════════════════════
            tlpBoard.BackColor = UIHelper.ColorBackground;
            tlpBoard.ColumnCount = 6;
            tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tlpBoard.Controls.Add(pnlTodo, 0, 0);
            tlpBoard.Controls.Add(pnlInProgress, 1, 0);
            tlpBoard.Controls.Add(pnlReview, 2, 0);
            tlpBoard.Controls.Add(pnlTesting, 3, 0);
            tlpBoard.Controls.Add(pnlFailed, 4, 0);
            tlpBoard.Controls.Add(pnlDone, 5, 0);
            tlpBoard.Dock = DockStyle.Fill;
            tlpBoard.Name = "tlpBoard";
            tlpBoard.Padding = new Padding(8, 10, 8, 10);
            tlpBoard.RowCount = 1;
            tlpBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBoard.TabIndex = 0;

            // ════════════════════════════════════════════════════
            // COLUMN: To Do — Slate (#E2E8F0 header / #F8FAFC body)
            // ════════════════════════════════════════════════════
            var clrTodoBg = System.Drawing.Color.FromArgb(248, 250, 252);
            var clrTodoHeader = System.Drawing.Color.FromArgb(226, 232, 240);
            var clrTodoFg = System.Drawing.Color.FromArgb(51, 65, 85);

            pnlTodo.BackColor = clrTodoBg;
            pnlTodo.Dock = DockStyle.Fill;
            pnlTodo.Margin = new Padding(4);
            pnlTodo.Name = "pnlTodo";
            pnlTodo.Padding = new Padding(0);
            pnlTodo.Controls.Add(flpTodo);
            pnlTodo.Controls.Add(lblTodo);

            lblTodo.BackColor = clrTodoHeader;
            lblTodo.Dock = DockStyle.Top;
            lblTodo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTodo.ForeColor = clrTodoFg;
            lblTodo.Name = "lblTodo";
            lblTodo.Padding = new Padding(10, 0, 0, 0);
            lblTodo.Size = new Size(0, 44);
            lblTodo.TabIndex = 0;
            lblTodo.Text = "📋  To Do";
            lblTodo.TextAlign = ContentAlignment.MiddleLeft;

            flpTodo.AutoScroll = true;
            flpTodo.BackColor = clrTodoBg;
            flpTodo.Dock = DockStyle.Fill;
            flpTodo.FlowDirection = FlowDirection.TopDown;
            flpTodo.Name = "flpTodo";
            flpTodo.Padding = new Padding(6);
            flpTodo.TabIndex = 1;
            flpTodo.WrapContents = false;

            // ════════════════════════════════════════════════════
            // COLUMN: In Progress — Blue (#BFDBFE header / #EFF6FF body)
            // ════════════════════════════════════════════════════
            var clrInBg = System.Drawing.Color.FromArgb(239, 246, 255);
            var clrInHeader = System.Drawing.Color.FromArgb(191, 219, 254);
            var clrInFg = System.Drawing.Color.FromArgb(30, 58, 138);

            pnlInProgress.BackColor = clrInBg;
            pnlInProgress.Dock = DockStyle.Fill;
            pnlInProgress.Margin = new Padding(4);
            pnlInProgress.Name = "pnlInProgress";
            pnlInProgress.Padding = new Padding(0);
            pnlInProgress.Controls.Add(flpInProgress);
            pnlInProgress.Controls.Add(lblInProgress);

            lblInProgress.BackColor = clrInHeader;
            lblInProgress.Dock = DockStyle.Top;
            lblInProgress.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblInProgress.ForeColor = clrInFg;
            lblInProgress.Name = "lblInProgress";
            lblInProgress.Padding = new Padding(10, 0, 0, 0);
            lblInProgress.Size = new Size(0, 44);
            lblInProgress.TabIndex = 1;
            lblInProgress.Text = "🔄  In Progress";
            lblInProgress.TextAlign = ContentAlignment.MiddleLeft;

            flpInProgress.AutoScroll = true;
            flpInProgress.BackColor = clrInBg;
            flpInProgress.Dock = DockStyle.Fill;
            flpInProgress.FlowDirection = FlowDirection.TopDown;
            flpInProgress.Name = "flpInProgress";
            flpInProgress.Padding = new Padding(6);
            flpInProgress.TabIndex = 2;
            flpInProgress.WrapContents = false;

            // ════════════════════════════════════════════════════
            // COLUMN: Review — Amber (#FDE68A header / #FFFBEB body)
            // ════════════════════════════════════════════════════
            var clrRevBg = System.Drawing.Color.FromArgb(255, 251, 235);
            var clrRevHeader = System.Drawing.Color.FromArgb(253, 230, 138);
            var clrRevFg = System.Drawing.Color.FromArgb(120, 53, 15);

            pnlReview.BackColor = clrRevBg;
            pnlReview.Dock = DockStyle.Fill;
            pnlReview.Margin = new Padding(4);
            pnlReview.Name = "pnlReview";
            pnlReview.Padding = new Padding(0);
            pnlReview.Controls.Add(flpReview);
            pnlReview.Controls.Add(lblReview);

            lblReview.BackColor = clrRevHeader;
            lblReview.Dock = DockStyle.Top;
            lblReview.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblReview.ForeColor = clrRevFg;
            lblReview.Name = "lblReview";
            lblReview.Padding = new Padding(10, 0, 0, 0);
            lblReview.Size = new Size(0, 44);
            lblReview.TabIndex = 1;
            lblReview.Text = "👀  Review";
            lblReview.TextAlign = ContentAlignment.MiddleLeft;

            flpReview.AutoScroll = true;
            flpReview.BackColor = clrRevBg;
            flpReview.Dock = DockStyle.Fill;
            flpReview.FlowDirection = FlowDirection.TopDown;
            flpReview.Name = "flpReview";
            flpReview.Padding = new Padding(6);
            flpReview.TabIndex = 2;
            flpReview.WrapContents = false;

            // ════════════════════════════════════════════════════
            // COLUMN: Testing — Purple (#DDD6FE header / #F5F3FF body)
            // ════════════════════════════════════════════════════
            var clrTestBg = System.Drawing.Color.FromArgb(245, 243, 255);
            var clrTestHeader = System.Drawing.Color.FromArgb(221, 214, 254);
            var clrTestFg = System.Drawing.Color.FromArgb(76, 29, 149);

            pnlTesting.BackColor = clrTestBg;
            pnlTesting.Dock = DockStyle.Fill;
            pnlTesting.Margin = new Padding(4);
            pnlTesting.Name = "pnlTesting";
            pnlTesting.Padding = new Padding(0);
            pnlTesting.Controls.Add(flpTesting);
            pnlTesting.Controls.Add(lblTesting);

            lblTesting.BackColor = clrTestHeader;
            lblTesting.Dock = DockStyle.Top;
            lblTesting.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTesting.ForeColor = clrTestFg;
            lblTesting.Name = "lblTesting";
            lblTesting.Padding = new Padding(10, 0, 0, 0);
            lblTesting.Size = new Size(0, 44);
            lblTesting.TabIndex = 1;
            lblTesting.Text = "🧪  Testing";
            lblTesting.TextAlign = ContentAlignment.MiddleLeft;

            flpTesting.AutoScroll = true;
            flpTesting.BackColor = clrTestBg;
            flpTesting.Dock = DockStyle.Fill;
            flpTesting.FlowDirection = FlowDirection.TopDown;
            flpTesting.Name = "flpTesting";
            flpTesting.Padding = new Padding(6);
            flpTesting.TabIndex = 2;
            flpTesting.WrapContents = false;

            // ════════════════════════════════════════════════════
            // COLUMN: Failed — Red (#FECACA header / #FEF2F2 body)
            // ════════════════════════════════════════════════════
            var clrFailBg = System.Drawing.Color.FromArgb(254, 242, 242);
            var clrFailHeader = System.Drawing.Color.FromArgb(254, 202, 202);
            var clrFailFg = System.Drawing.Color.FromArgb(127, 29, 29);

            pnlFailed.BackColor = clrFailBg;
            pnlFailed.Dock = DockStyle.Fill;
            pnlFailed.Margin = new Padding(4);
            pnlFailed.Name = "pnlFailed";
            pnlFailed.Padding = new Padding(0);
            pnlFailed.Controls.Add(flpFailed);
            pnlFailed.Controls.Add(lblFailed);

            lblFailed.BackColor = clrFailHeader;
            lblFailed.Dock = DockStyle.Top;
            lblFailed.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblFailed.ForeColor = clrFailFg;
            lblFailed.Name = "lblFailed";
            lblFailed.Padding = new Padding(10, 0, 0, 0);
            lblFailed.Size = new Size(0, 44);
            lblFailed.TabIndex = 1;
            lblFailed.Text = "❌  Failed";
            lblFailed.TextAlign = ContentAlignment.MiddleLeft;

            flpFailed.AutoScroll = true;
            flpFailed.BackColor = clrFailBg;
            flpFailed.Dock = DockStyle.Fill;
            flpFailed.FlowDirection = FlowDirection.TopDown;
            flpFailed.Name = "flpFailed";
            flpFailed.Padding = new Padding(6);
            flpFailed.TabIndex = 2;
            flpFailed.WrapContents = false;

            // ════════════════════════════════════════════════════
            // COLUMN: Done — Green (#BBF7D0 header / #F0FDF4 body)
            // ════════════════════════════════════════════════════
            var clrDoneBg = System.Drawing.Color.FromArgb(240, 253, 244);
            var clrDoneHeader = System.Drawing.Color.FromArgb(187, 247, 208);
            var clrDoneFg = System.Drawing.Color.FromArgb(20, 83, 45);

            pnlDone.BackColor = clrDoneBg;
            pnlDone.Dock = DockStyle.Fill;
            pnlDone.Margin = new Padding(4);
            pnlDone.Name = "pnlDone";
            pnlDone.Padding = new Padding(0);
            pnlDone.Controls.Add(flpDone);
            pnlDone.Controls.Add(lblDone);

            lblDone.BackColor = clrDoneHeader;
            lblDone.Dock = DockStyle.Top;
            lblDone.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblDone.ForeColor = clrDoneFg;
            lblDone.Name = "lblDone";
            lblDone.Padding = new Padding(10, 0, 0, 0);
            lblDone.Size = new Size(0, 44);
            lblDone.TabIndex = 1;
            lblDone.Text = "✅  Done";
            lblDone.TextAlign = ContentAlignment.MiddleLeft;

            flpDone.AutoScroll = true;
            flpDone.BackColor = clrDoneBg;
            flpDone.Dock = DockStyle.Fill;
            flpDone.FlowDirection = FlowDirection.TopDown;
            flpDone.Name = "flpDone";
            flpDone.Padding = new Padding(6);
            flpDone.TabIndex = 2;
            flpDone.WrapContents = false;

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.Text = "🗂️  Kanban Board";
            this.Size = new Size(1280, 800);
            this.MinimumSize = new Size(1000, 650);
            this.Name = "frmKanban";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Thứ tự Add PHẢI là: Fill trước → Bottom → Top (ngược chiều Dock)
            this.Controls.Add(tlpBoard);     // DockStyle.Fill
            this.Controls.Add(panelStatus);  // DockStyle.Bottom
            this.Controls.Add(panelFilter);  // DockStyle.Top (thứ hai)
            this.Controls.Add(panelHeader);  // DockStyle.Top (trên cùng)

            panelHeader.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            tlpBoard.ResumeLayout(false);
            pnlTodo.ResumeLayout(false);
            pnlInProgress.ResumeLayout(false);
            pnlReview.ResumeLayout(false);
            pnlTesting.ResumeLayout(false);
            pnlFailed.ResumeLayout(false);
            pnlDone.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;
        private Panel panelFilter;
        private Button btnRefresh;
        private Label lblFilterHint;
        private TableLayoutPanel tlpBoard;
        private Panel pnlTodo;
        private Label lblTodo;
        private DoubleBufferedFlowLayoutPanel flpTodo;
        private Panel pnlInProgress;
        private Label lblInProgress;
        private DoubleBufferedFlowLayoutPanel flpInProgress;
        private Panel pnlReview;
        private Label lblReview;
        private DoubleBufferedFlowLayoutPanel flpReview;
        private Panel pnlTesting;
        private Label lblTesting;
        private DoubleBufferedFlowLayoutPanel flpTesting;
        private Panel pnlFailed;
        private Label lblFailed;
        private DoubleBufferedFlowLayoutPanel flpFailed;
        private Panel pnlDone;
        private Label lblDone;
        private DoubleBufferedFlowLayoutPanel flpDone;
        private Panel panelStatus;
        private Label lblStatus;
    }
}