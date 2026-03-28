using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectMembers   // BaseForm declared in frmProjectMembers.cs
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
            lblTitle = new Label();
            panelAccent = new Panel();

            dgvMembers = new DataGridView();

            panelAdd = new Panel();
            lblAddTitle = new Label();
            cboUser = new ComboBox();
            cboProjectRole = new ComboBox();
            btnAddMember = new Button();

            panelBottom = new Panel();
            btnRemove = new Button();
            btnClose = new Button();
            lblCount = new Label();

            colMemberId = new DataGridViewTextBoxColumn();
            colMemberName = new DataGridViewTextBoxColumn();
            colMemberEmail = new DataGridViewTextBoxColumn();
            colMemberRole = new DataGridViewTextBoxColumn();
            colJoinedAt = new DataGridViewTextBoxColumn();

            panelHeader.SuspendLayout();
            panelAdd.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelHeader — Dark banner
            // ════════════════════════════════════════════════════
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(panelAccent);

            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = UIHelper.FontHeaderLarge;
            lblTitle.ForeColor = UIHelper.ColorHeaderFg;
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(16, 0, 0, 0);
            lblTitle.Text = "👥  Thành viên dự án";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // SỬA: Purple → Blue (#2563EB) để nhất quán; height 3 → 4
            panelAccent.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            panelAccent.Dock = DockStyle.Bottom;
            panelAccent.Height = 4;
            panelAccent.Name = "panelAccent";

            // ════════════════════════════════════════════════════
            // dgvMembers — DataGridView
            // SỬA: Toàn bộ hard-code → UIHelper.StyleDataGridView()
            // ════════════════════════════════════════════════════
            UIHelper.StyleDataGridView(dgvMembers);
            UIHelper.ApplyAlternateRowColors(dgvMembers);
            dgvMembers.Dock = DockStyle.Fill;
            dgvMembers.Name = "dgvMembers";
            dgvMembers.RowTemplate.Height = 34;

            colMemberId.Name = "colMemberId"; colMemberId.HeaderText = "ID"; colMemberId.Visible = false;
            colMemberName.Name = "colMemberName"; colMemberName.HeaderText = "Họ tên"; colMemberName.Width = 180;
            colMemberEmail.Name = "colMemberEmail"; colMemberEmail.HeaderText = "Email"; colMemberEmail.Width = 200;
            colMemberRole.Name = "colMemberRole"; colMemberRole.HeaderText = "Vai trò DA"; colMemberRole.Width = 110;
            colJoinedAt.Name = "colJoinedAt"; colJoinedAt.HeaderText = "Ngày tham gia"; colJoinedAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvMembers.Columns.AddRange(new DataGridViewColumn[]
            { colMemberId, colMemberName, colMemberEmail, colMemberRole, colJoinedAt });

            // ════════════════════════════════════════════════════
            // panelAdd — Khu vực thêm thành viên
            // SỬA: hard-code #F8FAFC → UIHelper.ColorBackground
            // ════════════════════════════════════════════════════
            panelAdd.BackColor = UIHelper.ColorBackground;
            panelAdd.Dock = DockStyle.Bottom;
            panelAdd.Height = 80;
            panelAdd.Name = "panelAdd";
            panelAdd.Controls.AddRange(new Control[]
            { lblAddTitle, cboUser, cboProjectRole, btnAddMember });

            lblAddTitle.AutoSize = true;
            lblAddTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lblAddTitle.ForeColor = UIHelper.ColorDark;
            lblAddTitle.Location = new System.Drawing.Point(14, 8);
            lblAddTitle.Name = "lblAddTitle";
            lblAddTitle.Text = "THÊM THÀNH VIÊN";

            cboUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUser.Font = UIHelper.FontBase;
            cboUser.Location = new System.Drawing.Point(14, 30);
            cboUser.Name = "cboUser";
            cboUser.Size = new System.Drawing.Size(270, 30);

            cboProjectRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProjectRole.Font = UIHelper.FontBase;
            cboProjectRole.Location = new System.Drawing.Point(294, 30);
            cboProjectRole.Name = "cboProjectRole";
            cboProjectRole.Size = new System.Drawing.Size(120, 30);

            // btnAddMember — Primary (SỬA: hard-code → UIHelper)
            UIHelper.StyleButton(btnAddMember, UIHelper.ButtonVariant.Primary);
            btnAddMember.Location = new System.Drawing.Point(424, 30);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.Size = new System.Drawing.Size(100, 30);
            btnAddMember.Text = "➕  Thêm";
            btnAddMember.Click += btnAddMember_Click;

            // ════════════════════════════════════════════════════
            // panelBottom — Nút xóa, đóng + count
            // SỬA: BackColor White → UIHelper.ColorSurface
            // ════════════════════════════════════════════════════
            panelBottom.BackColor = UIHelper.ColorSurface;
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 52;
            panelBottom.Name = "panelBottom";
            panelBottom.Controls.AddRange(new Control[] { btnRemove, lblCount, btnClose });

            // btnRemove — Danger (SỬA: hard-code → UIHelper)
            UIHelper.StyleButton(btnRemove, UIHelper.ButtonVariant.Danger);
            btnRemove.Location = new System.Drawing.Point(14, 10);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(150, 32);
            btnRemove.Text = "🗑️  Xóa thành viên";
            btnRemove.Click += btnRemove_Click;

            lblCount.AutoSize = false;
            lblCount.Font = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorMuted;
            lblCount.Location = new System.Drawing.Point(174, 10);
            lblCount.Name = "lblCount";
            lblCount.Size = new System.Drawing.Size(200, 32);
            lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnClose — Secondary (SỬA: hard-code → UIHelper)
            UIHelper.StyleButton(btnClose, UIHelper.ButtonVariant.Secondary);
            btnClose.Location = new System.Drawing.Point(428, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(96, 32);
            btnClose.Text = "Đóng";
            btnClose.Font = UIHelper.FontBase;
            btnClose.Click += btnClose_Click;

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 500);
            this.Font = UIHelper.FontBase;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectMembers";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thành viên dự án";

            // Thứ tự Add: Fill → Bottom panels → Top
            this.Controls.Add(dgvMembers);    // DockStyle.Fill
            this.Controls.Add(panelAdd);      // DockStyle.Bottom (thứ nhất từ dưới lên)
            this.Controls.Add(panelBottom);   // DockStyle.Bottom (tiếp theo)
            this.Controls.Add(panelHeader);   // DockStyle.Top

            panelHeader.ResumeLayout(false);
            panelAdd.ResumeLayout(false);
            panelAdd.PerformLayout();
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader, panelAccent;
        private Label lblTitle;
        private DataGridView dgvMembers;
        private Panel panelAdd;
        private Label lblAddTitle;
        private ComboBox cboUser, cboProjectRole;
        private Button btnAddMember;
        private Panel panelBottom;
        private Button btnRemove, btnClose;
        private Label lblCount;
        private DataGridViewTextBoxColumn colMemberId, colMemberName;
        private DataGridViewTextBoxColumn colMemberEmail, colMemberRole, colJoinedAt;
    }
}