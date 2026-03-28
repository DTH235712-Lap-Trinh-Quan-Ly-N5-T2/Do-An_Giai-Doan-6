// ============================================================
//  frmMyTasks.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
//
//  THAY ĐỔI SO VỚI PHIÊN BẢN CŨ:
//  ─────────────────────────────────────────────────────────
//  [UI]
//   • Kế thừa BaseForm thay vì Form trực tiếp
//
//  [Dead Code đã xóa]
//   • ApplyRowColor(DataGridViewRow, TaskItem) private static → ĐÃ XÓA
//     Lý do: Hoàn toàn trùng lặp với UIHelper.ApplyTaskRowStyle()
//     Thay bằng UIHelper.ApplyTaskRowStyle() trong BindGrid() để
//     đồng bộ 100% màu sắc với frmTaskList (single source of truth).
//
//  [Logic giữ nguyên 100%]
//   • OnLoad, LoadWelcomeInfo (đặt lại lblHeader trong Designer)
//   • ApplyRolePermissions, LoadAllTabsAsync
//   • BindGrid, btnRefresh_Click, dgv_CellDoubleClick
// ============================================================
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Form "Công việc của tôi" — hiển thị task liên quan đến user đang đăng nhập.
    ///
    /// Chia 3 tab:
    ///   - Tab 1 "Được giao cho tôi" : task mà AssignedToId = UserId hiện tại
    ///   - Tab 2 "Tôi cần Review"    : task mà Reviewer1Id hoặc Reviewer2Id = UserId
    ///   - Tab 3 "Tôi cần Test"      : task mà TesterId = UserId
    ///
    /// Developer chỉ thấy tab 1.
    /// Manager/Admin thấy cả 3 tab.
    /// </summary>
    public partial class frmMyTasks : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────────────────
        private readonly ITaskService _taskService = null!;
        private readonly IProjectService _projectService = null!;
        private readonly IUserService _userService = null!;

        // ── Constructors ──────────────────────────────────────────────────────

        /// <summary>
        /// Constructor mặc định — BẮT BUỘC để tab [Design] không lỗi.
        /// </summary>
        [Obsolete("Dùng constructor DI(ITaskService, IProjectService, IUserService). Constructor này chỉ dành cho VS Designer.")]
        public frmMyTasks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor DI — ServiceProvider gọi cái này khi chạy thật.
        /// </summary>
        public frmMyTasks(
            ITaskService taskService,
            IProjectService projectService,
            IUserService userService)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;

            _taskService.TaskDataChanged += OnTaskDataChanged;
        }

        private async void OnTaskDataChanged(object? sender, EventArgs e)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke((MethodInvoker)(async () => await LoadAllTabsAsync()));
            }
        }

        // ── Form Load ─────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Cập nhật tiêu đề form + header banner với thông tin user thực tế
            var title = $"📋  Công việc của tôi — {AppSession.FullName}";
            this.Text = title;
            lblHeader.Text = title;
            lblUser.Text = $"Đang hiển thị công việc của: {AppSession.FullName} ({AppSession.Username})";

            ApplyRolePermissions();
            await LoadAllTabsAsync();
        }

        // ── Role permissions ──────────────────────────────────────────────────

        /// <summary>Developer không thực hiện Review/Test → ẩn 2 tab đó.</summary>
        private void ApplyRolePermissions()
        {
            bool canReviewOrTest = AppSession.IsManager;
            tabReview.Parent = canReviewOrTest ? tabControl : null;
            tabTesting.Parent = canReviewOrTest ? tabControl : null;
        }

        // ── Load Data ─────────────────────────────────────────────────────────

        private async Task LoadAllTabsAsync()
        {
            SetStatus("⏳  Đang tải...");

            try
            {
                // Load song song 4 nguồn để tiết kiệm thời gian chờ
                var tMine = _taskService.GetMyTasksAsync(AppSession.UserId);
                var tReview1 = _taskService.GetTasksForReviewer1Async(AppSession.UserId);
                var tReview2 = _taskService.GetTasksForReviewer2Async(AppSession.UserId);
                var tTest = _taskService.GetTasksForTesterAsync(AppSession.UserId);

                await Task.WhenAll(tMine, tReview1, tReview2, tTest);

                // Gộp Review1 + Review2 vào 1 tab
                var reviewTasks = tReview1.Result.Concat(tReview2.Result).ToList();

                BindGrid(dgvMyTasks, tMine.Result);
                BindGrid(dgvReview, reviewTasks);
                BindGrid(dgvTesting, tTest.Result);

                // Cập nhật số lượng task lên tiêu đề tab
                tabMyTasks.Text = $"📋  Được giao ({tMine.Result.Count})";
                tabReview.Text = $"🔍  Review ({reviewTasks.Count})";
                tabTesting.Text = $"🧪  Testing ({tTest.Result.Count})";

                int total = tMine.Result.Count + reviewTasks.Count + tTest.Result.Count;
                SetStatus($"Tổng cộng {total} công việc liên quan đến bạn.");
            }
            catch (Exception ex)
            {
                SetStatus("⚠  Lỗi tải dữ liệu.");
                MessageBox.Show(
                    "Không thể tải dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ── Grid Binding ──────────────────────────────────────────────────────

        private static void BindGrid(DataGridView dgv, List<TaskItem> items)
        {
            dgv.Rows.Clear();

            foreach (var t in items)
            {
                var due = t.DueDate.HasValue
                    ? t.DueDate.Value.ToLocalTime().ToString("dd/MM/yyyy")
                    : "—";

                // Thứ tự cột: colId | colTitle | colProject | colPriority | colStatus | colProgress | colDueDate
                int idx = dgv.Rows.Add(
                    t.Id,
                    t.Title,
                    t.Project?.Name ?? "—",
                    t.Priority?.Name ?? "—",
                    t.Status?.Name ?? "—",
                    $"{t.ProgressPercent}%",
                    due);

                // [REFACTOR] Dùng UIHelper.ApplyTaskRowStyle() — single source of truth
                // Đã xóa private ApplyRowColor() bị duplicate ở đây.
                UIHelper.ApplyTaskRowStyle(
                    dgv.Rows[idx],
                    t.Status?.Name,
                    t.IsCompleted,
                    t.DueDate);
            }
        }

        // ── Events ────────────────────────────────────────────────────────────

        private async void btnRefresh_Click(object sender, EventArgs e)
            => await LoadAllTabsAsync();

        /// <summary>
        /// Double-click vào bất kỳ DataGridView nào → mở frmTaskEdit để xem/sửa chi tiết.
        /// </summary>
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (sender is not DataGridView dgv) return;

            var cell = dgv.Rows[e.RowIndex].Cells["colId"].Value;
            if (cell == null) return;

            int taskId = (int)cell;

            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, taskId);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                _ = LoadAllTabsAsync();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void SetStatus(string msg)
        {
            if (lblStatus != null && !lblStatus.IsDisposed)
                lblStatus.Text = msg;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _taskService.TaskDataChanged -= OnTaskDataChanged;
            base.OnFormClosed(e);
        }
    }
}