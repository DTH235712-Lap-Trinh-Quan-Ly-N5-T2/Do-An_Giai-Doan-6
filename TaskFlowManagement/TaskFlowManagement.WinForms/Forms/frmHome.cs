using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Màn hình chào sau khi đăng nhập thành công.
    /// Hiển thị: tên user, vai trò, thống kê nhanh (số liệu thật từ DB).
    /// Là MDI Child đầu tiên tự động mở khi vào frmMain.
    /// </summary>
    public partial class frmHome : BaseForm
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public frmHome(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
            InitializeComponent();
            LoadWelcomeInfo();
        }

        private void LoadWelcomeInfo()
        {
            var hour = DateTime.Now.Hour;
            var greeting = hour < 12 ? "Chào buổi sáng" :
                           hour < 18 ? "Chào buổi chiều" : "Chào buổi tối";

            // Cập nhật cả lblHeader ở dark banner lẫn lblGreeting trong body
            lblHeader.Text = $"🏠  Trang chủ — {AppSession.FullName}";
            lblGreeting.Text = $"{greeting}, {AppSession.FullName}! 👋";
            lblRole.Text = $"Vai trò: {string.Join(", ", AppSession.Roles)}";
            lblLastLogin.Text = $"Đăng nhập lúc: {DateTime.Now:HH:mm  dd/MM/yyyy}";

            // Placeholder trước khi load async
            lblStatProjects.Text = "...";
            lblStatTasks.Text = "...";
            lblStatOverdue.Text = "...";
            lblStatDone.Text = "...";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStatsAsync();
        }

        private async Task LoadStatsAsync()
        {
            try
            {
                bool isManager = AppSession.IsManager;
                int userId = AppSession.UserId;

                // Dự án đang chạy (InProgress)
                var projects = await _projectService.GetProjectsForUserAsync(userId, isManager);
                var runningProjects = projects.Count(p => p.Status == "InProgress");
                lblStatProjects.Text = runningProjects.ToString();

                // Công việc của tôi
                var myTasks = await _taskService.GetMyTasksAsync(userId);
                lblStatTasks.Text = myTasks.Count.ToString();

                // Quá hạn
                var overdue = await _taskService.GetOverdueTasksAsync();
                var overdueCount = isManager
                    ? overdue.Count
                    : overdue.Count(t => t.AssignedToId == userId);
                lblStatOverdue.Text = overdueCount.ToString();

                // Hoàn thành trong tháng này
                var thisMonth = DateTime.Now;
                var doneThisMonth = myTasks.Count(t =>
                    t.IsCompleted &&
                    t.CompletedAt.HasValue &&
                    t.CompletedAt.Value.Month == thisMonth.Month &&
                    t.CompletedAt.Value.Year == thisMonth.Year);
                lblStatDone.Text = doneThisMonth.ToString();

                lblNote.Text = $"ℹ️  Cập nhật lúc {DateTime.Now:HH:mm}  —  {projects.Count} dự án tổng";
            }
            catch
            {
                lblStatProjects.Text = "—";
                lblStatTasks.Text = "—";
                lblStatOverdue.Text = "—";
                lblStatDone.Text = "—";
                lblNote.Text = "ℹ️  Không thể tải số liệu. Kiểm tra kết nối database.";
            }
        }
    }
}