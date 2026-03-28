namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmChangePassword
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader  = new System.Windows.Forms.Panel();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.lblSubtitle  = new System.Windows.Forms.Label();
            this.panelForm    = new System.Windows.Forms.Panel();

            this.lblOldPass   = new System.Windows.Forms.Label();
            this.panelOld     = new System.Windows.Forms.Panel();
            this.txtOldPass   = new System.Windows.Forms.TextBox();
            this.btnEye1      = new System.Windows.Forms.Button();

            this.lblNewPass   = new System.Windows.Forms.Label();
            this.panelNew     = new System.Windows.Forms.Panel();
            this.txtNewPass   = new System.Windows.Forms.TextBox();
            this.btnEye2      = new System.Windows.Forms.Button();

            this.lblConfirm   = new System.Windows.Forms.Label();
            this.panelConfirm = new System.Windows.Forms.Panel();
            this.txtConfirm   = new System.Windows.Forms.TextBox();
            this.btnEye3      = new System.Windows.Forms.Button();

            this.lblHint      = new System.Windows.Forms.Label();
            this.lblError     = new System.Windows.Forms.Label();
            this.lblSuccess   = new System.Windows.Forms.Label();
            this.btnConfirm   = new System.Windows.Forms.Button();
            this.btnCancel    = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ──────────────────────────────────────
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height    = 72;
            this.panelHeader.Name      = "panelHeader";
            this.panelHeader.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.lblTitle, this.lblSubtitle });

            this.lblTitle.AutoSize  = false;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(24, 10);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.Size      = new System.Drawing.Size(352, 30);
            this.lblTitle.Text      = "🔐  Đổi mật khẩu";

            this.lblSubtitle.AutoSize  = false;
            this.lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubtitle.Location  = new System.Drawing.Point(26, 44);
            this.lblSubtitle.Name      = "lblSubtitle";
            this.lblSubtitle.Size      = new System.Drawing.Size(352, 20);
            this.lblSubtitle.Text      = "Nhập mật khẩu cũ và mật khẩu mới để xác nhận";

            // ── panelForm ─────────────────────────────────────────
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Name      = "panelForm";
            this.panelForm.AutoScroll = true; // BUG FIX #1: đảm bảo cuộn được nếu cần
            this.panelForm.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblOldPass,   this.panelOld,
                this.lblNewPass,   this.panelNew,
                this.lblConfirm,   this.panelConfirm,
                this.lblHint,
                this.lblError,     this.lblSuccess,
                this.btnConfirm,   this.btnCancel
            });

            // ── Mật khẩu cũ ──────────────────────────────────────
            this.lblOldPass.AutoSize  = true;
            this.lblOldPass.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblOldPass.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblOldPass.Location  = new System.Drawing.Point(24, 16);
            this.lblOldPass.Name      = "lblOldPass";
            this.lblOldPass.Text      = "MẬT KHẨU HIỆN TẠI";

            this.panelOld.BackColor   = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelOld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOld.Location    = new System.Drawing.Point(24, 36);
            this.panelOld.Name        = "panelOld";
            this.panelOld.Size        = new System.Drawing.Size(352, 40);
            this.panelOld.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtOldPass, this.btnEye1 });

            this.txtOldPass.BackColor             = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtOldPass.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.txtOldPass.Font                  = new System.Drawing.Font("Segoe UI", 11F);
            this.txtOldPass.Location              = new System.Drawing.Point(10, 9);
            this.txtOldPass.Name                  = "txtOldPass";
            this.txtOldPass.PlaceholderText       = "Nhập mật khẩu hiện tại...";
            this.txtOldPass.Size                  = new System.Drawing.Size(294, 22);
            this.txtOldPass.UseSystemPasswordChar = true;

            this.btnEye1.BackColor                         = System.Drawing.Color.Transparent;
            this.btnEye1.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnEye1.FlatAppearance.BorderSize         = 0;
            this.btnEye1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEye1.Font                              = new System.Drawing.Font("Segoe UI Emoji", 12F);
            this.btnEye1.ForeColor                         = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnEye1.Location                          = new System.Drawing.Point(314, 6);
            this.btnEye1.Name                              = "btnEye1";
            this.btnEye1.Size                              = new System.Drawing.Size(30, 28);
            this.btnEye1.TabStop                           = false;
            this.btnEye1.Text                              = "👁";
            this.btnEye1.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnEye1.Click                            += new System.EventHandler(this.btnEye1_Click);

            // ── Mật khẩu mới ─────────────────────────────────────
            this.lblNewPass.AutoSize  = true;
            this.lblNewPass.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNewPass.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNewPass.Location  = new System.Drawing.Point(24, 94);
            this.lblNewPass.Name      = "lblNewPass";
            this.lblNewPass.Text      = "MẬT KHẨU MỚI";

            this.panelNew.BackColor   = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNew.Location    = new System.Drawing.Point(24, 114);
            this.panelNew.Name        = "panelNew";
            this.panelNew.Size        = new System.Drawing.Size(352, 40);
            this.panelNew.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtNewPass, this.btnEye2 });

            this.txtNewPass.BackColor             = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtNewPass.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.txtNewPass.Font                  = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNewPass.Location              = new System.Drawing.Point(10, 9);
            this.txtNewPass.Name                  = "txtNewPass";
            this.txtNewPass.PlaceholderText       = "Tối thiểu 6 ký tự...";
            this.txtNewPass.Size                  = new System.Drawing.Size(294, 22);
            this.txtNewPass.UseSystemPasswordChar = true;

            this.btnEye2.BackColor                         = System.Drawing.Color.Transparent;
            this.btnEye2.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnEye2.FlatAppearance.BorderSize         = 0;
            this.btnEye2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEye2.Font                              = new System.Drawing.Font("Segoe UI Emoji", 12F);
            this.btnEye2.ForeColor                         = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnEye2.Location                          = new System.Drawing.Point(314, 6);
            this.btnEye2.Name                              = "btnEye2";
            this.btnEye2.Size                              = new System.Drawing.Size(30, 28);
            this.btnEye2.TabStop                           = false;
            this.btnEye2.Text                              = "👁";
            this.btnEye2.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnEye2.Click                            += new System.EventHandler(this.btnEye2_Click);

            // ── Xác nhận mật khẩu mới ────────────────────────────
            this.lblConfirm.AutoSize  = true;
            this.lblConfirm.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblConfirm.Location  = new System.Drawing.Point(24, 172);
            this.lblConfirm.Name      = "lblConfirm";
            this.lblConfirm.Text      = "XÁC NHẬN MẬT KHẨU MỚI";

            this.panelConfirm.BackColor   = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelConfirm.Location    = new System.Drawing.Point(24, 192);
            this.panelConfirm.Name        = "panelConfirm";
            this.panelConfirm.Size        = new System.Drawing.Size(352, 40);
            this.panelConfirm.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtConfirm, this.btnEye3 });

            this.txtConfirm.BackColor             = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtConfirm.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.txtConfirm.Font                  = new System.Drawing.Font("Segoe UI", 11F);
            this.txtConfirm.Location              = new System.Drawing.Point(10, 9);
            this.txtConfirm.Name                  = "txtConfirm";
            this.txtConfirm.PlaceholderText       = "Nhập lại mật khẩu mới...";
            this.txtConfirm.Size                  = new System.Drawing.Size(294, 22);
            this.txtConfirm.UseSystemPasswordChar = true;

            this.btnEye3.BackColor                         = System.Drawing.Color.Transparent;
            this.btnEye3.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnEye3.FlatAppearance.BorderSize         = 0;
            this.btnEye3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEye3.Font                              = new System.Drawing.Font("Segoe UI Emoji", 12F);
            this.btnEye3.ForeColor                         = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnEye3.Location                          = new System.Drawing.Point(314, 6);
            this.btnEye3.Name                              = "btnEye3";
            this.btnEye3.Size                              = new System.Drawing.Size(30, 28);
            this.btnEye3.TabStop                           = false;
            this.btnEye3.Text                              = "👁";
            this.btnEye3.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnEye3.Click                            += new System.EventHandler(this.btnEye3_Click);

            // ── Hint (gợi ý điều kiện mật khẩu) ─────────────────
            this.lblHint.AutoSize  = false;
            this.lblHint.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHint.Location  = new System.Drawing.Point(24, 242);
            this.lblHint.Name      = "lblHint";
            this.lblHint.Size      = new System.Drawing.Size(352, 18);
            this.lblHint.Text      = "ℹ️  Mật khẩu mới phải có ít nhất 6 ký tự.";

            // ── lblError / lblSuccess ─────────────────────────────
            this.lblError.AutoSize  = false;
            this.lblError.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblError.Location  = new System.Drawing.Point(24, 268);
            this.lblError.Name      = "lblError";
            this.lblError.Size      = new System.Drawing.Size(352, 20);
            this.lblError.Text      = "";

            this.lblSuccess.AutoSize  = false;
            this.lblSuccess.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSuccess.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.lblSuccess.Location  = new System.Drawing.Point(24, 268);
            this.lblSuccess.Name      = "lblSuccess";
            this.lblSuccess.Size      = new System.Drawing.Size(352, 20);
            this.lblSuccess.Text      = "";

            // ── BUG FIX #1: btnConfirm ────────────────────────────
            // Trước đây form ClientSize=340 nhưng btnConfirm ở y=276 + height=42 = 318
            // panelHeader=72 + 318 = 390 → bị cắt mất nút!
            // Fix: tăng ClientSize lên 420 để btnConfirm và btnCancel hiện đủ
            this.btnConfirm.BackColor                         = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnConfirm.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.FlatAppearance.BorderSize         = 0;
            this.btnConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnConfirm.Font                              = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor                         = System.Drawing.Color.White;
            this.btnConfirm.Location                          = new System.Drawing.Point(24, 298);
            this.btnConfirm.Name                              = "btnConfirm";
            this.btnConfirm.Size                              = new System.Drawing.Size(236, 42);
            this.btnConfirm.Text                              = "✔  Xác nhận đổi mật khẩu";
            this.btnConfirm.Click                            += new System.EventHandler(this.btnConfirm_Click);

            this.btnCancel.BackColor                         = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnCancel.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize         = 1;
            this.btnCancel.FlatAppearance.BorderColor        = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnCancel.Font                              = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor                         = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnCancel.Location                          = new System.Drawing.Point(272, 298);
            this.btnCancel.Name                              = "btnCancel";
            this.btnCancel.Size                              = new System.Drawing.Size(104, 42);
            this.btnCancel.Text                              = "Hủy";
            this.btnCancel.Click                            += new System.EventHandler(this.btnCancel_Click);

            // ── frmChangePassword ─────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            // BUG FIX #1: ClientSize phải đủ lớn để hiện toàn bộ controls
            // panelHeader=72 + lblOldPass(y=16) + ... + btnConfirm(y=298+42=340) + padding=16 ≈ 428
            this.ClientSize          = new System.Drawing.Size(400, 428);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelHeader);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "frmChangePassword";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Đổi mật khẩu";

            this.panelHeader.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel   panelHeader;
        private System.Windows.Forms.Label   lblTitle;
        private System.Windows.Forms.Label   lblSubtitle;
        private System.Windows.Forms.Panel   panelForm;
        private System.Windows.Forms.Label   lblOldPass;
        private System.Windows.Forms.Panel   panelOld;
        private System.Windows.Forms.TextBox txtOldPass;
        private System.Windows.Forms.Button  btnEye1;
        private System.Windows.Forms.Label   lblNewPass;
        private System.Windows.Forms.Panel   panelNew;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Button  btnEye2;
        private System.Windows.Forms.Label   lblConfirm;
        private System.Windows.Forms.Panel   panelConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Button  btnEye3;
        private System.Windows.Forms.Label   lblHint;
        private System.Windows.Forms.Label   lblError;
        private System.Windows.Forms.Label   lblSuccess;
        private System.Windows.Forms.Button  btnConfirm;
        private System.Windows.Forms.Button  btnCancel;
    }
}
