using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;
using TaskFlowManagement.WinForms.Controls;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmKanban : BaseForm
    {
        // ── Status ID constants ───────────────────────────────────────────────
        private const int StatusCreated = 1;
        private const int StatusAssigned = 2;
        private const int StatusInProgress = 3;
        private const int StatusFailed = 4;
        private const int StatusReview1 = 5;
        private const int StatusReview2 = 6;
        private const int StatusApproved = 7;
        private const int StatusInTest = 8;
        private const int StatusResolved = 9;
        private const int StatusClosed = 10;

        // ── Dependencies ──────────────────────────────────────────────────────
        private readonly ITaskService? _taskService;
        private readonly IProjectService? _projectService;
        private readonly IUserService? _userService;
        private readonly int _projectId;

        // ── Constructors ──────────────────────────────────────────────────────

        /// <summary>
        /// Constructor mặc định — chỉ dành cho VS Designer.
        /// KHÔNG gọi trực tiếp ở runtime; dùng constructor DI bên dưới.
        /// </summary>
        [Obsolete("Dùng constructor DI(ITaskService, IProjectService, IUserService, int). Constructor này chỉ dành cho VS Designer.")]
        public frmKanban()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>Constructor DI — ServiceProvider gọi cái này khi chạy thật.</summary>
        public frmKanban(
            ITaskService taskService,
            IProjectService projectService,
            IUserService userService,
            int projectId)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _projectId = projectId;
        }

        // ── Form Load ─────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_taskService == null || DesignMode) return;

            // Cập nhật tiêu đề với project ID thực tế (sau khi DI constructor chạy)
            var title = $"🗂️  Kanban Board — Dự án #{_projectId}";
            this.Text = title;
            lblHeader.Text = title;

            await LoadTasksAsync();
        }

        // ── Khởi tạo form (chỉ những gì phụ thuộc runtime) ──────────────────

        private void InitializeForm()
        {
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1000, 650);

            EnableDragDropOnAllColumns();
        }

        // ── Load Data ─────────────────────────────────────────────────────────

        private async Task LoadTasksAsync()
        {
            SetStatus("⏳  Đang tải dữ liệu...");
            ClearAllColumns();

            try
            {
                var tasks = await _taskService!.GetBoardTasksAsync(_projectId);

                foreach (var task in tasks)
                {
                    var card = new ucTaskCard { Margin = new Padding(6) };
                    card.Bind(task);
                    card.StatusChanged += TaskCard_StatusChanged;
                    card.CardDoubleClicked += TaskCard_DoubleClicked;
                    MoveCardToStatusPanel(card, task.StatusId);
                }

                SetStatus($"✔  Đã tải {tasks.Count} công việc");
            }
            catch (Exception ex)
            {
                SetStatus("⚠  Lỗi tải dữ liệu.");
                MessageBox.Show(
                    "Không thể tải Kanban board:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is ucTaskCard card)
                        card.StatusChanged -= TaskCard_StatusChanged;
                }
                panel.Controls.Clear();
            }
        }

        // ── Event Handlers ────────────────────────────────────────────────────

        private async void TaskCard_StatusChanged(object? sender, StatusChangedEventArgs e)
        {
            if (sender is not ucTaskCard card) return;

            try
            {
                var (success, message) = await UpdateTaskStatusAsync(e.TaskId, e.NewStatusId);
                if (!success)
                {
                    MessageBox.Show(
                        message,
                        "Không thể chuyển trạng thái",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (card.Parent is FlowLayoutPanel currentParent)
                    currentParent.Controls.Remove(card);

                card.UpdateBoundStatus(e.NewStatusId);
                MoveCardToStatusPanel(card, e.NewStatusId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cập nhật trạng thái:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task<(bool Success, string Message)> UpdateTaskStatusAsync(int taskId, int newStatusId)
            => await _taskService!.UpdateStatusAsync(taskId, newStatusId, AppSession.UserId, AppSession.Roles);

        private async void TaskCard_DoubleClicked(object? sender, int taskId)
        {
            try
            {
                using var detailForm = new frmTaskEdit(_taskService!, _projectService!, _userService!, taskId);
                var result = detailForm.ShowDialog();
                if (result == DialogResult.OK)
                    await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết: " + ex.Message, "Lỗi");
            }
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
            => await LoadTasksAsync();

        // ── Routing helpers ───────────────────────────────────────────────────

        private void MoveCardToStatusPanel(ucTaskCard card, int statusId)
        {
            var targetPanel = GetPanelByStatusId(statusId);
            if (targetPanel != null)
                targetPanel.Controls.Add(card);
        }

        private DoubleBufferedFlowLayoutPanel? GetPanelByStatusId(int statusId)
        {
            return statusId switch
            {
                StatusCreated or StatusAssigned => flpTodo,
                StatusInProgress => flpInProgress,
                StatusReview1 or StatusReview2 => flpReview,
                StatusInTest => flpTesting,
                StatusFailed => flpFailed,
                StatusApproved or StatusResolved or StatusClosed => flpDone,
                _ => null
            };
        }

        private IEnumerable<DoubleBufferedFlowLayoutPanel> GetAllColumns()
        {
            yield return flpTodo;
            yield return flpInProgress;
            yield return flpReview;
            yield return flpTesting;
            yield return flpFailed;
            yield return flpDone;
        }

        // ── UI Helpers ────────────────────────────────────────────────────────

        private void SetStatus(string msg)
        {
            if (lblStatus != null && !lblStatus.IsDisposed)
                lblStatus.Text = msg;
        }

        private void EnableDragDropOnAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                panel.AllowDrop = true;
                panel.DragEnter += Column_DragEnter;
                panel.DragDrop += Column_DragDrop;
            }
        }

        private void Column_DragEnter(object? sender, DragEventArgs e)
            => e.Effect = e.Data?.GetDataPresent(typeof(int)) == true
                ? DragDropEffects.Move
                : DragDropEffects.None;

        private async void Column_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is not DoubleBufferedFlowLayoutPanel targetPanel) return;
            if (e.Data?.GetData(typeof(int)) is not int taskId) return;

            int newStatusId = GetPrimaryStatusForPanel(targetPanel);
            if (newStatusId <= 0) return;

            var (success, message) = await _taskService!.UpdateStatusAsync(
                taskId, newStatusId, AppSession.UserId, AppSession.Roles);

            if (!success)
            {
                MessageBox.Show(message, "Không thể chuyển trạng thái",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var card = FindCardById(taskId);
            if (card == null) return;

            if (card.Parent is FlowLayoutPanel srcPanel)
                srcPanel.Controls.Remove(card);

            card.UpdateBoundStatus(newStatusId);
            targetPanel.Controls.Add(card);
        }

        private int GetPrimaryStatusForPanel(DoubleBufferedFlowLayoutPanel panel)
        {
            if (panel == flpTodo) return StatusCreated;
            if (panel == flpInProgress) return StatusInProgress;
            if (panel == flpReview) return StatusReview1;
            if (panel == flpTesting) return StatusInTest;
            if (panel == flpFailed) return StatusFailed;
            if (panel == flpDone) return StatusResolved;
            return 0;
        }

        private ucTaskCard? FindCardById(int taskId)
        {
            foreach (var panel in GetAllColumns())
                foreach (Control ctrl in panel.Controls)
                    if (ctrl is ucTaskCard card && card.TaskId == taskId)
                        return card;
            return null;
        }
    }

    // ── Double-buffered FlowLayoutPanel (giữ nguyên ở đây) ───────────────────
    public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
    {
        public DoubleBufferedFlowLayoutPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}