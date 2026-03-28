using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmCustomers   // BaseForm declared in frmCustomers.cs
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
            panelTop = new Panel();
            panelAccentLine = new Panel();
            lblHeader = new Label();

            panelFilter = new Panel();
            txtSearch = new TextBox();
            btnRefresh = new Button();

            panelToolbar = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnDetail = new Button();
            lblCount = new Label();

            dgvCustomers = new DataGridView();

            panelStatus = new Panel();
            lblStatus = new Label();

            colCustId = new DataGridViewTextBoxColumn();
            colCompany = new DataGridViewTextBoxColumn();
            colContact = new DataGridViewTextBoxColumn();
            colCustEmail = new DataGridViewTextBoxColumn();
            colCustPhone = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colCreatedAt = new DataGridViewTextBoxColumn();

            panelTop.SuspendLayout();
            panelFilter.SuspendLayout();
            panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelTop — Dark header + accent line
            // SỬA: height 52→58, thêm panelAccentLine
            // ════════════════════════════════════════════════════
            panelTop.BackColor = UIHelper.ColorHeaderBg;
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 58;
            panelTop.Name = "panelTop";
            panelTop.Controls.Add(lblHeader);
            panelTop.Controls.Add(panelAccentLine);

            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(16, 0, 0, 4);
            lblHeader.Text = "🏢  Quản lý Khách hàng";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // panelFilter — Search + Refresh
            // SỬA: hard-code #F8FAFC → UIHelper.ColorBackground
            // ════════════════════════════════════════════════════
            panelFilter.BackColor = UIHelper.ColorBackground;
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";
            panelFilter.Controls.AddRange(new Control[] { txtSearch, btnRefresh });

            txtSearch.Font = UIHelper.FontSmall;
            txtSearch.Location = new System.Drawing.Point(12, 10);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍  Tìm theo tên công ty, liên hệ, email...";
            txtSearch.Size = new System.Drawing.Size(340, 26);
            txtSearch.TextChanged += txtSearch_TextChanged;

            // SỬA: icon-only 36px → text đầy đủ với UIHelper
            UIHelper.StyleToolButton(btnRefresh, "🔄  Làm mới", UIHelper.ButtonVariant.Secondary, 362, 10, 100, 26);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Click += btnRefresh_Click;

            // ════════════════════════════════════════════════════
            // panelToolbar — CRUD buttons
            // SỬA: White bg → UIHelper.ColorSurface; tất cả btn hard-code → UIHelper
            // ════════════════════════════════════════════════════
            panelToolbar.BackColor = UIHelper.ColorSurface;
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Height = 52;
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Controls.AddRange(new Control[]
            { btnAdd, btnEdit, btnDelete, btnDetail, lblCount });

            int bx = 12, by = 9, bg = 6, bh = 34;
            UIHelper.StyleToolButton(btnAdd, "➕  Thêm mới", UIHelper.ButtonVariant.Primary, bx, by, 120, bh); bx += 120 + bg;
            UIHelper.StyleToolButton(btnEdit, "✏️  Sửa", UIHelper.ButtonVariant.Success, bx, by, 90, bh); bx += 90 + bg;
            UIHelper.StyleToolButton(btnDelete, "🗑️  Xóa", UIHelper.ButtonVariant.Danger, bx, by, 80, bh); bx += 80 + bg;
            UIHelper.StyleToolButton(btnDetail, "📋  Xem dự án", UIHelper.ButtonVariant.Slate, bx, by, 120, bh); bx += 120 + bg;

            btnAdd.Name = "btnAdd"; btnAdd.Click += btnAdd_Click;
            btnEdit.Name = "btnEdit"; btnEdit.Click += btnEdit_Click;
            btnDelete.Name = "btnDelete"; btnDelete.Click += btnDelete_Click;
            btnDetail.Name = "btnDetail"; btnDetail.Click += btnDetail_Click;

            btnEdit.Enabled = btnDelete.Enabled = btnDetail.Enabled = false;

            lblCount.AutoSize = false;
            lblCount.Font = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorMuted;
            lblCount.Location = new System.Drawing.Point(bx, by);
            lblCount.Name = "lblCount";
            lblCount.Size = new System.Drawing.Size(180, bh);
            lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // dgvCustomers — DataGridView
            // SỬA: Toàn bộ hard-code → UIHelper.StyleDataGridView()
            // ════════════════════════════════════════════════════
            UIHelper.StyleDataGridView(dgvCustomers);
            UIHelper.ApplyAlternateRowColors(dgvCustomers);
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowTemplate.Height = 34;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            dgvCustomers.CellDoubleClick += dgvCustomers_CellDoubleClick;

            colCustId.Name = "colCustId"; colCustId.HeaderText = "ID"; colCustId.Width = 45; colCustId.Visible = false;
            colCompany.Name = "colCompany"; colCompany.HeaderText = "Tên công ty"; colCompany.Width = 220;
            colContact.Name = "colContact"; colContact.HeaderText = "Người liên hệ"; colContact.Width = 160;
            colCustEmail.Name = "colCustEmail"; colCustEmail.HeaderText = "Email"; colCustEmail.Width = 190;
            colCustPhone.Name = "colCustPhone"; colCustPhone.HeaderText = "Điện thoại"; colCustPhone.Width = 120;
            colAddress.Name = "colAddress"; colAddress.HeaderText = "Địa chỉ"; colAddress.Width = 200;
            colCreatedAt.Name = "colCreatedAt"; colCreatedAt.HeaderText = "Ngày tạo"; colCreatedAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvCustomers.Columns.AddRange(new DataGridViewColumn[]
            { colCustId, colCompany, colContact, colCustEmail, colCustPhone, colAddress, colCreatedAt });

            // ════════════════════════════════════════════════════
            // panelStatus — Status bar
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
            lblStatus.Text = "";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Font = UIHelper.FontBase;
            this.Name = "frmCustomers";
            this.Text = "🏢  Quản lý Khách hàng";
            this.StartPosition = FormStartPosition.Manual;

            // Thứ tự Add: Fill → Bottom → Top (ngược chiều Dock)
            this.Controls.Add(dgvCustomers);
            this.Controls.Add(panelStatus);
            this.Controls.Add(panelToolbar);
            this.Controls.Add(panelFilter);
            this.Controls.Add(panelTop);

            panelTop.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelTop, panelAccentLine, panelFilter, panelToolbar, panelStatus;
        private Label lblHeader, lblCount, lblStatus;
        private TextBox txtSearch;
        private Button btnRefresh, btnAdd, btnEdit, btnDelete, btnDetail;
        private DataGridView dgvCustomers;
        private DataGridViewTextBoxColumn colCustId, colCompany, colContact;
        private DataGridViewTextBoxColumn colCustEmail, colCustPhone, colAddress, colCreatedAt;
    }
}