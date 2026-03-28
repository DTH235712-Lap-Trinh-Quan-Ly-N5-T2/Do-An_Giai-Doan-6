using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Form thêm/sửa dự án.
    /// Thêm mới: nhập tên, khách hàng, PM, ngày, deadline, ngân sách, priority.
    /// Sửa: load data hiện tại vào form, cho phép chỉnh sửa.
    /// </summary>
    public partial class frmProjectEdit : BaseForm
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ICustomerRepository _customerRepo;
        private readonly Project? _editProject;
        private readonly bool _isEdit;

        private List<Customer> _customers = new();
        private List<User> _managers = new();

        public frmProjectEdit(
            IProjectService projectService,
            IUserService userService,
            ICustomerRepository customerRepo,
            Project? editProject)
        {
            _projectService = projectService;
            _userService = userService;
            _customerRepo = customerRepo;
            _editProject = editProject;
            _isEdit = editProject != null;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDropdownsAsync();
            if (_isEdit) LoadEditData();
        }

        /// <summary>Load danh sách khách hàng, managers, priority vào các dropdown.</summary>
        private async Task LoadDropdownsAsync()
        {
            // Khách hàng
            _customers = await _customerRepo.GetAllAsync();
            cboCustomer.Items.Clear();
            cboCustomer.Items.Add("— Không chọn —");
            foreach (var c in _customers)
                cboCustomer.Items.Add(c.CompanyName);
            cboCustomer.SelectedIndex = 0;

            // Managers (Admin + Manager) làm PM
            var allUsers = await _userService.GetAllUsersAsync();
            _managers = allUsers.Where(u => u.IsActive &&
                u.UserRoles.Any(r => r.Role?.Name == "Manager" || r.Role?.Name == "Admin")).ToList();
            cboOwner.Items.Clear();
            foreach (var m in _managers)
                cboOwner.Items.Add(m.FullName);
            if (cboOwner.Items.Count > 0) cboOwner.SelectedIndex = 0;

            // Priority
            cboPriority.Items.AddRange(new object[] { "Low", "Medium", "High", "Critical" });
            cboPriority.SelectedIndex = 1;
        }

        /// <summary>Load dữ liệu dự án hiện tại vào form khi chế độ sửa.</summary>
        private void LoadEditData()
        {
            this.Text = "Sửa thông tin dự án";
            lblTitleForm.Text = "✏️  Sửa thông tin dự án";

            txtName.Text = _editProject!.Name;
            txtDescription.Text = _editProject.Description ?? "";
            dtpStartDate.Value = _editProject.StartDate.ToDateTime(TimeOnly.MinValue);

            if (_editProject.PlannedEndDate.HasValue)
            {
                chkDeadline.Checked = true;
                dtpDeadline.Value = _editProject.PlannedEndDate.Value.ToDateTime(TimeOnly.MinValue);
            }

            txtBudget.Text = _editProject.Budget > 0 ? _editProject.Budget.ToString("0") : "";

            // Chọn khách hàng trong dropdown
            if (_editProject.CustomerId.HasValue)
            {
                var idx = _customers.FindIndex(c => c.Id == _editProject.CustomerId);
                cboCustomer.SelectedIndex = idx >= 0 ? idx + 1 : 0;
            }

            // Chọn PM
            var ownerIdx = _managers.FindIndex(m => m.Id == _editProject.OwnerId);
            if (ownerIdx >= 0) cboOwner.SelectedIndex = ownerIdx;

            // Chọn priority
            cboPriority.SelectedIndex = Math.Max(0, _editProject.Priority - 1);
        }

        /// <summary>Lưu dự án (tạo mới hoặc cập nhật) sau khi validate.</summary>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            { lblError.Text = "⚠  Tên dự án không được để trống."; txtName.Focus(); return; }

            if (cboOwner.SelectedIndex < 0)
            { lblError.Text = "⚠  Vui lòng chọn người quản lý."; return; }

            if (chkDeadline.Checked && dtpDeadline.Value.Date <= dtpStartDate.Value.Date)
            {
                lblError.Text = "⚠  Deadline phải sau ngày bắt đầu.";
                dtpDeadline.Focus();
                return;
            }

            SetLoading(true);
            try
            {
                int? customerId = cboCustomer.SelectedIndex > 0
                    ? _customers[cboCustomer.SelectedIndex - 1].Id : null;
                int ownerId = _managers[cboOwner.SelectedIndex].Id;

                decimal budget = 0;
                if (!string.IsNullOrWhiteSpace(txtBudget.Text))
                    decimal.TryParse(txtBudget.Text.Replace(",", "").Replace(".", ""), out budget);

                DateOnly? deadline = chkDeadline.Checked
                    ? DateOnly.FromDateTime(dtpDeadline.Value) : null;

                if (_isEdit)
                {
                    _editProject!.Name = txtName.Text.Trim();
                    _editProject.Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
                    _editProject.CustomerId = customerId;
                    _editProject.OwnerId = ownerId;
                    _editProject.StartDate = DateOnly.FromDateTime(dtpStartDate.Value);
                    _editProject.PlannedEndDate = deadline;
                    _editProject.Budget = budget;
                    _editProject.Priority = (byte)(cboPriority.SelectedIndex + 1);

                    var (ok, msg) = await _projectService.UpdateProjectAsync(_editProject);
                    SetLoading(false);
                    if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                    else lblError.Text = "⚠  " + msg;
                }
                else
                {
                    var project = new Project
                    {
                        Name = txtName.Text.Trim(),
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                        CustomerId = customerId,
                        OwnerId = ownerId,
                        StartDate = DateOnly.FromDateTime(dtpStartDate.Value),
                        PlannedEndDate = deadline,
                        Budget = budget,
                        Priority = (byte)(cboPriority.SelectedIndex + 1),
                        Status = "NotStarted"
                    };
                    var (ok, msg) = await _projectService.CreateProjectAsync(project);
                    SetLoading(false);
                    if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                    else lblError.Text = "⚠  " + msg;
                }
            }
            catch (Exception ex)
            {
                SetLoading(false);
                lblError.Text = "⚠  Lỗi: " + (ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        { this.DialogResult = DialogResult.Cancel; this.Close(); }

        /// <summary>Bật/tắt DateTimePicker deadline theo checkbox.</summary>
        private void chkDeadline_CheckedChanged(object sender, EventArgs e)
        { dtpDeadline.Enabled = chkDeadline.Checked; }

        private void SetLoading(bool loading)
        { btnSave.Enabled = !loading; btnSave.Text = loading ? "⏳  Đang lưu..." : "💾  Lưu dự án"; }
    }
}