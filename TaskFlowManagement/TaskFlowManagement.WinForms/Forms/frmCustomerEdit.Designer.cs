using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmCustomerEdit
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
            this.lblCompany      = new System.Windows.Forms.Label();
            this.txtCompany      = new System.Windows.Forms.TextBox();
            this.lblContact      = new System.Windows.Forms.Label();
            this.txtContact      = new System.Windows.Forms.TextBox();
            this.lblEmail        = new System.Windows.Forms.Label();
            this.txtEmail        = new System.Windows.Forms.TextBox();
            this.lblPhone        = new System.Windows.Forms.Label();
            this.txtPhone        = new System.Windows.Forms.TextBox();
            this.lblAddress      = new System.Windows.Forms.Label();
            this.txtAddress      = new System.Windows.Forms.TextBox();
            this.panelFooter     = new System.Windows.Forms.Panel();
            this.panelFooterLine = new System.Windows.Forms.Panel();
            this.lblError        = new System.Windows.Forms.Label();
            this.btnSave         = new System.Windows.Forms.Button();
            this.btnCancel       = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ─────────────────────────────────────
            // FIX: Thêm AccentLine cho nhất quán với frmUserEdit
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
            this.lblTitleForm.Text      = "➕  Thêm khách hàng mới";
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // FIX: AccentLine xanh dưới header – nhất quán design
            this.panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelAccentLine.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelAccentLine.Height    = 4;
            this.panelAccentLine.Name      = "panelAccentLine";

            // ── panelBody ────────────────────────────────────────
            // FIX: Background nhất quán với frmUserEdit (xám nhạt thay vì trắng)
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelBody.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Name      = "panelBody";
            this.panelBody.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblCompany, this.txtCompany,
                this.lblContact, this.txtContact,
                this.lblEmail,   this.txtEmail,
                this.lblPhone,   this.txtPhone,
                this.lblAddress, this.txtAddress,
            });

            // ── lblCompany ───────────────────────────────────────
            this.lblCompany.AutoSize  = true;
            this.lblCompany.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCompany.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCompany.Location  = new System.Drawing.Point(20, 16);
            this.lblCompany.Name      = "lblCompany";
            this.lblCompany.Text      = "TÊN CÔNG TY *";

            this.txtCompany.BackColor       = System.Drawing.Color.White;
            this.txtCompany.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompany.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCompany.Location        = new System.Drawing.Point(20, 36);
            this.txtCompany.Name            = "txtCompany";
            this.txtCompany.PlaceholderText = "VD: FPT Software";
            this.txtCompany.Size            = new System.Drawing.Size(360, 30);

            // ── lblContact ───────────────────────────────────────
            this.lblContact.AutoSize  = true;
            this.lblContact.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblContact.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblContact.Location  = new System.Drawing.Point(20, 82);
            this.lblContact.Name      = "lblContact";
            this.lblContact.Text      = "NGƯỜI LIÊN HỆ";

            this.txtContact.BackColor       = System.Drawing.Color.White;
            this.txtContact.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContact.Location        = new System.Drawing.Point(20, 102);
            this.txtContact.Name            = "txtContact";
            this.txtContact.PlaceholderText = "Họ tên người liên hệ";
            this.txtContact.Size            = new System.Drawing.Size(360, 30);

            // ── lblEmail ─────────────────────────────────────────
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
            this.txtEmail.PlaceholderText = "contact@company.com";
            this.txtEmail.Size            = new System.Drawing.Size(360, 30);

            // ── lblPhone ─────────────────────────────────────────
            this.lblPhone.AutoSize  = true;
            this.lblPhone.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPhone.Location  = new System.Drawing.Point(20, 214);
            this.lblPhone.Name      = "lblPhone";
            this.lblPhone.Text      = "ĐIỆN THOẠI";

            this.txtPhone.BackColor       = System.Drawing.Color.White;
            this.txtPhone.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location        = new System.Drawing.Point(20, 234);
            this.txtPhone.Name            = "txtPhone";
            this.txtPhone.PlaceholderText = "028 1234 5678";
            this.txtPhone.Size            = new System.Drawing.Size(360, 30);

            // ── lblAddress ───────────────────────────────────────
            this.lblAddress.AutoSize  = true;
            this.lblAddress.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAddress.Location  = new System.Drawing.Point(20, 280);
            this.lblAddress.Name      = "lblAddress";
            this.lblAddress.Text      = "ĐỊA CHỈ";

            this.txtAddress.BackColor       = System.Drawing.Color.White;
            this.txtAddress.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font            = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Location        = new System.Drawing.Point(20, 300);
            this.txtAddress.Multiline       = true;
            this.txtAddress.Name            = "txtAddress";
            this.txtAddress.PlaceholderText = "Số nhà, đường, quận/huyện, tỉnh/thành phố...";
            this.txtAddress.Size            = new System.Drawing.Size(360, 52);

            // ── panelFooter (nhất quán với frmUserEdit) ──────────
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

            // lblError
            this.lblError.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblError.Location  = new System.Drawing.Point(16, 6);
            this.lblError.Name      = "lblError";
            this.lblError.Size      = new System.Drawing.Size(370, 18);
            this.lblError.Text      = "";

            // btnSave
            UIHelper.StyleButton(this.btnSave, UIHelper.ButtonVariant.Primary);
            this.btnSave.Location = new System.Drawing.Point(16, 28);
            this.btnSave.Size = new System.Drawing.Size(270, 40);
            this.btnSave.Text = "💾  Lưu";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Click += btnSave_Click;

            // btnCancel
            UIHelper.StyleButton(this.btnCancel, UIHelper.ButtonVariant.Secondary);
            this.btnCancel.Location = new System.Drawing.Point(296, 28);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += btnCancel_Click;

            // ── frmCustomerEdit ──────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(400, 510);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "frmCustomerEdit";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Khách hàng";

            this.panelHeader.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel   panelHeader;
        private System.Windows.Forms.Panel   panelAccentLine;
        private System.Windows.Forms.Label   lblTitleForm;
        private System.Windows.Forms.Panel   panelBody;
        private System.Windows.Forms.Label   lblCompany;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label   lblContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label   lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label   lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label   lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Panel   panelFooter;
        private System.Windows.Forms.Panel   panelFooterLine;
        private System.Windows.Forms.Label   lblError;
        private System.Windows.Forms.Button  btnSave;
        private System.Windows.Forms.Button  btnCancel;
    }
}
