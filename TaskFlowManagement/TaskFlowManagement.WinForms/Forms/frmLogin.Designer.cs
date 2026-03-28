// frmLogin.Designer.cs – CHUẨN Designer, không dùng lambda trong InitializeComponent
//
// Quy tắc Visual Studio Designer BẮT BUỘC:
//   ❌ Không được dùng: lambda (s, e) => {...}, using var, helper method call
//   ❌ Không được dùng: Paint += lambda, Enter += lambda trong InitializeComponent
//   ✅ Chỉ được dùng: property assignment, AddRange, += tên_method (method khai báo riêng)
//
// Giải pháp cho các hiệu ứng đặc biệt:
//   - Rounded input border  → khai báo panelInputUser_Paint() riêng bên dưới
//   - Gradient logo box     → khai báo panelLogoBox_Paint() riêng bên dưới
//   - Gradient button       → khai báo btnLogin_Paint() riêng bên dưới
//   - Focus border thay đổi → dùng txtUsername_Enter/Leave() trong frmLogin.cs
//   - Progress bar gradient → khai báo panelProgress_Paint() riêng bên dưới

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // ─────────────────────────────────────────────────────
        // InitializeComponent – KHÔNG có lambda, KHÔNG có logic
        // ─────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.panelLeft      = new System.Windows.Forms.Panel();
            this.panelRing1     = new System.Windows.Forms.Panel();
            this.panelRing2     = new System.Windows.Forms.Panel();
            this.panelLogoBox   = new System.Windows.Forms.Panel();
            this.lblLogoIcon    = new System.Windows.Forms.Label();
            this.lblAppName     = new System.Windows.Forms.Label();
            this.panelBadge     = new System.Windows.Forms.Panel();
            this.lblBadgeDot    = new System.Windows.Forms.Label();
            this.lblBadgeText   = new System.Windows.Forms.Label();
            this.lblTagline     = new System.Windows.Forms.Label();
            this.lblTaglineSub  = new System.Windows.Forms.Label();
            this.panelAccent    = new System.Windows.Forms.Panel();
            this.lblFeat1       = new System.Windows.Forms.Label();
            this.lblFeat2       = new System.Windows.Forms.Label();
            this.lblFeat3       = new System.Windows.Forms.Label();

            this.panelRight     = new System.Windows.Forms.Panel();
            this.lblTitle       = new System.Windows.Forms.Label();
            this.lblSubtitle    = new System.Windows.Forms.Label();
            this.panelTitleLine = new System.Windows.Forms.Panel();
            this.lblLabelUser   = new System.Windows.Forms.Label();
            this.panelInputUser = new System.Windows.Forms.Panel();
            this.lblIconUser    = new System.Windows.Forms.Label();
            this.txtUsername    = new System.Windows.Forms.TextBox();
            this.lblLabelPass   = new System.Windows.Forms.Label();
            this.panelInputPass = new System.Windows.Forms.Panel();
            this.lblIconPass    = new System.Windows.Forms.Label();
            this.txtPassword    = new System.Windows.Forms.TextBox();
            this.btnEye         = new System.Windows.Forms.Button();
            this.chkRemember    = new System.Windows.Forms.CheckBox();
            this.lblError       = new System.Windows.Forms.Label();
            this.panelProgress  = new System.Windows.Forms.Panel();
            this.btnLogin       = new System.Windows.Forms.Button();
            this.lblFooter      = new System.Windows.Forms.Label();

            this.panelLeft.SuspendLayout();
            this.panelLogoBox.SuspendLayout();
            this.panelBadge.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelInputUser.SuspendLayout();
            this.panelInputPass.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════
            // LEFT PANEL – Dark navy #0B1120
            // ══════════════════════════════════════════
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(11, 17, 32);
            this.panelLeft.Location  = new System.Drawing.Point(0, 0);
            this.panelLeft.Name      = "panelLeft";
            this.panelLeft.Size      = new System.Drawing.Size(380, 580);
            this.panelLeft.TabIndex  = 0;
            this.panelLeft.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.panelRing1, this.panelRing2,
                this.panelLogoBox, this.lblAppName,
                this.panelBadge,
                this.lblTagline, this.lblTaglineSub,
                this.panelAccent,
                this.lblFeat1, this.lblFeat2, this.lblFeat3 });

            // panelRing1 – vòng tròn trang trí lớn (góc trên phải)
            // Paint event khai báo bên dưới: panelRing1_Paint
            this.panelRing1.BackColor = System.Drawing.Color.Transparent;
            this.panelRing1.Location  = new System.Drawing.Point(240, -90);
            this.panelRing1.Name      = "panelRing1";
            this.panelRing1.Size      = new System.Drawing.Size(260, 260);
            this.panelRing1.TabIndex  = 0;
            this.panelRing1.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelRing1_Paint);

            // panelRing2 – vòng tròn trang trí nhỏ hơn
            this.panelRing2.BackColor = System.Drawing.Color.Transparent;
            this.panelRing2.Location  = new System.Drawing.Point(278, -52);
            this.panelRing2.Name      = "panelRing2";
            this.panelRing2.Size      = new System.Drawing.Size(160, 160);
            this.panelRing2.TabIndex  = 1;
            this.panelRing2.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelRing2_Paint);

            // panelLogoBox – hình vuông gradient chứa icon 📋
            // Paint event khai báo bên dưới: panelLogoBox_Paint
            this.panelLogoBox.Location  = new System.Drawing.Point(36, 52);
            this.panelLogoBox.Name      = "panelLogoBox";
            this.panelLogoBox.Size      = new System.Drawing.Size(52, 52);
            this.panelLogoBox.TabIndex  = 2;
            this.panelLogoBox.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelLogoBox_Paint);
            this.panelLogoBox.Controls.Add(this.lblLogoIcon);

            // lblLogoIcon – emoji icon bên trong logo box
            this.lblLogoIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblLogoIcon.Font      = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblLogoIcon.ForeColor = System.Drawing.Color.White;
            this.lblLogoIcon.Location  = new System.Drawing.Point(0, 0);
            this.lblLogoIcon.Name      = "lblLogoIcon";
            this.lblLogoIcon.Size      = new System.Drawing.Size(52, 52);
            this.lblLogoIcon.TabIndex  = 0;
            this.lblLogoIcon.Text      = "📋";
            this.lblLogoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblAppName – "TaskFlow" chữ trắng đậm bên phải logo
            this.lblAppName.AutoSize  = false;
            this.lblAppName.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location  = new System.Drawing.Point(100, 60);
            this.lblAppName.Name      = "lblAppName";
            this.lblAppName.Size      = new System.Drawing.Size(240, 36);
            this.lblAppName.TabIndex  = 3;
            this.lblAppName.Text      = "TaskFlow";

            // panelBadge – badge pill "v1.0 · Giai đoạn 1"
            // Paint event khai báo bên dưới: panelBadge_Paint
            this.panelBadge.BackColor = System.Drawing.Color.FromArgb(20, 37, 99, 235);
            this.panelBadge.Location  = new System.Drawing.Point(36, 120);
            this.panelBadge.Name      = "panelBadge";
            this.panelBadge.Size      = new System.Drawing.Size(175, 26);
            this.panelBadge.TabIndex  = 4;
            this.panelBadge.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelBadge_Paint);
            this.panelBadge.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBadgeDot, this.lblBadgeText });

            // lblBadgeDot – chấm teal nhỏ trong badge
            this.lblBadgeDot.BackColor = System.Drawing.Color.Transparent;
            this.lblBadgeDot.Font      = new System.Drawing.Font("Segoe UI", 16F);
            this.lblBadgeDot.ForeColor = System.Drawing.Color.FromArgb(6, 182, 212);
            this.lblBadgeDot.Location  = new System.Drawing.Point(8, -2);
            this.lblBadgeDot.Name      = "lblBadgeDot";
            this.lblBadgeDot.Size      = new System.Drawing.Size(18, 26);
            this.lblBadgeDot.TabIndex  = 0;
            this.lblBadgeDot.Text      = "•";
            this.lblBadgeDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblBadgeText – text "v1.0 · Giai đoạn 1"
            this.lblBadgeText.BackColor = System.Drawing.Color.Transparent;
            this.lblBadgeText.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBadgeText.ForeColor = System.Drawing.Color.FromArgb(99, 163, 248);
            this.lblBadgeText.Location  = new System.Drawing.Point(28, 0);
            this.lblBadgeText.Name      = "lblBadgeText";
            this.lblBadgeText.Size      = new System.Drawing.Size(143, 26);
            this.lblBadgeText.TabIndex  = 1;
            this.lblBadgeText.Text      = "v1.0  ·  2025 – 2026";
            this.lblBadgeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblTagline – "Quản lý dự án" chữ trắng lớn
            this.lblTagline.Font      = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTagline.ForeColor = System.Drawing.Color.White;
            this.lblTagline.Location  = new System.Drawing.Point(36, 164);
            this.lblTagline.Name      = "lblTagline";
            this.lblTagline.Size      = new System.Drawing.Size(310, 48);
            this.lblTagline.TabIndex  = 5;
            this.lblTagline.Text      = "Quản lý Dự án";

            // lblTaglineSub – "chuyên nghiệp" màu teal
            this.lblTaglineSub.Font      = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTaglineSub.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.lblTaglineSub.Location  = new System.Drawing.Point(36, 210);
            this.lblTaglineSub.Name      = "lblTaglineSub";
            this.lblTaglineSub.Size      = new System.Drawing.Size(310, 48);
            this.lblTaglineSub.TabIndex  = 6;
            this.lblTaglineSub.Text      = "Phần mềm";

            // panelAccent – gạch dưới màu xanh 40px
            this.panelAccent.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelAccent.Location  = new System.Drawing.Point(36, 268);
            this.panelAccent.Name      = "panelAccent";
            this.panelAccent.Size      = new System.Drawing.Size(40, 3);
            this.panelAccent.TabIndex  = 7;

            // lblFeat1/2/3 – danh sách tính năng với emoji icon
            this.lblFeat1.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeat1.ForeColor = System.Drawing.Color.FromArgb(176, 200, 230);
            this.lblFeat1.Location  = new System.Drawing.Point(36, 288);
            this.lblFeat1.Name      = "lblFeat1";
            this.lblFeat1.Size      = new System.Drawing.Size(308, 28);
            this.lblFeat1.TabIndex  = 8;
            this.lblFeat1.Text      = "📁  Quản lý dự án & khách hàng";

            this.lblFeat2.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeat2.ForeColor = System.Drawing.Color.FromArgb(176, 200, 230);
            this.lblFeat2.Location  = new System.Drawing.Point(36, 324);
            this.lblFeat2.Name      = "lblFeat2";
            this.lblFeat2.Size      = new System.Drawing.Size(308, 28);
            this.lblFeat2.TabIndex  = 9;
            this.lblFeat2.Text      = "✅  Giao việc & theo dõi tiến độ";

            this.lblFeat3.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeat3.ForeColor = System.Drawing.Color.FromArgb(176, 200, 230);
            this.lblFeat3.Location  = new System.Drawing.Point(36, 360);
            this.lblFeat3.Name      = "lblFeat3";
            this.lblFeat3.Size      = new System.Drawing.Size(308, 28);
            this.lblFeat3.TabIndex  = 10;
            this.lblFeat3.Text      = "🔒  Phân quyền linh hoạt 3 vai trò";

            // ══════════════════════════════════════════
            // RIGHT PANEL – White form area
            // ══════════════════════════════════════════
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Location  = new System.Drawing.Point(380, 0);
            this.panelRight.Name      = "panelRight";
            this.panelRight.Size      = new System.Drawing.Size(500, 580);
            this.panelRight.TabIndex  = 1;
            this.panelRight.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblSubtitle, this.panelTitleLine,
                this.lblLabelUser, this.panelInputUser,
                this.lblLabelPass, this.panelInputPass,
                this.chkRemember, this.lblError,
                this.panelProgress, this.btnLogin, this.lblFooter });

            // lblTitle – "Đăng nhập" heading
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(11, 17, 32);
            this.lblTitle.Location  = new System.Drawing.Point(52, 68);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.Size      = new System.Drawing.Size(396, 46);
            this.lblTitle.TabIndex  = 0;
            this.lblTitle.Text      = "Đăng nhập";

            // lblSubtitle – mô tả nhỏ bên dưới heading
            this.lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location  = new System.Drawing.Point(52, 116);
            this.lblSubtitle.Name      = "lblSubtitle";
            this.lblSubtitle.Size      = new System.Drawing.Size(396, 22);
            this.lblSubtitle.TabIndex  = 1;
            this.lblSubtitle.Text      = "Nhập thông tin tài khoản để tiếp tục";

            // panelTitleLine – gạch dưới xanh 48px
            this.panelTitleLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelTitleLine.Location  = new System.Drawing.Point(52, 144);
            this.panelTitleLine.Name      = "panelTitleLine";
            this.panelTitleLine.Size      = new System.Drawing.Size(48, 3);
            this.panelTitleLine.TabIndex  = 2;

            // lblLabelUser – label "TÊN ĐĂNG NHẬP"
            this.lblLabelUser.AutoSize  = true;
            this.lblLabelUser.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLabelUser.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblLabelUser.Location  = new System.Drawing.Point(52, 166);
            this.lblLabelUser.Name      = "lblLabelUser";
            this.lblLabelUser.TabIndex  = 3;
            this.lblLabelUser.Text      = "TÊN ĐĂNG NHẬP";

            // panelInputUser – wrapper bao ngoài input username
            // Paint event: panelInputUser_Paint (vẽ border bo góc)
            // Enter/Leave của txtUsername sẽ gọi panelInputUser.Invalidate() từ frmLogin.cs
            this.panelInputUser.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelInputUser.Location  = new System.Drawing.Point(52, 188);
            this.panelInputUser.Name      = "panelInputUser";
            this.panelInputUser.Size      = new System.Drawing.Size(396, 46);
            this.panelInputUser.TabIndex  = 4;
            this.panelInputUser.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelInput_Paint);
            this.panelInputUser.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconUser, this.txtUsername });

            // lblIconUser – icon 👤 bên trái input
            this.lblIconUser.BackColor = System.Drawing.Color.Transparent;
            this.lblIconUser.Font      = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblIconUser.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblIconUser.Location  = new System.Drawing.Point(10, 8);
            this.lblIconUser.Name      = "lblIconUser";
            this.lblIconUser.Size      = new System.Drawing.Size(30, 30);
            this.lblIconUser.TabIndex  = 0;
            this.lblIconUser.Text      = "👤";
            this.lblIconUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtUsername – không có border riêng (border do panelInputUser vẽ)
            this.txtUsername.BackColor       = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtUsername.BorderStyle     = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font            = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsername.ForeColor       = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtUsername.Location        = new System.Drawing.Point(46, 12);
            this.txtUsername.Name            = "txtUsername";
            this.txtUsername.PlaceholderText = "Nhập tên đăng nhập...";
            this.txtUsername.Size            = new System.Drawing.Size(340, 24);
            this.txtUsername.TabIndex        = 1;
            // Enter/Leave xử lý trong frmLogin.cs để gọi panelInputUser.Invalidate()
            this.txtUsername.Enter          += new System.EventHandler(this.txtUsername_EnterLeave);
            this.txtUsername.Leave          += new System.EventHandler(this.txtUsername_EnterLeave);

            // lblLabelPass – label "MẬT KHẨU"
            this.lblLabelPass.AutoSize  = true;
            this.lblLabelPass.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLabelPass.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblLabelPass.Location  = new System.Drawing.Point(52, 250);
            this.lblLabelPass.Name      = "lblLabelPass";
            this.lblLabelPass.TabIndex  = 5;
            this.lblLabelPass.Text      = "MẬT KHẨU";

            // panelInputPass – wrapper bao ngoài input password
            this.panelInputPass.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelInputPass.Location  = new System.Drawing.Point(52, 272);
            this.panelInputPass.Name      = "panelInputPass";
            this.panelInputPass.Size      = new System.Drawing.Size(396, 46);
            this.panelInputPass.TabIndex  = 6;
            this.panelInputPass.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelInput_Paint);
            this.panelInputPass.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconPass, this.txtPassword, this.btnEye });

            // lblIconPass – icon 🔑 bên trái input
            this.lblIconPass.BackColor = System.Drawing.Color.Transparent;
            this.lblIconPass.Font      = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblIconPass.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblIconPass.Location  = new System.Drawing.Point(10, 8);
            this.lblIconPass.Name      = "lblIconPass";
            this.lblIconPass.Size      = new System.Drawing.Size(30, 30);
            this.lblIconPass.TabIndex  = 0;
            this.lblIconPass.Text      = "🔑";
            this.lblIconPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtPassword – ô nhập mật khẩu
            this.txtPassword.BackColor             = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtPassword.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font                  = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.ForeColor             = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtPassword.Location              = new System.Drawing.Point(46, 12);
            this.txtPassword.Name                  = "txtPassword";
            this.txtPassword.PlaceholderText       = "Nhập mật khẩu...";
            this.txtPassword.Size                  = new System.Drawing.Size(304, 24);
            this.txtPassword.TabIndex              = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown              += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.Enter                += new System.EventHandler(this.txtPassword_EnterLeave);
            this.txtPassword.Leave                += new System.EventHandler(this.txtPassword_EnterLeave);

            // btnEye – nút 👁 toggle hiện/ẩn mật khẩu
            this.btnEye.BackColor                         = System.Drawing.Color.Transparent;
            this.btnEye.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnEye.FlatAppearance.BorderSize         = 0;
            this.btnEye.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEye.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEye.Font                              = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.btnEye.ForeColor                         = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnEye.Location                          = new System.Drawing.Point(356, 8);
            this.btnEye.Name                              = "btnEye";
            this.btnEye.Size                              = new System.Drawing.Size(34, 30);
            this.btnEye.TabIndex                          = 2;
            this.btnEye.TabStop                           = false;
            this.btnEye.Text                              = "👁";
            this.btnEye.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnEye.UseVisualStyleBackColor           = false;

            // chkRemember – "Nhớ tên đăng nhập"
            this.chkRemember.AutoSize  = true;
            this.chkRemember.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkRemember.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.chkRemember.Location  = new System.Drawing.Point(52, 334);
            this.chkRemember.Name      = "chkRemember";
            this.chkRemember.TabIndex  = 7;
            this.chkRemember.Text      = "Nhớ tên đăng nhập";

            // lblError – thông báo lỗi đăng nhập (màu đỏ)
            this.lblError.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblError.Location  = new System.Drawing.Point(52, 366);
            this.lblError.Name      = "lblError";
            this.lblError.Size      = new System.Drawing.Size(396, 22);
            this.lblError.TabIndex  = 8;
            this.lblError.Text      = "";

            // panelProgress – loading bar gradient (ẩn lúc đầu, hiện khi đang login)
            // Width tăng dần qua Timer trong frmLogin.cs, Paint vẽ gradient
            this.panelProgress.BackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.panelProgress.Location  = new System.Drawing.Point(52, 393);
            this.panelProgress.Name      = "panelProgress";
            this.panelProgress.Size      = new System.Drawing.Size(0, 3);
            this.panelProgress.TabIndex  = 9;
            this.panelProgress.Visible   = false;
            this.panelProgress.Paint    += new System.Windows.Forms.PaintEventHandler(this.panelProgress_Paint);

            // btnLogin – nút đăng nhập, Paint vẽ gradient + bo góc
            this.btnLogin.BackColor                         = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnLogin.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize         = 0;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnLogin.Font                              = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor                         = System.Drawing.Color.White;
            this.btnLogin.Location                          = new System.Drawing.Point(52, 406);
            this.btnLogin.Name                              = "btnLogin";
            this.btnLogin.Size                              = new System.Drawing.Size(396, 52);
            this.btnLogin.TabIndex                          = 10;
            this.btnLogin.Text                              = "ĐĂNG NHẬP  →";
            this.btnLogin.UseVisualStyleBackColor           = false;
            this.btnLogin.Click                            += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.Paint                            += new System.Windows.Forms.PaintEventHandler(this.btnLogin_Paint);

            // lblFooter – dòng thông tin cuối form
            this.lblFooter.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.lblFooter.Location  = new System.Drawing.Point(0, 548);
            this.lblFooter.Name      = "lblFooter";
            this.lblFooter.Size      = new System.Drawing.Size(500, 22);
            this.lblFooter.TabIndex  = 11;
            this.lblFooter.Text      = "Hệ thống Quản lý Dự án Phần mềm  ·  2025 – 2026";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ══════════════════════════════════════════
            // FORM – frmLogin
            // ══════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(11, 17, 32);
            this.ClientSize          = new System.Drawing.Size(880, 580);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "frmLogin";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "TaskFlow – Quản lý Dự án Phần mềm";

            this.panelInputUser.ResumeLayout(false);
            this.panelInputPass.ResumeLayout(false);
            this.panelLogoBox.ResumeLayout(false);
            this.panelBadge.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ─────────────────────────────────────────────────────
        // PAINT EVENTS – khai báo riêng ngoài InitializeComponent
        // Designer chỉ cần thấy tên method, không thấy logic bên trong
        // ─────────────────────────────────────────────────────

        // Vẽ vòng tròn ring 1 (màu xanh navy nhạt)
        private void panelRing1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(30, 37, 99, 235), 1f);
            e.Graphics.DrawEllipse(pen, 0, 0, panelRing1.Width - 1, panelRing1.Height - 1);
        }

        // Vẽ vòng tròn ring 2 (màu teal nhạt)
        private void panelRing2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(25, 6, 182, 212), 1f);
            e.Graphics.DrawEllipse(pen, 0, 0, panelRing2.Width - 1, panelRing2.Height - 1);
        }

        // Vẽ logo box: nền gradient xanh + bo góc 12px
        private void panelLogoBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new System.Drawing.Rectangle(0, 0, panelLogoBox.Width, panelLogoBox.Height);
            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                rect,
                System.Drawing.Color.FromArgb(37, 99, 235),
                System.Drawing.Color.FromArgb(6, 182, 212),
                135f);
            using var path = CreateRoundedPath(rect, 12);
            e.Graphics.FillPath(brush, path);
        }

        // Vẽ badge pill: viền bo góc màu xanh nhạt
        private void panelBadge_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new System.Drawing.Rectangle(0, 0, panelBadge.Width - 1, panelBadge.Height - 1);
            using var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(60, 59, 130, 246), 1f);
            using var path = CreateRoundedPath(rect, 13);
            e.Graphics.DrawPath(pen, path);
        }

        // Vẽ border bo góc cho input wrapper (dùng chung cho cả 2 input)
        // Border xanh đậm khi control bên trong đang focused, xám nhạt khi không focus
        private void panelInput_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var panel   = (System.Windows.Forms.Panel)sender;
            bool focused = panel.ContainsFocus; // true khi TextBox bên trong đang focus
            var color = focused
                ? System.Drawing.Color.FromArgb(37, 99, 235)   // Xanh khi focus
                : System.Drawing.Color.FromArgb(226, 232, 240); // Xám khi bình thường
            float width = focused ? 2f : 1.5f;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new System.Drawing.Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using var pen  = new System.Drawing.Pen(color, width);
            using var path = CreateRoundedPath(rect, 10);
            e.Graphics.DrawPath(pen, path);
        }

        // Vẽ loading bar gradient (xanh → teal)
        private void panelProgress_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (panelProgress.Width <= 0) return;
            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new System.Drawing.Rectangle(0, 0, panelProgress.Width, panelProgress.Height),
                System.Drawing.Color.FromArgb(37, 99, 235),
                System.Drawing.Color.FromArgb(6, 182, 212),
                0f);
            e.Graphics.FillRectangle(brush, 0, 0, panelProgress.Width, panelProgress.Height);
        }

        // Vẽ nút Login: gradient xanh + bo góc 10px + text căn giữa
        private void btnLogin_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                btnLogin.ClientRectangle,
                System.Drawing.Color.FromArgb(37, 99, 235),
                System.Drawing.Color.FromArgb(29, 78, 216),
                90f);
            using var path = CreateRoundedPath(btnLogin.ClientRectangle, 10);
            e.Graphics.FillPath(brush, path);

            using var sf = new System.Drawing.StringFormat
            {
                Alignment     = System.Drawing.StringAlignment.Center,
                LineAlignment = System.Drawing.StringAlignment.Center
            };
            e.Graphics.DrawString(
                btnLogin.Text, btnLogin.Font,
                System.Drawing.Brushes.White,
                btnLogin.ClientRectangle, sf);
        }

        // ─────────────────────────────────────────────────────
        // HELPER – tạo GraphicsPath hình chữ nhật bo góc
        // Dùng bởi tất cả Paint events bên trên
        // ─────────────────────────────────────────────────────
        private static System.Drawing.Drawing2D.GraphicsPath CreateRoundedPath(
            System.Drawing.Rectangle bounds, int radius)
        {
            int d    = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X,                        bounds.Y,                        d, d, 180, 90);
            path.AddArc(bounds.X + bounds.Width - d,     bounds.Y,                        d, d, 270, 90);
            path.AddArc(bounds.X + bounds.Width - d,     bounds.Y + bounds.Height - d,    d, d,   0, 90);
            path.AddArc(bounds.X,                        bounds.Y + bounds.Height - d,    d, d,  90, 90);
            path.CloseFigure();
            return path;
        }

        // ── Control declarations ──
        private System.Windows.Forms.Panel    panelLeft;
        private System.Windows.Forms.Panel    panelRing1;
        private System.Windows.Forms.Panel    panelRing2;
        private System.Windows.Forms.Panel    panelLogoBox;
        private System.Windows.Forms.Label    lblLogoIcon;
        private System.Windows.Forms.Label    lblAppName;
        private System.Windows.Forms.Panel    panelBadge;
        private System.Windows.Forms.Label    lblBadgeDot;
        private System.Windows.Forms.Label    lblBadgeText;
        private System.Windows.Forms.Label    lblTagline;
        private System.Windows.Forms.Label    lblTaglineSub;
        private System.Windows.Forms.Panel    panelAccent;
        private System.Windows.Forms.Label    lblFeat1;
        private System.Windows.Forms.Label    lblFeat2;
        private System.Windows.Forms.Label    lblFeat3;
        private System.Windows.Forms.Panel    panelRight;
        private System.Windows.Forms.Label    lblTitle;
        private System.Windows.Forms.Label    lblSubtitle;
        private System.Windows.Forms.Panel    panelTitleLine;
        private System.Windows.Forms.Label    lblLabelUser;
        private System.Windows.Forms.Panel    panelInputUser;
        private System.Windows.Forms.Label    lblIconUser;
        private System.Windows.Forms.TextBox  txtUsername;
        private System.Windows.Forms.Label    lblLabelPass;
        private System.Windows.Forms.Panel    panelInputPass;
        private System.Windows.Forms.Label    lblIconPass;
        private System.Windows.Forms.TextBox  txtPassword;
        private System.Windows.Forms.Button   btnEye;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label    lblError;
        private System.Windows.Forms.Panel    panelProgress;
        private System.Windows.Forms.Button   btnLogin;
        private System.Windows.Forms.Label    lblFooter;
    }
}
