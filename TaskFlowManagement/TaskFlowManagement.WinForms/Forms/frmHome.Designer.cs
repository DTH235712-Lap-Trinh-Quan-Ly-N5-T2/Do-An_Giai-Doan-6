using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmHome   // BaseForm declared in frmHome.cs
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

            panelBody = new Panel();
            panelWelcome = new Panel();
            lblGreeting = new Label();
            lblRole = new Label();
            lblLastLogin = new Label();

            panelStats = new Panel();
            flowCards = new FlowLayoutPanel();
            lblNote = new Label();

            // Card 1 – Dự án đang chạy
            panelCard1 = new Panel();
            panelCard1Top = new Panel();
            lblCard1Icon = new Label();
            lblCard1Title = new Label();
            lblStatProjects = new Label();
            lblCard1Sub = new Label();

            // Card 2 – Công việc của tôi
            panelCard2 = new Panel();
            panelCard2Top = new Panel();
            lblCard2Icon = new Label();
            lblCard2Title = new Label();
            lblStatTasks = new Label();
            lblCard2Sub = new Label();

            // Card 3 – Quá hạn
            panelCard3 = new Panel();
            panelCard3Top = new Panel();
            lblCard3Icon = new Label();
            lblCard3Title = new Label();
            lblStatOverdue = new Label();
            lblCard3Sub = new Label();

            // Card 4 – Hoàn thành tháng này
            panelCard4 = new Panel();
            panelCard4Top = new Panel();
            lblCard4Icon = new Label();
            lblCard4Title = new Label();
            lblStatDone = new Label();
            lblCard4Sub = new Label();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            panelWelcome.SuspendLayout();
            panelStats.SuspendLayout();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelHeader — Dark banner
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

            // lblHeader — tiêu đề sẽ được cập nhật trong LoadWelcomeInfo()
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(18, 0, 0, 4);
            lblHeader.Text = "🏠  Trang chủ";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // panelBody — Container toàn bộ nội dung bên dưới header
            // ════════════════════════════════════════════════════
            panelBody.BackColor = UIHelper.ColorBackground;
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Controls.Add(panelStats);
            panelBody.Controls.Add(panelWelcome);

            // ════════════════════════════════════════════════════
            // panelWelcome — Dòng chào + thông tin user (Dock Top trong panelBody)
            // ════════════════════════════════════════════════════
            panelWelcome.BackColor = UIHelper.ColorHeaderBg;
            panelWelcome.Dock = DockStyle.Top;
            panelWelcome.Height = 98;
            panelWelcome.Name = "panelWelcome";
            panelWelcome.Padding = new Padding(24, 12, 24, 0);
            panelWelcome.Controls.AddRange(new Control[]
            { lblGreeting, lblRole, lblLastLogin });

            lblGreeting.AutoSize = false;
            lblGreeting.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblGreeting.ForeColor = System.Drawing.Color.White;
            lblGreeting.Location = new Point(24, 12);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new System.Drawing.Size(900, 38);
            lblGreeting.Text = "Chào buổi sáng, ...! 👋";

            lblRole.AutoSize = false;
            lblRole.Font = UIHelper.FontBase;
            lblRole.ForeColor = UIHelper.ColorSubtitle;
            lblRole.Location = new Point(26, 54);
            lblRole.Name = "lblRole";
            lblRole.Size = new System.Drawing.Size(500, 20);
            lblRole.Text = "Vai trò: ...";

            lblLastLogin.AutoSize = false;
            lblLastLogin.Font = UIHelper.FontSmall;
            lblLastLogin.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblLastLogin.Location = new Point(26, 76);
            lblLastLogin.Name = "lblLastLogin";
            lblLastLogin.Size = new System.Drawing.Size(500, 18);
            lblLastLogin.Text = "";

            // ════════════════════════════════════════════════════
            // panelStats — container cards + lblNote (Dock Fill trong panelBody)
            // ════════════════════════════════════════════════════
            panelStats.BackColor = UIHelper.ColorBackground;
            panelStats.Dock = DockStyle.Fill;
            panelStats.Name = "panelStats";
            panelStats.Padding = new Padding(20, 16, 20, 8);
            panelStats.Controls.Add(lblNote);
            panelStats.Controls.Add(flowCards);

            // flowCards
            flowCards.AutoSize = true;
            flowCards.BackColor = System.Drawing.Color.Transparent;
            flowCards.Dock = DockStyle.Top;
            flowCards.Name = "flowCards";
            flowCards.Padding = new Padding(0, 0, 0, 0);
            flowCards.WrapContents = true;
            flowCards.Controls.AddRange(new Control[]
            { panelCard1, panelCard2, panelCard3, panelCard4 });

            // lblNote
            lblNote.AutoSize = false;
            lblNote.Font = UIHelper.FontSmall;
            lblNote.ForeColor = UIHelper.ColorMuted;
            lblNote.Dock = DockStyle.Bottom;
            lblNote.Name = "lblNote";
            lblNote.Padding = new Padding(4, 0, 0, 8);
            lblNote.Size = new System.Drawing.Size(0, 28);
            lblNote.Text = "ℹ️  Đang tải số liệu...";

            // ════════════════════════════════════════════════════
            // Helper — kích thước card đồng nhất
            // ════════════════════════════════════════════════════
            var cardSize = new System.Drawing.Size(240, 162);
            var cardMargin = new Padding(10, 8, 10, 8);

            // ════════════════════════════════════════════════════
            // Card 1 — Dự án đang chạy (Blue)
            // ════════════════════════════════════════════════════
            panelCard1.BackColor = System.Drawing.Color.White;
            panelCard1.BorderStyle = BorderStyle.FixedSingle;
            panelCard1.Cursor = Cursors.Hand;
            panelCard1.Name = "panelCard1";
            panelCard1.Size = cardSize;
            panelCard1.Margin = cardMargin;
            panelCard1.Controls.AddRange(new Control[]
            { panelCard1Top, lblCard1Icon, lblCard1Title, lblStatProjects, lblCard1Sub });

            panelCard1Top.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            panelCard1Top.Dock = DockStyle.Top;
            panelCard1Top.Height = 5;
            panelCard1Top.Name = "panelCard1Top";

            lblCard1Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard1Icon.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblCard1Icon.Location = new Point(14, 14);
            lblCard1Icon.Name = "lblCard1Icon";
            lblCard1Icon.Size = new System.Drawing.Size(44, 40);
            lblCard1Icon.Text = "📁";
            lblCard1Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard1Title.ForeColor = UIHelper.ColorMuted;
            lblCard1Title.Location = new Point(64, 22);
            lblCard1Title.Name = "lblCard1Title";
            lblCard1Title.Size = new System.Drawing.Size(162, 16);
            lblCard1Title.Text = "DỰ ÁN ĐANG CHẠY";

            lblStatProjects.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatProjects.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblStatProjects.Location = new Point(14, 58);
            lblStatProjects.Name = "lblStatProjects";
            lblStatProjects.Size = new System.Drawing.Size(200, 60);
            lblStatProjects.Text = "...";

            lblCard1Sub.Font = UIHelper.FontSmall;
            lblCard1Sub.ForeColor = UIHelper.ColorSubtitle;
            lblCard1Sub.Location = new Point(14, 122);
            lblCard1Sub.Name = "lblCard1Sub";
            lblCard1Sub.Size = new System.Drawing.Size(200, 16);
            lblCard1Sub.Text = "dự án InProgress";

            // ════════════════════════════════════════════════════
            // Card 2 — Công việc của tôi (Green / Teal)
            // ════════════════════════════════════════════════════
            panelCard2.BackColor = System.Drawing.Color.White;
            panelCard2.BorderStyle = BorderStyle.FixedSingle;
            panelCard2.Cursor = Cursors.Hand;
            panelCard2.Name = "panelCard2";
            panelCard2.Size = cardSize;
            panelCard2.Margin = cardMargin;
            panelCard2.Controls.AddRange(new Control[]
            { panelCard2Top, lblCard2Icon, lblCard2Title, lblStatTasks, lblCard2Sub });

            panelCard2Top.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            panelCard2Top.Dock = DockStyle.Top;
            panelCard2Top.Height = 5;
            panelCard2Top.Name = "panelCard2Top";

            lblCard2Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard2Icon.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            lblCard2Icon.Location = new Point(14, 14);
            lblCard2Icon.Name = "lblCard2Icon";
            lblCard2Icon.Size = new System.Drawing.Size(44, 40);
            lblCard2Icon.Text = "✅";
            lblCard2Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard2Title.ForeColor = UIHelper.ColorMuted;
            lblCard2Title.Location = new Point(64, 22);
            lblCard2Title.Name = "lblCard2Title";
            lblCard2Title.Size = new System.Drawing.Size(162, 16);
            lblCard2Title.Text = "CÔNG VIỆC CỦA TÔI";

            lblStatTasks.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatTasks.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            lblStatTasks.Location = new Point(14, 58);
            lblStatTasks.Name = "lblStatTasks";
            lblStatTasks.Size = new System.Drawing.Size(200, 60);
            lblStatTasks.Text = "...";

            lblCard2Sub.Font = UIHelper.FontSmall;
            lblCard2Sub.ForeColor = UIHelper.ColorSubtitle;
            lblCard2Sub.Location = new Point(14, 122);
            lblCard2Sub.Name = "lblCard2Sub";
            lblCard2Sub.Size = new System.Drawing.Size(200, 16);
            lblCard2Sub.Text = "task được giao";

            // ════════════════════════════════════════════════════
            // Card 3 — Quá hạn (Red)
            // ════════════════════════════════════════════════════
            panelCard3.BackColor = System.Drawing.Color.White;
            panelCard3.BorderStyle = BorderStyle.FixedSingle;
            panelCard3.Cursor = Cursors.Hand;
            panelCard3.Name = "panelCard3";
            panelCard3.Size = cardSize;
            panelCard3.Margin = cardMargin;
            panelCard3.Controls.AddRange(new Control[]
            { panelCard3Top, lblCard3Icon, lblCard3Title, lblStatOverdue, lblCard3Sub });

            panelCard3Top.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            panelCard3Top.Dock = DockStyle.Top;
            panelCard3Top.Height = 5;
            panelCard3Top.Name = "panelCard3Top";

            lblCard3Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard3Icon.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblCard3Icon.Location = new Point(14, 14);
            lblCard3Icon.Name = "lblCard3Icon";
            lblCard3Icon.Size = new System.Drawing.Size(44, 40);
            lblCard3Icon.Text = "⚠️";
            lblCard3Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard3Title.ForeColor = UIHelper.ColorMuted;
            lblCard3Title.Location = new Point(64, 22);
            lblCard3Title.Name = "lblCard3Title";
            lblCard3Title.Size = new System.Drawing.Size(162, 16);
            lblCard3Title.Text = "QUÁ HẠN";

            lblStatOverdue.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatOverdue.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblStatOverdue.Location = new Point(14, 58);
            lblStatOverdue.Name = "lblStatOverdue";
            lblStatOverdue.Size = new System.Drawing.Size(200, 60);
            lblStatOverdue.Text = "...";

            lblCard3Sub.Font = UIHelper.FontSmall;
            lblCard3Sub.ForeColor = UIHelper.ColorSubtitle;
            lblCard3Sub.Location = new Point(14, 122);
            lblCard3Sub.Name = "lblCard3Sub";
            lblCard3Sub.Size = new System.Drawing.Size(200, 16);
            lblCard3Sub.Text = "task đã qua deadline";

            // ════════════════════════════════════════════════════
            // Card 4 — Hoàn thành tháng này (Purple)
            // ════════════════════════════════════════════════════
            panelCard4.BackColor = System.Drawing.Color.White;
            panelCard4.BorderStyle = BorderStyle.FixedSingle;
            panelCard4.Cursor = Cursors.Hand;
            panelCard4.Name = "panelCard4";
            panelCard4.Size = cardSize;
            panelCard4.Margin = cardMargin;
            panelCard4.Controls.AddRange(new Control[]
            { panelCard4Top, lblCard4Icon, lblCard4Title, lblStatDone, lblCard4Sub });

            panelCard4Top.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            panelCard4Top.Dock = DockStyle.Top;
            panelCard4Top.Height = 5;
            panelCard4Top.Name = "panelCard4Top";

            lblCard4Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard4Icon.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            lblCard4Icon.Location = new Point(14, 14);
            lblCard4Icon.Name = "lblCard4Icon";
            lblCard4Icon.Size = new System.Drawing.Size(44, 40);
            lblCard4Icon.Text = "🎯";
            lblCard4Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard4Title.ForeColor = UIHelper.ColorMuted;
            lblCard4Title.Location = new Point(64, 22);
            lblCard4Title.Name = "lblCard4Title";
            lblCard4Title.Size = new System.Drawing.Size(162, 16);
            lblCard4Title.Text = "XONG THÁNG NÀY";

            lblStatDone.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatDone.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            lblStatDone.Location = new Point(14, 58);
            lblStatDone.Name = "lblStatDone";
            lblStatDone.Size = new System.Drawing.Size(200, 60);
            lblStatDone.Text = "...";

            lblCard4Sub.Font = UIHelper.FontSmall;
            lblCard4Sub.ForeColor = UIHelper.ColorSubtitle;
            lblCard4Sub.Location = new Point(14, 122);
            lblCard4Sub.Name = "lblCard4Sub";
            lblCard4Sub.Size = new System.Drawing.Size(200, 16);
            lblCard4Sub.Text = $"task hoàn thành T{DateTime.Now.Month}";

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = UIHelper.ColorBackground;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Font = UIHelper.FontBase;
            this.Name = "frmHome";
            this.Text = "🏠  Trang chủ";
            this.StartPosition = FormStartPosition.Manual;

            // Thứ tự Add: Fill trước → Top
            this.Controls.Add(panelBody);    // DockStyle.Fill
            this.Controls.Add(panelHeader);  // DockStyle.Top

            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelWelcome.ResumeLayout(false);
            panelStats.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;
        private Panel panelBody;
        private Panel panelWelcome;
        private Label lblGreeting;
        private Label lblRole;
        private Label lblLastLogin;
        private Panel panelStats;
        private FlowLayoutPanel flowCards;
        private Label lblNote;

        private Panel panelCard1;
        private Panel panelCard1Top;
        private Label lblCard1Icon;
        private Label lblCard1Title;
        private Label lblStatProjects;
        private Label lblCard1Sub;

        private Panel panelCard2;
        private Panel panelCard2Top;
        private Label lblCard2Icon;
        private Label lblCard2Title;
        private Label lblStatTasks;
        private Label lblCard2Sub;

        private Panel panelCard3;
        private Panel panelCard3Top;
        private Label lblCard3Icon;
        private Label lblCard3Title;
        private Label lblStatOverdue;
        private Label lblCard3Sub;

        private Panel panelCard4;
        private Panel panelCard4Top;
        private Label lblCard4Icon;
        private Label lblCard4Title;
        private Label lblStatDone;
        private Label lblCard4Sub;
    }
}