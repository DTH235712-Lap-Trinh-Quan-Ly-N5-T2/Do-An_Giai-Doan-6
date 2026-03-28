namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip      = new System.Windows.Forms.MenuStrip();
            this.statusStrip    = new System.Windows.Forms.StatusStrip();
            this.lblStatusUser  = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusRole  = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusTime  = new System.Windows.Forms.ToolStripStatusLabel();

            // Menu top-level items
            this.menuSystem    = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsers     = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProjects  = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTasks     = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReports   = new System.Windows.Forms.ToolStripMenuItem();

            // Menu System children
            this.menuHome           = new System.Windows.Forms.ToolStripMenuItem(); // BUG FIX #4
            this.toolStripSep0      = new System.Windows.Forms.ToolStripSeparator();
            this.menuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSep1      = new System.Windows.Forms.ToolStripSeparator();
            this.menuLogout         = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit           = new System.Windows.Forms.ToolStripMenuItem();

            // Menu Users children
            this.menuUserAccounts  = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmployees     = new System.Windows.Forms.ToolStripMenuItem();

            // Menu Customers children
            this.menuCustomerList  = new System.Windows.Forms.ToolStripMenuItem();

            // Menu Projects children
            this.menuProjectList   = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProjectNew    = new System.Windows.Forms.ToolStripMenuItem();

            // Menu Tasks children
            this.menuTaskList      = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKanban        = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMyTasks       = new System.Windows.Forms.ToolStripMenuItem();

            // Menu Reports children
            this.menuDashboard      = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportProgress = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportBudget   = new System.Windows.Forms.ToolStripMenuItem();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // ═══════════════════════════════════════════
            // MENU STRIP
            // ═══════════════════════════════════════════
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuStrip.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuStrip.Padding   = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuSystem,
                this.menuUsers,
                this.menuCustomers,
                this.menuProjects,
                this.menuTasks,
                this.menuReports
            });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name     = "menuStrip";
            this.menuStrip.Size     = new System.Drawing.Size(1280, 28);
            this.menuStrip.TabIndex = 0;

            // ---- menuSystem ----
            this.menuSystem.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuSystem.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.menuSystem.Name      = "menuSystem";
            this.menuSystem.Padding   = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.menuSystem.Text      = "⚙  Hệ thống";
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuHome,          // BUG FIX #4: nút về Trang chủ
                this.toolStripSep0,
                this.menuChangePassword,
                this.toolStripSep1,
                this.menuLogout,
                this.menuExit
            });

            // BUG FIX #4: menuHome – về trang chủ từ bất kỳ form con nào
            this.menuHome.Name   = "menuHome";
            this.menuHome.Text   = "🏠  Trang chủ";
            this.menuHome.Font   = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.menuHome.Click += new System.EventHandler(this.menuHome_Click);

            this.toolStripSep0.Name = "toolStripSep0";

            this.menuChangePassword.Name   = "menuChangePassword";
            this.menuChangePassword.Text   = "🔐  Đổi mật khẩu";
            this.menuChangePassword.Click += new System.EventHandler(this.menuChangePassword_Click);

            this.toolStripSep1.Name = "toolStripSep1";

            this.menuLogout.Name   = "menuLogout";
            this.menuLogout.Text   = "🚪  Đăng xuất";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);

            this.menuExit.Name   = "menuExit";
            this.menuExit.Text   = "❌  Thoát";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);

            // ---- menuUsers ----
            this.menuUsers.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuUsers.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.menuUsers.Name      = "menuUsers";
            this.menuUsers.Padding   = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.menuUsers.Text      = "👥  Người dùng";
            this.menuUsers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuUserAccounts,
                this.menuEmployees
            });

            this.menuUserAccounts.Name   = "menuUserAccounts";
            this.menuUserAccounts.Text   = "Quản lý tài khoản";
            this.menuUserAccounts.Click += new System.EventHandler(this.menuUserAccounts_Click);

            this.menuEmployees.Name   = "menuEmployees";
            this.menuEmployees.Text   = "Quản lý nhân viên";
            this.menuEmployees.Click += new System.EventHandler(this.menuEmployees_Click);

            // ---- menuCustomers ----
            this.menuCustomers.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuCustomers.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.menuCustomers.Name      = "menuCustomers";
            this.menuCustomers.Padding   = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.menuCustomers.Text      = "🏢  Khách hàng";
            this.menuCustomers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuCustomerList
            });

            this.menuCustomerList.Name   = "menuCustomerList";
            this.menuCustomerList.Text   = "Danh sách khách hàng";
            this.menuCustomerList.Click += new System.EventHandler(this.menuCustomerList_Click);

            // ---- menuProjects ----
            this.menuProjects.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuProjects.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.menuProjects.Name      = "menuProjects";
            this.menuProjects.Padding   = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.menuProjects.Text      = "📁  Dự án";
            this.menuProjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuProjectList,
                this.menuProjectNew
            });

            this.menuProjectList.Name   = "menuProjectList";
            this.menuProjectList.Text   = "Danh sách dự án";
            this.menuProjectList.Click += new System.EventHandler(this.menuProjectList_Click);

            this.menuProjectNew.Name   = "menuProjectNew";
            this.menuProjectNew.Text   = "➕  Tạo dự án mới";
            this.menuProjectNew.Click += new System.EventHandler(this.menuProjectNew_Click);

            // ---- menuTasks ----
            this.menuTasks.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuTasks.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.menuTasks.Name      = "menuTasks";
            this.menuTasks.Padding   = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.menuTasks.Text      = "✅  Công việc";
            this.menuTasks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuTaskList,
                this.menuKanban,
                this.menuMyTasks
            });

            this.menuTaskList.Name   = "menuTaskList";
            this.menuTaskList.Text   = "Danh sách công việc";
            this.menuTaskList.Click += new System.EventHandler(this.menuTaskList_Click);

            this.menuKanban.Name   = "menuKanban";
            this.menuKanban.Text   = "🗂  Kanban Board";
            this.menuKanban.Click += new System.EventHandler(this.menuKanban_Click);

            this.menuMyTasks.Name   = "menuMyTasks";
            this.menuMyTasks.Text   = "📌  Công việc của tôi";
            this.menuMyTasks.Click += new System.EventHandler(this.menuMyTasks_Click);

            // ---- menuReports ----
            this.menuReports.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.menuReports.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.menuReports.Name      = "menuReports";
            this.menuReports.Padding   = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.menuReports.Text      = "📊  Báo cáo";
            this.menuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuDashboard,
                this.menuReportProgress,
                this.menuReportBudget
            });

            this.menuDashboard.Name   = "menuDashboard";
            this.menuDashboard.Text   = "📈  Dashboard tổng quan";
            this.menuDashboard.Click += new System.EventHandler(this.menuDashboard_Click);

            this.menuReportProgress.Name   = "menuReportProgress";
            this.menuReportProgress.Text   = "Báo cáo tiến độ";
            this.menuReportProgress.Click += new System.EventHandler(this.menuReportProgress_Click);

            this.menuReportBudget.Name   = "menuReportBudget";
            this.menuReportBudget.Text   = "Báo cáo ngân sách";
            this.menuReportBudget.Click += new System.EventHandler(this.menuReportBudget_Click);

            // ═══════════════════════════════════════════
            // STATUS STRIP
            // ═══════════════════════════════════════════
            this.statusStrip.BackColor  = System.Drawing.Color.FromArgb(15, 23, 42);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.lblStatusUser,
                this.lblStatusRole,
                this.lblStatusTime
            });
            this.statusStrip.Location = new System.Drawing.Point(0, 728);
            this.statusStrip.Name     = "statusStrip";
            this.statusStrip.Size     = new System.Drawing.Size(1280, 22);
            this.statusStrip.TabIndex = 1;

            this.lblStatusUser.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusUser.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.lblStatusUser.Name      = "lblStatusUser";
            this.lblStatusUser.Text      = "  👤 ...";

            this.lblStatusRole.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatusRole.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStatusRole.Name      = "lblStatusRole";
            this.lblStatusRole.Spring    = true;
            this.lblStatusRole.Text      = "";

            this.lblStatusTime.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusTime.ForeColor = System.Drawing.Color.FromArgb(100, 200, 130);
            this.lblStatusTime.Name      = "lblStatusTime";
            this.lblStatusTime.Text      = "  --:--:--  ";

            // ═══════════════════════════════════════════
            // FORM – frmMain
            // ═══════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize          = new System.Drawing.Size(1280, 750);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.IsMdiContainer  = true;
            this.MainMenuStrip   = this.menuStrip;
            this.MinimumSize     = new System.Drawing.Size(1024, 600);
            this.Name            = "frmMain";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "TaskFlow Management";
            this.WindowState     = System.Windows.Forms.FormWindowState.Maximized;

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Khai báo controls
        private System.Windows.Forms.MenuStrip   menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusUser;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusRole;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusTime;

        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuUsers;
        private System.Windows.Forms.ToolStripMenuItem menuCustomers;
        private System.Windows.Forms.ToolStripMenuItem menuProjects;
        private System.Windows.Forms.ToolStripMenuItem menuTasks;
        private System.Windows.Forms.ToolStripMenuItem menuReports;

        private System.Windows.Forms.ToolStripMenuItem  menuHome;          // BUG FIX #4
        private System.Windows.Forms.ToolStripSeparator toolStripSep0;
        private System.Windows.Forms.ToolStripMenuItem  menuChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSep1;
        private System.Windows.Forms.ToolStripMenuItem  menuLogout;
        private System.Windows.Forms.ToolStripMenuItem  menuExit;

        private System.Windows.Forms.ToolStripMenuItem menuUserAccounts;
        private System.Windows.Forms.ToolStripMenuItem menuEmployees;
        private System.Windows.Forms.ToolStripMenuItem menuCustomerList;
        private System.Windows.Forms.ToolStripMenuItem menuProjectList;
        private System.Windows.Forms.ToolStripMenuItem menuProjectNew;
        private System.Windows.Forms.ToolStripMenuItem menuTaskList;
        private System.Windows.Forms.ToolStripMenuItem menuKanban;
        private System.Windows.Forms.ToolStripMenuItem menuMyTasks;
        private System.Windows.Forms.ToolStripMenuItem menuDashboard;
        private System.Windows.Forms.ToolStripMenuItem menuReportProgress;
        private System.Windows.Forms.ToolStripMenuItem menuReportBudget;
    }
}
