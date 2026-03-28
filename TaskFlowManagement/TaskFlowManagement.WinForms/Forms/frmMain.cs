using Microsoft.Extensions.DependencyInjection;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmMain : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private System.Windows.Forms.Timer? _clockTimer;

        public frmMain(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            ApplyRolePermissions();
            StartClock();
            UpdateUserInfo();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OpenHome();
        }

        private void OpenHome()
        {
            foreach (Form child in this.MdiChildren) child.Close();
            var home = _serviceProvider.GetRequiredService<frmHome>();
            home.MdiParent   = this;
            home.WindowState = FormWindowState.Maximized;
            home.Show();
        }

        private void UpdateUserInfo()
        {
            lblStatusUser.Text = $"  👤 {AppSession.FullName}";
            lblStatusRole.Text = $"  [{string.Join(", ", AppSession.Roles)}]";
            this.Text          = $"TaskFlow  —  {AppSession.FullName}";
        }

        // Phân quyền menu theo Role
        private void ApplyRolePermissions()
        {
            // Reset — mặc định hiện tất cả
            menuUsers.Visible        = true;
            menuCustomers.Visible    = true;
            menuProjects.Visible     = true;
            menuReports.Visible      = true;
            menuTaskList.Visible     = true;
            menuKanban.Visible       = true;
            menuUserAccounts.Visible = true;

            // FIX GD4: menuMyTasks hiện cho MỌI role (cả Developer lẫn Manager)
            // Developer cần xem task được giao cho mình → không ẩn menu này
            menuMyTasks.Visible = true;

            if (!AppSession.IsManager)
            {
                menuUsers.Visible     = false;
                menuCustomers.Visible = false;
                menuReports.Visible   = false;
                // FIX GD4: Developer vẫn thấy menu "Danh sách công việc" (xem read-only)
                // Trong frmTaskList, ApplyRolePermissions() đã ẩn nút Thêm/Sửa/Xóa rồi
                // menuTaskList.Visible = false; ← ĐÃ BỎ dòng này
                menuKanban.Visible    = false;
                menuProjectNew.Visible = false; // Developer không tạo dự án mới
            }
            if (!AppSession.IsAdmin)
                menuUserAccounts.Visible = false;
        }

        // Đồng hồ realtime
        private void StartClock()
        {
            StopClock();
            _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _clockTimer.Tick += (s, e) =>
                lblStatusTime.Text = DateTime.Now.ToString("HH:mm:ss   dd/MM/yyyy  ");
            _clockTimer.Start();
        }

        private void StopClock()
        {
            if (_clockTimer != null)
            { _clockTimer.Stop(); _clockTimer.Dispose(); _clockTimer = null; }
        }

        // Mở MDI child, kiểm tra tránh mở trùng form cùng type
        private void OpenMdiChild(Form child)
        {
            foreach (Form existing in this.MdiChildren)
            {
                if (existing.GetType() == child.GetType())
                { existing.Activate(); child.Dispose(); return; }
            }
            child.MdiParent   = this;
            child.WindowState = FormWindowState.Maximized;
            child.Show();
        }

        // ── Menu: Hệ thống ────────────────────────────────────
        private void menuHome_Click(object sender, EventArgs e) => OpenHome();

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmChangePassword>();
            frm.ShowDialog(this);
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            StopClock();
            AppSession.Logout();
            this.Hide();

            var loginForm = _serviceProvider.GetRequiredService<frmLogin>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                UpdateUserInfo();
                ApplyRolePermissions();
                StartClock();
                this.Show();
                OpenHome();
            }
            else Application.Exit();
        }

        private void menuExit_Click(object sender, EventArgs e) => Application.Exit();

        // ── Menu: Người dùng ──────────────────────────────────
        private void menuUserAccounts_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmUsers>());

        private void menuEmployees_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmUsers>());

        // ── Menu: Khách hàng ──────────────────────────────────
        private void menuCustomerList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmCustomers>());

        // ── Menu: Dự án (GD3) ─────────────────────────────────
        private void menuProjectList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmProjects>());

        private void menuProjectNew_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmProjects>());

        // ── Menu: Công việc (GD4) ─────────────────────────────

        /// <summary>
        /// Mở form Danh sách công việc — GD4.
        /// Manager: thấy tất cả task, có nút Thêm/Sửa/Xóa.
        /// Developer: chỉ thấy task được giao, không có nút CRUD.
        /// </summary>
        private void menuTaskList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmTaskList>());

        private void menuKanban_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bảng Kanban cần được gắn với một dự án cụ thể.\n\nVui lòng chọn một Dự án trong danh sách và bấm nút 'Kanban' trên thanh công cụ nhé!",
                            "Hướng dẫn",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // Tự động chuyển hướng mở form Danh sách Dự án luôn cho tiện
            OpenMdiChild(_serviceProvider.GetRequiredService<frmProjects>());
        }

        /// <summary>
        /// Mở form Công việc của tôi — GD4.
        /// Hiển thị task được giao / cần review / cần test của user đang đăng nhập.
        /// </summary>
        private void menuMyTasks_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmMyTasks>());

        // ── Menu: Báo cáo (Giai đoạn 6) ───────────────────────
        private void OpenDashboardTab(int index)
        {
            var existing = this.MdiChildren.OfType<frmDashboard>().FirstOrDefault();
            if (existing != null)
            {
                existing.Activate();
                existing.SelectTab(index);
            }
            else
            {
                var frm = _serviceProvider.GetRequiredService<frmDashboard>();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                // Phải show rồi mới SelectTab được vì TabControl cần tạo handle trước
                frm.SelectTab(index); 
            }
        }

        private void menuDashboard_Click(object sender, EventArgs e)
            => OpenDashboardTab(0);

        private void menuReportProgress_Click(object sender, EventArgs e)
            => OpenDashboardTab(1);

        private void menuReportBudget_Click(object sender, EventArgs e)
            => OpenDashboardTab(2);

        private static void ShowComingSoon(string featureName, string phase)
            => MessageBox.Show(
                $"Tính năng \"{featureName}\" đang được phát triển.\n\nDự kiến hoàn thành: {phase}.",
                "Đang phát triển", MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected override void OnFormClosing(FormClosingEventArgs e)
        { StopClock(); base.OnFormClosing(e); }
    }
}
