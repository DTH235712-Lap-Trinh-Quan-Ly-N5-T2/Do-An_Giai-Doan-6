using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmUserEdit : Form
    {
        private readonly IUserService _userService;
        private readonly User?        _editUser;
        private readonly bool         _isEdit;

        public frmUserEdit(IUserService userService, User? editUser)
        {
            _userService = userService;
            _editUser    = editUser;
            _isEdit      = editUser != null;
            InitializeComponent();
            LoadForm();
        }

        private void LoadForm()
        {
            cboRole.Items.AddRange(new object[] { "Admin", "Manager", "Developer" });

            if (_isEdit)
            {
                this.Text         = "Sửa thông tin tài khoản";
                lblTitleForm.Text = "✏️  Sửa thông tin";

                txtUsername.Text     = _editUser!.Username;
                txtUsername.Enabled  = false;
                txtUsername.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
                txtFullName.Text     = _editUser.FullName;
                txtEmail.Text        = _editUser.Email;
                txtPhone.Text        = _editUser.Phone ?? "";

                // Ẩn password
                panelDivider.Visible   = false;
                lblPassword.Visible    = false;
                panelPassInput.Visible = false;

                bool showRole = AppSession.IsAdmin && _editUser.Id != AppSession.UserId;
                if (showRole)
                {
                    lblRole.Visible = true;
                    cboRole.Visible = true;
                    var currentRole = _editUser.UserRoles?.FirstOrDefault()?.Role?.Name ?? "Developer";
                    cboRole.SelectedItem = currentRole;
                    if (cboRole.SelectedIndex < 0) cboRole.SelectedIndex = 2;

                    // Dời Role lên sát Phone bằng offset tương đối
                    // Designer: lblRole.Top=362, panelDivider.Top=280
                    // Khoảng cần dời lên = lblRole.Top - panelDivider.Top = 82px
                    int moveUp = lblRole.Top - panelDivider.Top;
                    lblRole.Top -= moveUp;
                    cboRole.Top -= moveUp;

                    // Thu nhỏ form đúng bằng khoảng đã dời
                    this.Height -= moveUp;
                }
                else
                {
                    lblRole.Visible = false;
                    cboRole.Visible = false;

                    // Ẩn hết password + role → cắt từ Divider trở xuống
                    // Khoảng cắt = cboRole.Bottom - panelDivider.Top + padding
                    int cutHeight = cboRole.Bottom - panelDivider.Top + 16;
                    this.Height -= cutHeight;
                }
            }
            else
            {
                this.Text         = "Thêm tài khoản mới";
                lblTitleForm.Text = "➕  Thêm tài khoản mới";
                cboRole.SelectedIndex = 2;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            SetLoading(true);

            if (_isEdit)
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    ShowError("Họ tên không được để trống.");
                    SetLoading(false);
                    txtFullName.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ShowError("Email không được để trống.");
                    SetLoading(false);
                    txtEmail.Focus();
                    return;
                }
                var email = txtEmail.Text.Trim();
                if (!email.Contains('@') || !email.Contains('.'))
                {
                    ShowError("Email không hợp lệ.");
                    SetLoading(false);
                    txtEmail.Focus();
                    return;
                }

                _editUser!.FullName = txtFullName.Text.Trim();
                _editUser.Email     = email;
                _editUser.Phone     = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();

                var (ok, msg) = await _userService.UpdateUserAsync(_editUser);
                if (!ok) { SetLoading(false); ShowError(msg); return; }

                // Đổi role nếu Admin thay đổi
                if (cboRole.Visible && cboRole.SelectedItem != null)
                {
                    var newRole = cboRole.SelectedItem.ToString()!;
                    var currentRole = _editUser.UserRoles?.FirstOrDefault()?.Role?.Name;
                    if (currentRole != newRole)
                    {
                        var (roleOk, roleMsg) = await _userService.ChangeRoleAsync(_editUser.Id, newRole);
                        if (!roleOk) { SetLoading(false); ShowError(roleMsg); return; }
                    }
                }

                SetLoading(false);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                { ShowError("Tên đăng nhập không được để trống."); SetLoading(false); txtUsername.Focus(); return; }
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                { ShowError("Họ tên không được để trống."); SetLoading(false); txtFullName.Focus(); return; }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                { ShowError("Email không được để trống."); SetLoading(false); txtEmail.Focus(); return; }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                { ShowError("Mật khẩu không được để trống."); SetLoading(false); txtPassword.Focus(); return; }

                var (ok, msg) = await _userService.CreateUserAsync(
                    txtUsername.Text.Trim(), txtFullName.Text.Trim(), txtEmail.Text.Trim(),
                    txtPassword.Text, cboRole.SelectedItem?.ToString() ?? "Developer",
                    phone: string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim());

                SetLoading(false);
                if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                else ShowError(msg);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        { this.DialogResult = DialogResult.Cancel; this.Close(); }

        private void btnEyePass_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnEyePass.Text = txtPassword.UseSystemPasswordChar ? "👁" : "🙈";
        }

        private void ShowError(string msg) => lblError.Text = "⚠  " + msg;

        private void SetLoading(bool loading)
        { btnSave.Enabled = !loading; btnSave.Text = loading ? "Đang lưu..." : "💾  Lưu"; }
    }
}
