namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmUsers
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop     = new System.Windows.Forms.Panel();
            this.lblHeader    = new System.Windows.Forms.Label();
            this.panelFilter  = new System.Windows.Forms.Panel();
            this.txtSearch    = new System.Windows.Forms.TextBox();
            this.cboFilterRole   = new System.Windows.Forms.ComboBox();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.btnRefresh   = new System.Windows.Forms.Button();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnAdd       = new System.Windows.Forms.Button();
            this.btnEdit      = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnActivate  = new System.Windows.Forms.Button();
            this.lblCount     = new System.Windows.Forms.Label();
            this.dgvUsers     = new System.Windows.Forms.DataGridView();
            this.panelStatus  = new System.Windows.Forms.Panel();
            this.lblStatus    = new System.Windows.Forms.Label();

            // Columns
            this.colId        = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFullName  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRole      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus    = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvUsers).BeginInit();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════
            // panelTop — Header dark navy
            // ══════════════════════════════════════════════════
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height    = 52;
            this.panelTop.Name      = "panelTop";
            this.panelTop.Controls.Add(this.lblHeader);

            this.lblHeader.AutoSize  = false;
            this.lblHeader.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Name      = "lblHeader";
            this.lblHeader.Text      = "  👥  Quản lý Tài khoản";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════
            // panelFilter — Search + ComboBoxes + Refresh
            // FIX: Căn chỉnh đều, tăng height, thêm padding
            // ══════════════════════════════════════════════════
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelFilter.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Height    = 50;
            this.panelFilter.Name      = "panelFilter";
            this.panelFilter.Padding   = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.panelFilter.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.txtSearch, this.cboFilterRole, this.cboFilterStatus, this.btnRefresh });

            this.txtSearch.BorderStyle   = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location      = new System.Drawing.Point(14, 12);
            this.txtSearch.Name          = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍  Tìm theo tên, username, email...";
            this.txtSearch.Size          = new System.Drawing.Size(300, 30);
            this.txtSearch.TextChanged  += new System.EventHandler(this.txtSearch_TextChanged);

            this.cboFilterRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterRole.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cboFilterRole.Location      = new System.Drawing.Point(324, 12);
            this.cboFilterRole.Name          = "cboFilterRole";
            this.cboFilterRole.Size          = new System.Drawing.Size(140, 30);
            this.cboFilterRole.Items.AddRange(new object[] { "Tất cả Role", "Admin", "Manager", "Developer" });
            this.cboFilterRole.SelectedIndex = 0;
            this.cboFilterRole.SelectedIndexChanged += new System.EventHandler(this.cboFilterRole_SelectedIndexChanged);

            this.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterStatus.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cboFilterStatus.Location      = new System.Drawing.Point(474, 12);
            this.cboFilterStatus.Name          = "cboFilterStatus";
            this.cboFilterStatus.Size          = new System.Drawing.Size(130, 30);
            this.cboFilterStatus.Items.AddRange(new object[] { "Tất cả", "Active", "Inactive" });
            this.cboFilterStatus.SelectedIndex = 0;
            this.cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cboFilterStatus_SelectedIndexChanged);

            // FIX: Refresh button cùng height với combobox, sát bên cạnh
            this.btnRefresh.BackColor                         = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRefresh.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor        = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnRefresh.Font                              = new System.Drawing.Font("Segoe UI Emoji", 11F);
            this.btnRefresh.Location                          = new System.Drawing.Point(614, 12);
            this.btnRefresh.Name                              = "btnRefresh";
            this.btnRefresh.Size                              = new System.Drawing.Size(38, 30);
            this.btnRefresh.Text                              = "🔄";
            this.btnRefresh.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click                            += new System.EventHandler(this.btnRefresh_Click);

            // ══════════════════════════════════════════════════
            // panelToolbar — Action buttons
            // FIX: Buttons rộng hơn, spacing đều 10px, height 34px, border-radius feel
            // ══════════════════════════════════════════════════
            this.panelToolbar.BackColor = System.Drawing.Color.White;
            this.panelToolbar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Height    = 50;
            this.panelToolbar.Name      = "panelToolbar";
            this.panelToolbar.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.btnAdd, this.btnEdit, this.btnDeactivate, this.btnActivate, this.lblCount });

            // ── btnAdd ───────────────────────────────────────
            this.btnAdd.BackColor                 = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdd.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnAdd.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor                 = System.Drawing.Color.White;
            this.btnAdd.Location                  = new System.Drawing.Point(14, 9);
            this.btnAdd.Name                      = "btnAdd";
            this.btnAdd.Size                      = new System.Drawing.Size(120, 34);
            this.btnAdd.Text                      = "➕  Thêm mới";

            // ── btnEdit ──────────────────────────────────────
            this.btnEdit.BackColor                 = System.Drawing.Color.FromArgb(5, 150, 105);
            this.btnEdit.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(4, 120, 87);
            this.btnEdit.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor                 = System.Drawing.Color.White;
            this.btnEdit.Location                  = new System.Drawing.Point(144, 9);
            this.btnEdit.Name                      = "btnEdit";
            this.btnEdit.Size                      = new System.Drawing.Size(140, 34);
            this.btnEdit.Text                      = "✏️  Sửa thông tin";

            // ── btnDeactivate ────────────────────────────────
            this.btnDeactivate.BackColor                 = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDeactivate.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnDeactivate.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.FlatAppearance.BorderSize = 0;
            this.btnDeactivate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnDeactivate.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeactivate.ForeColor                 = System.Drawing.Color.White;
            this.btnDeactivate.Location                  = new System.Drawing.Point(294, 9);
            this.btnDeactivate.Name                      = "btnDeactivate";
            this.btnDeactivate.Size                      = new System.Drawing.Size(140, 34);
            this.btnDeactivate.Text                      = "🔴  Vô hiệu hóa";

            // ── btnActivate ──────────────────────────────────
            this.btnActivate.BackColor                 = System.Drawing.Color.FromArgb(124, 58, 237);
            this.btnActivate.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnActivate.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivate.FlatAppearance.BorderSize = 0;
            this.btnActivate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(109, 40, 217);
            this.btnActivate.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActivate.ForeColor                 = System.Drawing.Color.White;
            this.btnActivate.Location                  = new System.Drawing.Point(444, 9);
            this.btnActivate.Name                      = "btnActivate";
            this.btnActivate.Size                      = new System.Drawing.Size(140, 34);
            this.btnActivate.Text                      = "✅  Kích hoạt lại";

            this.btnAdd.Click        += new System.EventHandler(this.btnAdd_Click);
            this.btnEdit.Click       += new System.EventHandler(this.btnEdit_Click);
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            this.btnActivate.Click   += new System.EventHandler(this.btnActivate_Click);
            this.btnEdit.Enabled       = false;
            this.btnDeactivate.Enabled = false;
            this.btnActivate.Enabled   = false;

            // FIX: lblCount nằm sát phải hơn, căn giữa vertical
            this.lblCount.AutoSize  = false;
            this.lblCount.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCount.Location  = new System.Drawing.Point(596, 9);
            this.lblCount.Name      = "lblCount";
            this.lblCount.Size      = new System.Drawing.Size(160, 34);
            this.lblCount.Text      = "";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════
            // DataGridView — Danh sách users
            // ══════════════════════════════════════════════════
            this.dgvUsers.AllowUserToAddRows    = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.BackgroundColor       = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.dgvUsers.CellBorderStyle       = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock                  = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Font                  = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvUsers.GridColor             = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvUsers.MultiSelect           = false;
            this.dgvUsers.Name                  = "dgvUsers";
            this.dgvUsers.ReadOnly              = true;
            this.dgvUsers.RowHeadersVisible     = false;
            this.dgvUsers.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Padding   = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.RowTemplate.Height    = 36;
            this.dgvUsers.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.dgvUsers.SelectionChanged     += new System.EventHandler(this.dgvUsers_SelectionChanged);
            this.dgvUsers.CellDoubleClick      += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellDoubleClick);

            // Columns
            this.colId.Name = "colId"; this.colId.HeaderText = "ID"; this.colId.Width = 45; this.colId.Visible = false;
            this.colUsername.Name = "colUsername";   this.colUsername.HeaderText = "Username";         this.colUsername.Width = 120;
            this.colFullName.Name = "colFullName";   this.colFullName.HeaderText = "Họ và tên";       this.colFullName.Width = 180;
            this.colEmail.Name    = "colEmail";      this.colEmail.HeaderText    = "Email";            this.colEmail.Width    = 200;
            this.colPhone.Name    = "colPhone";      this.colPhone.HeaderText    = "Điện thoại";      this.colPhone.Width    = 120;
            this.colRole.Name     = "colRole";       this.colRole.HeaderText     = "Vai trò";         this.colRole.Width     = 100;
            this.colStatus.Name   = "colStatus";     this.colStatus.HeaderText   = "Trạng thái";      this.colStatus.Width   = 100;
            this.colLastLogin.Name = "colLastLogin"; this.colLastLogin.HeaderText = "Đăng nhập cuối"; this.colLastLogin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            { colId, colUsername, colFullName, colEmail, colPhone, colRole, colStatus, colLastLogin });

            // ══════════════════════════════════════════════════
            // panelStatus — Status bar
            // ══════════════════════════════════════════════════
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelStatus.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Height    = 28;
            this.panelStatus.Name      = "panelStatus";
            this.panelStatus.Controls.Add(this.lblStatus);

            this.lblStatus.AutoSize  = false;
            this.lblStatus.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStatus.Name      = "lblStatus";
            this.lblStatus.Padding   = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblStatus.Text      = "";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════
            // frmUsers
            // ══════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name          = "frmUsers";
            this.Text          = "Quản lý Tài khoản";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvUsers).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterRole;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastLogin;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}
