namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmUserEdit
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader     = new System.Windows.Forms.Panel();
            this.lblTitleForm    = new System.Windows.Forms.Label();
            this.panelAccentLine = new System.Windows.Forms.Panel();
            this.panelBody       = new System.Windows.Forms.Panel();
            this.lblUsername     = new System.Windows.Forms.Label();
            this.txtUsername     = new System.Windows.Forms.TextBox();
            this.lblFullName     = new System.Windows.Forms.Label();
            this.txtFullName     = new System.Windows.Forms.TextBox();
            this.lblEmail        = new System.Windows.Forms.Label();
            this.txtEmail        = new System.Windows.Forms.TextBox();
            this.lblPhone        = new System.Windows.Forms.Label();
            this.txtPhone        = new System.Windows.Forms.TextBox();
            this.panelDivider    = new System.Windows.Forms.Panel();
            this.lblPassword     = new System.Windows.Forms.Label();
            this.panelPassInput  = new System.Windows.Forms.Panel();
            this.txtPassword     = new System.Windows.Forms.TextBox();
            this.btnEyePass      = new System.Windows.Forms.Button();
            this.lblRole         = new System.Windows.Forms.Label();
            this.cboRole         = new System.Windows.Forms.ComboBox();
            this.panelFooter     = new System.Windows.Forms.Panel();
            this.panelFooterLine = new System.Windows.Forms.Panel();
            this.lblError        = new System.Windows.Forms.Label();
            this.btnSave         = new System.Windows.Forms.Button();
            this.btnCancel       = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelPassInput.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════
            // panelHeader — Giống hệt frmCustomerEdit
            // ══════════════════════════════════════════════════
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height    = 64;
            this.panelHeader.Name      = "panelHeader";
            this.panelHeader.Controls.Add(this.lblTitleForm);
            this.panelHeader.Controls.Add(this.panelAccentLine);

            this.lblTitleForm.AutoSize  = false;
            this.lblTitleForm.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleForm.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitleForm.ForeColor = System.Drawing.Color.White;
            this.lblTitleForm.Name      = "lblTitleForm";
            this.lblTitleForm.Padding   = new System.Windows.Forms.Padding(18, 0, 0, 4);
            this.lblTitleForm.Size      = new System.Drawing.Size(400, 60);
            this.lblTitleForm.Text      = "➕  Thêm tài khoản mới";
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelAccentLine.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelAccentLine.Height    = 4;
            this.panelAccentLine.Name      = "panelAccentLine";

            // ══════════════════════════════════════════════════
            // panelBody — Cùng pattern với CustomerEdit
            // Spacing: label Y = 16, 82, 148, 214 (mỗi field cách 66px)
            // Label → TextBox gap = 20px
            // Padding left/right = 20px, TextBox width = 360px
            // ══════════════════════════════════════════════════
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelBody.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Name      = "panelBody";
            this.panelBody.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblUsername, this.txtUsername,
                this.lblFullName, this.txtFullName,
                this.lblEmail,   this.txtEmail,
                this.lblPhone,   this.txtPhone,
                this.panelDivider,
                this.lblPassword, this.panelPassInput,
                this.lblRole,    this.cboRole,
            });

            // ── TÊN ĐĂNG NHẬP (y=16, 36) — giống TÊN CÔNG TY ──
            this.lblUsername.AutoSize  = true;
            this.lblUsername.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUsername.Location  = new System.Drawing.Point(20, 16);
            this.lblUsername.Name      = "lblUsername";
            this.lblUsername.Text      = "TÊN ĐĂNG NHẬP";

            this.txtUsername.BackColor       = System.Drawing.Color.White;
            this.txtUsername.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location        = new System.Drawing.Point(20, 36);
            this.txtUsername.Name            = "txtUsername";
            this.txtUsername.PlaceholderText = "Chỉ chữ, số, gạch dưới (3-50 ký tự)";
            this.txtUsername.Size            = new System.Drawing.Size(360, 30);
            this.txtUsername.TabIndex        = 1;

            // ── HỌ VÀ TÊN (y=82, 102) — giống NGƯỜI LIÊN HỆ ───
            this.lblFullName.AutoSize  = true;
            this.lblFullName.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFullName.Location  = new System.Drawing.Point(20, 82);
            this.lblFullName.Name      = "lblFullName";
            this.lblFullName.Text      = "HỌ VÀ TÊN";

            this.txtFullName.BackColor       = System.Drawing.Color.White;
            this.txtFullName.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullName.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.Location        = new System.Drawing.Point(20, 102);
            this.txtFullName.Name            = "txtFullName";
            this.txtFullName.PlaceholderText = "Nguyễn Văn A";
            this.txtFullName.Size            = new System.Drawing.Size(360, 30);
            this.txtFullName.TabIndex        = 2;

            // ── EMAIL (y=148, 168) — giống EMAIL Customer ────────
            this.lblEmail.AutoSize  = true;
            this.lblEmail.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblEmail.Location  = new System.Drawing.Point(20, 148);
            this.lblEmail.Name      = "lblEmail";
            this.lblEmail.Text      = "EMAIL";

            this.txtEmail.BackColor       = System.Drawing.Color.White;
            this.txtEmail.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location        = new System.Drawing.Point(20, 168);
            this.txtEmail.Name            = "txtEmail";
            this.txtEmail.PlaceholderText = "example@email.com";
            this.txtEmail.Size            = new System.Drawing.Size(360, 30);
            this.txtEmail.TabIndex        = 3;

            // ── SỐ ĐIỆN THOẠI (y=214, 234) — giống ĐIỆN THOẠI ──
            this.lblPhone.AutoSize  = true;
            this.lblPhone.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPhone.Location  = new System.Drawing.Point(20, 214);
            this.lblPhone.Name      = "lblPhone";
            this.lblPhone.Text      = "SỐ ĐIỆN THOẠI  (tùy chọn)";

            this.txtPhone.BackColor       = System.Drawing.Color.White;
            this.txtPhone.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location        = new System.Drawing.Point(20, 234);
            this.txtPhone.Name            = "txtPhone";
            this.txtPhone.PlaceholderText = "0912 345 678";
            this.txtPhone.Size            = new System.Drawing.Size(360, 30);
            this.txtPhone.TabIndex        = 4;

            // ── Divider line (y=280) ─────────────────────────────
            this.panelDivider.BackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.panelDivider.Location  = new System.Drawing.Point(20, 280);
            this.panelDivider.Name      = "panelDivider";
            this.panelDivider.Size      = new System.Drawing.Size(360, 1);

            // ── MẬT KHẨU (y=296, 316) ───────────────────────────
            this.lblPassword.AutoSize  = true;
            this.lblPassword.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPassword.Location  = new System.Drawing.Point(20, 296);
            this.lblPassword.Name      = "lblPassword";
            this.lblPassword.Text      = "MẬT KHẨU";

            // Panel chứa txtPassword + btnEye để eye button nằm trong textbox
            this.panelPassInput.BackColor   = System.Drawing.Color.White;
            this.panelPassInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPassInput.Location    = new System.Drawing.Point(20, 316);
            this.panelPassInput.Name        = "panelPassInput";
            this.panelPassInput.Size        = new System.Drawing.Size(360, 32);
            this.panelPassInput.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.txtPassword, this.btnEyePass });

            this.txtPassword.BackColor             = System.Drawing.Color.White;
            this.txtPassword.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font                  = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location              = new System.Drawing.Point(6, 5);
            this.txtPassword.Name                  = "txtPassword";
            this.txtPassword.PlaceholderText       = "Tối thiểu 6 ký tự";
            this.txtPassword.Size                  = new System.Drawing.Size(314, 22);
            this.txtPassword.TabIndex              = 5;
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnEyePass.BackColor                         = System.Drawing.Color.Transparent;
            this.btnEyePass.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnEyePass.FlatAppearance.BorderSize         = 0;
            this.btnEyePass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEyePass.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnEyePass.Font                              = new System.Drawing.Font("Segoe UI Emoji", 11F);
            this.btnEyePass.ForeColor                         = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnEyePass.Location                          = new System.Drawing.Point(324, 2);
            this.btnEyePass.Name                              = "btnEyePass";
            this.btnEyePass.Size                              = new System.Drawing.Size(32, 28);
            this.btnEyePass.TabStop                           = false;
            this.btnEyePass.Text                              = "👁";
            this.btnEyePass.UseVisualStyleBackColor           = false;
            this.btnEyePass.Click                            += new System.EventHandler(this.btnEyePass_Click);

            // ── VAI TRÒ (y=362, 382) ────────────────────────────
            // Hiện khi tạo mới + khi Admin edit (cho phép đổi role)
            this.lblRole.AutoSize  = true;
            this.lblRole.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRole.Location  = new System.Drawing.Point(20, 362);
            this.lblRole.Name      = "lblRole";
            this.lblRole.Text      = "VAI TRÒ";

            this.cboRole.BackColor      = System.Drawing.Color.White;
            this.cboRole.DropDownStyle  = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.Font           = new System.Drawing.Font("Segoe UI", 10F);
            this.cboRole.Location       = new System.Drawing.Point(20, 382);
            this.cboRole.Name           = "cboRole";
            this.cboRole.Size           = new System.Drawing.Size(360, 31);
            this.cboRole.TabIndex       = 6;

            // ══════════════════════════════════════════════════
            // panelFooter — Giống hệt frmCustomerEdit
            // ══════════════════════════════════════════════════
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Height    = 76;
            this.panelFooter.Name      = "panelFooter";
            this.panelFooter.Controls.Add(this.panelFooterLine);
            this.panelFooter.Controls.Add(this.lblError);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);

            this.panelFooterLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.panelFooterLine.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelFooterLine.Height    = 1;
            this.panelFooterLine.Name      = "panelFooterLine";

            this.lblError.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblError.Location  = new System.Drawing.Point(16, 6);
            this.lblError.Name      = "lblError";
            this.lblError.Size      = new System.Drawing.Size(370, 18);
            this.lblError.Text      = "";

            this.btnSave.BackColor                         = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSave.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize         = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnSave.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font                              = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor                         = System.Drawing.Color.White;
            this.btnSave.Location                          = new System.Drawing.Point(16, 28);
            this.btnSave.Name                              = "btnSave";
            this.btnSave.Size                              = new System.Drawing.Size(230, 40);
            this.btnSave.TabIndex                          = 7;
            this.btnSave.Text                              = "💾  Lưu";
            this.btnSave.UseVisualStyleBackColor           = false;
            this.btnSave.Click                            += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.BackColor                         = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnCancel.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor        = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnCancel.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font                              = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor                         = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnCancel.Location                          = new System.Drawing.Point(256, 28);
            this.btnCancel.Name                              = "btnCancel";
            this.btnCancel.Size                              = new System.Drawing.Size(124, 40);
            this.btnCancel.TabIndex                          = 8;
            this.btnCancel.Text                              = "Hủy";
            this.btnCancel.UseVisualStyleBackColor           = false;
            this.btnCancel.Click                            += new System.EventHandler(this.btnCancel_Click);

            // ══════════════════════════════════════════════════
            // frmUserEdit — Cùng width 400 với frmCustomerEdit
            // Height 580 đủ chỗ cho 4 fields + password + role + footer
            // ══════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(400, 580);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "frmUserEdit";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Tài khoản";

            this.panelHeader.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelPassInput.ResumeLayout(false);
            this.panelPassInput.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel    panelHeader;
        private System.Windows.Forms.Panel    panelAccentLine;
        private System.Windows.Forms.Label    lblTitleForm;
        private System.Windows.Forms.Panel    panelBody;
        private System.Windows.Forms.Label    lblUsername;
        private System.Windows.Forms.TextBox  txtUsername;
        private System.Windows.Forms.Label    lblFullName;
        private System.Windows.Forms.TextBox  txtFullName;
        private System.Windows.Forms.Label    lblEmail;
        private System.Windows.Forms.TextBox  txtEmail;
        private System.Windows.Forms.Label    lblPhone;
        private System.Windows.Forms.TextBox  txtPhone;
        private System.Windows.Forms.Panel    panelDivider;
        private System.Windows.Forms.Label    lblPassword;
        private System.Windows.Forms.Panel    panelPassInput;
        private System.Windows.Forms.TextBox  txtPassword;
        private System.Windows.Forms.Button   btnEyePass;
        private System.Windows.Forms.Label    lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Panel    panelFooter;
        private System.Windows.Forms.Panel    panelFooterLine;
        private System.Windows.Forms.Label    lblError;
        private System.Windows.Forms.Button   btnSave;
        private System.Windows.Forms.Button   btnCancel;
    }
}
