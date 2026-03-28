// ============================================================
//  frmTaskList.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
//
//  THAY ĐỔI SO VỚI PHIÊN BẢN CŨ:
//  ─────────────────────────────────────────────────────────
//  [UI]
//   • Kế thừa BaseForm thay vì Form trực tiếp
//   • KHÔNG hard-code màu/font
//
//  [Dead Code đã xóa]
//   • ApplyRowColor() private static → ĐÃ XÓA
//     Lý do: logic này GIỐNG HOÀN TOÀN frmMyTasks.ApplyRowColor()
//     → đây là code bị DUPLICATE. Thay bằng UIHelper.ApplyTaskRowStyle()
//
//   • ComboItem record bên dưới → ĐÃ CHUYỂN sang Common/ComboItem.cs
//     Không định nghĩa helper record bên trong file Form nữa.
//
//  [DI Bug đã sửa]
//   • Constructor mặc định vẫn giữ để Designer không lỗi,
//     nhưng thêm [Obsolete] annotation để tránh dùng nhầm ở runtime.
// ============================================================
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmTaskList : BaseForm   // ← đổi từ Form sang BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────────────────
        private readonly ITaskService    _taskService    = null!;
        private readonly IProjectService _projectService = null!;
        private readonly IUserService    _userService    = null!;

        // ── State ─────────────────────────────────────────────────────────────
        private List<TaskItem> _currentItems = new();
        private TaskItem?      _selectedTask;
        private int            _currentPage  = 1;
        private int            _totalCount   = 0;
        private const int      PAGE_SIZE     = 20;

        // Debounce: tránh gọi DB liên tục khi user đang gõ
        private readonly System.Windows.Forms.Timer _debounceTimer = new() { Interval = 500 };

        // Cache lookup — load 1 lần khi mở form
        private List<Status>  _statuses = new();
        private List<Project> _projects = new();

        // ── Constructors ──────────────────────────────────────────────────────

        /// <summary>
        /// Constructor mặc định — chỉ dành cho VS Designer.
        /// KHÔNG gọi trực tiếp ở runtime; dùng constructor DI bên dưới.
        /// </summary>
        [Obsolete("Dùng constructor DI(ITaskService, IProjectService, IUserService). Constructor này chỉ dành cho VS Designer.")]
        public frmTaskList()
        {
            InitializeComponent();
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        /// <summary>
        /// Constructor DI — ServiceProvider gọi cái này khi chạy thật.
        /// </summary>
        public frmTaskList(
            ITaskService    taskService,
            IProjectService projectService,
            IUserService    userService)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService    = taskService;
            _projectService = projectService;
            _userService    = userService;
        }

        // ── Form Load ─────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyRolePermissions();
            await LoadLookupsAsync();
            await LoadDataAsync();
        }

        /// <summary>Ẩn nút CRUD nếu là Developer (chỉ được xem).</summary>
        private void ApplyRolePermissions()
        {
            bool canEdit = AppSession.IsManager;
            btnAddNew.Visible = canEdit;
            btnEdit.Visible   = canEdit;
            btnDelete.Visible = canEdit;
        }

        // ── Lookups ───────────────────────────────────────────────────────────

        private async Task LoadLookupsAsync()
        {
            // Load song song để tiết kiệm thời gian
            var t1 = _taskService.GetAllStatusesAsync();
            var t2 = _projectService.GetProjectsForUserAsync(
                         AppSession.UserId, AppSession.IsManager);
            await Task.WhenAll(t1, t2);

            _statuses = t1.Result;
            _projects = t2.Result;

            cboStatusFilter.Items.Clear();
            cboStatusFilter.Items.Add(new ComboItem(0, "— Tất cả trạng thái —"));
            foreach (var s in _statuses)
                cboStatusFilter.Items.Add(new ComboItem(s.Id, s.Name));
            cboStatusFilter.SelectedIndex = 0;

            cboProjectFilter.Items.Clear();
            cboProjectFilter.Items.Add(new ComboItem(0, "— Tất cả dự án —"));
            foreach (var p in _projects)
                cboProjectFilter.Items.Add(new ComboItem(p.Id, p.Name));
            cboProjectFilter.SelectedIndex = 0;
        }

        // ── Load Data ─────────────────────────────────────────────────────────

        private async Task LoadDataAsync()
        {
            SetStatus("⏳ Đang tải...");
            try
            {
                var keyword   = txtSearch.Text.Trim();
                var statusId  = GetComboId(cboStatusFilter);
                var projectId = GetComboId(cboProjectFilter);

                // Developer chỉ thấy task được giao cho chính mình
                int? assignedToId = AppSession.IsManager ? null : AppSession.UserId;

                var (items, total) = await _taskService.GetPagedAsync(
                    page         : _currentPage,
                    pageSize     : PAGE_SIZE,
                    projectId    : projectId > 0 ? projectId    : null,
                    assignedToId : assignedToId,
                    statusId     : statusId  > 0 ? statusId     : null,
                    keyword      : string.IsNullOrEmpty(keyword) ? null : keyword);

                _currentItems = items;
                _totalCount   = total;

                BindGrid(items);
                UpdatePagingLabel();
                SetStatus($"Hiển thị {items.Count} / {total} công việc");
            }
            catch (Exception ex)
            {
                SetStatus("⚠ Lỗi tải dữ liệu.");
                MessageBox.Show($"Không thể tải dữ liệu:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Grid Binding ──────────────────────────────────────────────────────

        private void BindGrid(List<TaskItem> items)
        {
            dgvTasks.Rows.Clear();
            _selectedTask = null;
            RefreshButtonStates();

            foreach (var t in items)
            {
                var due = t.DueDate.HasValue
                    ? t.DueDate.Value.ToLocalTime().ToString("dd/MM/yyyy") : "—";

                int idx = dgvTasks.Rows.Add(
                    t.Id,
                    t.Title,
                    t.Project?.Name        ?? "—",
                    t.AssignedTo?.FullName ?? "—",
                    t.Priority?.Name       ?? "—",
                    t.Status?.Name         ?? "—",
                    $"{t.ProgressPercent}%",
                    due);

                // [REFACTOR] Không còn private ApplyRowColor() ở đây nữa.
                // UIHelper.ApplyTaskRowStyle() là SINGLE SOURCE OF TRUTH
                // dùng chung cho cả frmTaskList và frmMyTasks.
                UIHelper.ApplyTaskRowStyle(
                    dgvTasks.Rows[idx],
                    t.Status?.Name,
                    t.IsCompleted,
                    t.DueDate);
            }

            lblCount.Text = $"{items.Count} công việc";
        }

        // ── Debounce Search ───────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private async void DebounceTimer_Tick(object? sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ── Filter ────────────────────────────────────────────────────────────

        private async void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ── Pagination ────────────────────────────────────────────────────────

        private void UpdatePagingLabel()
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PAGE_SIZE));
            lblPage.Text    = $"Trang {_currentPage} / {totalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage <= 1) return;
            _currentPage--;
            await LoadDataAsync();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PAGE_SIZE));
            if (_currentPage >= totalPages) return;
            _currentPage++;
            await LoadDataAsync();
        }

        // ── Selection ─────────────────────────────────────────────────────────

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                _selectedTask = null;
                RefreshButtonStates();
                return;
            }

            var cell = dgvTasks.SelectedRows[0].Cells["colId"].Value;
            if (cell == null) return;
            _selectedTask = _currentItems.FirstOrDefault(t => t.Id == (int)cell);
            RefreshButtonStates();
        }

        private void RefreshButtonStates()
        {
            bool sel = _selectedTask != null;
            btnEdit.Enabled   = sel;
            btnDelete.Enabled = sel;
        }

        // ── CRUD ──────────────────────────────────────────────────────────────

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, null);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadDataAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedTask == null) return;
            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, _selectedTask.Id);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadDataAsync();
        }

        private void dgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTask == null) return;
            if (MessageBox.Show(
                    $"Xóa công việc \"{_selectedTask.Title}\"?\n\n⚠ Không thể hoàn tác.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes) return;

            var (ok, msg) = await _taskService.DeleteTaskAsync(_selectedTask.Id);
            MessageBox.Show(msg,
                ok ? "Thành công" : "Không thể xóa",
                MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (ok) await LoadDataAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboStatusFilter.SelectedIndex  = 0;
            cboProjectFilter.SelectedIndex = 0;
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static int GetComboId(ComboBox cbo)
            => cbo.SelectedItem is ComboItem ci ? ci.Id : 0;

        private void SetStatus(string msg) => lblStatus.Text = msg;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Dispose();
            base.OnFormClosed(e);
        }
    }
}
