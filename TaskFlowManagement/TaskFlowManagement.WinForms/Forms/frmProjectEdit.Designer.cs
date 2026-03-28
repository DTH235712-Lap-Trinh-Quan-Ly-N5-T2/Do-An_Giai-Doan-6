using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectEdit   // BaseForm declared in frmProjectEdit.cs
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
            lblTitleForm = new Label();
            panelAccentLine = new Panel();
            panelBody = new Panel();
            panelFooter = new Panel();
            panelFooterLine = new Panel();
            lblError = new Label();
            btnSave = new Button();
            btnCancel = new Button();

            lblName = new Label(); txtName = new TextBox();
            lblCustomer = new Label(); cboCustomer = new ComboBox();
            lblOwner = new Label(); cboOwner = new ComboBox();
            lblStart = new Label(); dtpStartDate = new DateTimePicker();
            lblDeadline = new Label(); dtpDeadline = new DateTimePicker();
            chkDeadline = new CheckBox();
            lblBudget = new Label(); txtBudget = new TextBox();
            lblPriority = new Label(); cboPriority = new ComboBox();
            lblDesc = new Label(); txtDescription = new TextBox();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            panelFooter.SuspendLayout();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelHeader — Dark banner
            // ════════════════════════════════════════════════════
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 64;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblTitleForm);
            panelHeader.Controls.Add(panelAccentLine);

            lblTitleForm.AutoSize = false;
            lblTitleForm.Dock = DockStyle.Fill;
            lblTitleForm.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblTitleForm.ForeColor = UIHelper.ColorHeaderFg;
            lblTitleForm.Name = "lblTitleForm";
            lblTitleForm.Padding = new Padding(18, 0, 0, 4);
            lblTitleForm.Text = "➕  Tạo dự án mới";
            lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            // ════════════════════════════════════════════════════
            // panelBody — Form fields
            // ════════════════════════════════════════════════════
            panelBody.BackColor = UIHelper.ColorBackground;
            panelBody.Dock = DockStyle.Fill;
            panelBody.AutoScroll = true;
            panelBody.Name = "panelBody";
            panelBody.Controls.AddRange(new Control[]
            {
                lblName, txtName, lblCustomer, cboCustomer, lblOwner, cboOwner,
                lblStart, dtpStartDate, lblDeadline, chkDeadline, dtpDeadline,
                lblBudget, txtBudget, lblPriority, cboPriority,
                lblDesc, txtDescription
            });

            // ── Layout constants ──────────────────────────────────────────────
            int y = 14, gap = 56, lx = 20, tw = 400;

            // ── Tên dự án ─────────────────────────────────────────────────────
            SetLabel(lblName, "TÊN DỰ ÁN *", lx, y);
            SetTextBox(txtName, "Nhập tên dự án...", lx, y + 18, tw, 1);

            // ── Khách hàng ────────────────────────────────────────────────────
            y += gap;
            SetLabel(lblCustomer, "KHÁCH HÀNG", lx, y);
            StyleCombo(cboCustomer, lx, y + 18, tw, 2);

            // ── Quản lý (PM) ──────────────────────────────────────────────────
            y += gap;
            SetLabel(lblOwner, "QUẢN LÝ DỰ ÁN (PM) *", lx, y);
            StyleCombo(cboOwner, lx, y + 18, tw, 3);

            // ── Ngày bắt đầu + Deadline (cùng hàng) ──────────────────────────
            y += gap;
            SetLabel(lblStart, "NGÀY BẮT ĐẦU", lx, y);
            dtpStartDate.Font = UIHelper.FontBase;
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new System.Drawing.Point(lx, y + 18);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new System.Drawing.Size(190, 30);
            dtpStartDate.TabIndex = 4;

            SetLabel(lblDeadline, "DEADLINE", lx + 210, y);

            chkDeadline.AutoSize = true;
            chkDeadline.Font = UIHelper.FontSmall;
            chkDeadline.ForeColor = UIHelper.ColorMuted;
            chkDeadline.Location = new System.Drawing.Point(lx + 290, y + 1);
            chkDeadline.Name = "chkDeadline";
            chkDeadline.Text = "Có deadline";
            chkDeadline.CheckedChanged += chkDeadline_CheckedChanged;

            dtpDeadline.Font = UIHelper.FontBase;
            dtpDeadline.Format = DateTimePickerFormat.Short;
            dtpDeadline.Location = new System.Drawing.Point(lx + 210, y + 18);
            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Size = new System.Drawing.Size(190, 30);
            dtpDeadline.TabIndex = 5;
            dtpDeadline.Enabled = false;

            // ── Ngân sách + Priority (cùng hàng) ─────────────────────────────
            y += gap;
            SetLabel(lblBudget, "NGÂN SÁCH (VNĐ)", lx, y);
            SetTextBox(txtBudget, "VD: 500000000", lx, y + 18, 190, 6);

            SetLabel(lblPriority, "ĐỘ ƯU TIÊN", lx + 210, y);
            StyleCombo(cboPriority, lx + 210, y + 18, 190, 7);

            // ── Mô tả ─────────────────────────────────────────────────────────
            y += gap;
            SetLabel(lblDesc, "MÔ TẢ (tùy chọn)", lx, y);

            txtDescription.BackColor = System.Drawing.Color.White;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = UIHelper.FontBase;
            txtDescription.Location = new System.Drawing.Point(lx, y + 18);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Mô tả chi tiết dự án...";
            txtDescription.Size = new System.Drawing.Size(tw, 60);
            txtDescription.TabIndex = 8;

            // ════════════════════════════════════════════════════
            // panelFooter — Buttons
            // ════════════════════════════════════════════════════
            panelFooter.BackColor = System.Drawing.Color.White;
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 76;
            panelFooter.Name = "panelFooter";
            panelFooter.Controls.AddRange(new Control[]
            { panelFooterLine, lblError, btnSave, btnCancel });

            panelFooterLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            panelFooterLine.Dock = DockStyle.Top;
            panelFooterLine.Height = 1;
            panelFooterLine.Name = "panelFooterLine";

            // lblError
            lblError.Font = UIHelper.FontSmall;
            lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblError.Location = new System.Drawing.Point(16, 6);
            lblError.Name = "lblError";
            lblError.Size = new System.Drawing.Size(420, 18);

            // btnSave — Primary (SỬA: trước hard-code BackColor thủ công)
            UIHelper.StyleButton(btnSave, UIHelper.ButtonVariant.Primary);
            btnSave.Location = new System.Drawing.Point(16, 28);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(270, 40);
            btnSave.Text = "💾  Lưu dự án";
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.Click += btnSave_Click;

            // btnCancel — Secondary (SỬA: trước hard-code BackColor thủ công)
            UIHelper.StyleButton(btnCancel, UIHelper.ButtonVariant.Secondary);
            btnCancel.Location = new System.Drawing.Point(296, 28);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(130, 40);
            btnCancel.Text = "Hủy";
            btnCancel.Font = UIHelper.FontBase;
            btnCancel.Click += btnCancel_Click;

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 590);
            this.Font = UIHelper.FontBase;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectEdit";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Dự án";

            // Thứ tự Add: Fill → Bottom → Top
            this.Controls.Add(panelBody);
            this.Controls.Add(panelFooter);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelBody.PerformLayout();
            panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Private layout helpers ────────────────────────────────────────────

        /// <summary>Tạo Label field chuẩn — dùng UIHelper thay vì hard-code.</summary>
        private static void SetLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = UIHelper.ColorDark;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }

        /// <summary>Tạo TextBox field chuẩn — dùng UIHelper thay vì hard-code.</summary>
        private static void SetTextBox(TextBox txt, string placeholder, int x, int y, int w, int tabIdx)
        {
            txt.BackColor = System.Drawing.Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = UIHelper.FontBase;
            txt.Location = new System.Drawing.Point(x, y);
            txt.PlaceholderText = placeholder;
            txt.Size = new System.Drawing.Size(w, 30);
            txt.TabIndex = tabIdx;
        }

        /// <summary>Tạo ComboBox field chuẩn.</summary>
        private static void StyleCombo(ComboBox cbo, int x, int y, int w, int tabIdx)
        {
            cbo.BackColor = System.Drawing.Color.White;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.Font = UIHelper.FontBase;
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Size = new System.Drawing.Size(w, 30);
            cbo.TabIndex = tabIdx;
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader, panelAccentLine, panelBody, panelFooter, panelFooterLine;
        private Label lblTitleForm, lblError;
        private Label lblName, lblCustomer, lblOwner, lblStart, lblDeadline;
        private Label lblBudget, lblPriority, lblDesc;
        private TextBox txtName, txtBudget, txtDescription;
        private ComboBox cboCustomer, cboOwner, cboPriority;
        private DateTimePicker dtpStartDate, dtpDeadline;
        private CheckBox chkDeadline;
        private Button btnSave, btnCancel;
    }
}