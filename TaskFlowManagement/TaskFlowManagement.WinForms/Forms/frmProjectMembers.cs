using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Quản lý thành viên dự án – thêm developer, gán vai trò, xóa (soft delete).
    /// </summary>
    public partial class frmProjectMembers : BaseForm
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly Project _project;
        private List<ProjectMember> _members = new();
        private List<User> _availableUsers = new();

        public frmProjectMembers(
            IProjectService projectService,
            IUserService userService,
            Project project)
        {
            _projectService = projectService;
            _userService = userService;
            _project = project;
            InitializeComponent();

            // Cập nhật tiêu đề header với tên dự án thực tế
            var title = $"👥  Thành viên — {_project.Name}";
            this.Text = title;
            lblTitle.Text = title;

            // Developer chỉ xem, không thêm/xóa
            bool canEdit = AppSession.IsManager;
            panelAdd.Visible = canEdit;
            btnRemove.Visible = canEdit;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadMembersAsync();
            await LoadAvailableUsersAsync();

            // Load danh sách vai trò trong dự án
            cboProjectRole.Items.AddRange(new object[] { "Developer", "Tester", "BA", "Tech Lead" });
            cboProjectRole.SelectedIndex = 0;
        }

        // ── Load Data ─────────────────────────────────────────────────────────

        private async Task LoadMembersAsync()
        {
            _members = await _projectService.GetMembersAsync(_project.Id);
            dgvMembers.Rows.Clear();
            foreach (var m in _members)
            {
                dgvMembers.Rows.Add(
                    m.UserId,
                    m.User?.FullName ?? "—",
                    m.User?.Email ?? "—",
                    m.ProjectRole ?? "—",
                    m.JoinedAt.ToLocalTime().ToString("dd/MM/yyyy"));
            }
            lblCount.Text = $"{_members.Count} thành viên";
        }

        private async Task LoadAvailableUsersAsync()
        {
            var allActive = await _userService.GetAllActiveUsersAsync();
            var memberIds = _members.Select(m => m.UserId).ToHashSet();
            _availableUsers = allActive.Where(u => !memberIds.Contains(u.Id)).ToList();

            cboUser.Items.Clear();
            foreach (var u in _availableUsers)
                cboUser.Items.Add($"{u.FullName}  ({u.Username})");
            if (cboUser.Items.Count > 0) cboUser.SelectedIndex = 0;
        }

        // ── Events ────────────────────────────────────────────────────────────

        private async void btnAddMember_Click(object sender, EventArgs e)
        {
            if (cboUser.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn người dùng.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = _availableUsers[cboUser.SelectedIndex];
            var role = cboProjectRole.SelectedItem?.ToString() ?? "Developer";

            var (ok, msg) = await _projectService.AddMemberAsync(_project.Id, user.Id, role);
            if (ok)
            {
                await LoadMembersAsync();
                await LoadAvailableUsersAsync();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0) return;
            int userId = (int)dgvMembers.SelectedRows[0].Cells["colMemberId"].Value;
            var member = _members.FirstOrDefault(m => m.UserId == userId);
            if (member == null) return;

            if (MessageBox.Show(
                    $"Xóa \"{member.User?.FullName}\" khỏi dự án?\n\nLịch sử tham gia vẫn được lưu lại.",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            var (ok, _) = await _projectService.RemoveMemberAsync(_project.Id, userId);
            if (ok)
            {
                await LoadMembersAsync();
                await LoadAvailableUsersAsync();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
            => this.Close();
    }
}