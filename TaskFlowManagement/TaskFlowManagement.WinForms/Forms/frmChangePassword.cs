using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmChangePassword : Form
    {
        private readonly IUserService _userService;

        // BUG FIX: Track trạng thái đã đổi thành công để tự đóng form
        private bool _changeSucceeded = false;

        public frmChangePassword(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            lblError.Text   = "";
            lblSuccess.Text = "";

            // BUG FIX: KHÔNG .Trim() mật khẩu
            // Người dùng có thể đặt mật khẩu có khoảng trắng đầu/cuối có chủ ý
            // .Trim() ở đây sẽ khiến họ không bao giờ đăng nhập được lại (vì lúc tạo không trim)
            var oldPass = txtOldPass.Text;
            var newPass = txtNewPass.Text;
            var confirm = txtConfirm.Text;

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            {
                ShowError("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (newPass != confirm)
            {
                ShowError("Mật khẩu mới và xác nhận không khớp.");
                txtConfirm.Clear();
                txtConfirm.Focus();
                return;
            }

            if (newPass == oldPass)
            {
                ShowError("Mật khẩu mới phải khác mật khẩu cũ.");
                return;
            }

            SetLoading(true);
            var (success, message) = await _userService.ChangePasswordAsync(
                AppSession.UserId, oldPass, newPass);
            SetLoading(false);

            if (success)
            {
                _changeSucceeded = true;
                // Xóa các ô để tránh lộ mật khẩu trong bộ nhớ
                txtOldPass.Clear();
                txtNewPass.Clear();
                txtConfirm.Clear();

                // Hiện toast thành công rồi tự đóng sau 2 giây
                ShowSuccessToast();
            }
            else
            {
                ShowError(message);
            }
        }

        // ── Toast thông báo thành công ────────────────────────────
        // Hiện overlay xanh lá full-form với checkmark lớn, tự đóng sau 2s
        private async void ShowSuccessToast()
        {
            // Tạo panel overlay phủ toàn bộ form
            var overlay = new Panel
            {
                BackColor = System.Drawing.Color.FromArgb(240, 5, 150, 105), // Xanh lá teal, hơi trong
                Bounds    = new System.Drawing.Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height),
                Visible   = false
            };

            // Icon checkmark lớn
            var lblCheck = new Label
            {
                Font      = new System.Drawing.Font("Segoe UI Emoji", 48F),
                ForeColor = System.Drawing.Color.White,
                Text      = "✅",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize  = false,
                Bounds    = new System.Drawing.Rectangle(0, 60, this.ClientSize.Width, 80),
                BackColor = System.Drawing.Color.Transparent
            };

            // Dòng chữ thành công
            var lblMsg = new Label
            {
                Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Text      = "Đổi mật khẩu thành công!",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize  = false,
                Bounds    = new System.Drawing.Rectangle(0, 148, this.ClientSize.Width, 34),
                BackColor = System.Drawing.Color.Transparent
            };

            // Dòng phụ đếm ngược
            var lblSub = new Label
            {
                Font      = new System.Drawing.Font("Segoe UI", 10F),
                ForeColor = System.Drawing.Color.FromArgb(200, 255, 255, 255),
                Text      = "Tự đóng sau 2 giây...",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize  = false,
                Bounds    = new System.Drawing.Rectangle(0, 188, this.ClientSize.Width, 24),
                BackColor = System.Drawing.Color.Transparent
            };

            overlay.Controls.AddRange(new Control[] { lblCheck, lblMsg, lblSub });
            this.Controls.Add(overlay);
            overlay.BringToFront();
            overlay.Visible = true;

            // Đếm ngược 2 → 1 → đóng
            await Task.Delay(1000);
            if (!this.IsDisposed) lblSub.Text = "Tự đóng sau 1 giây...";
            await Task.Delay(1000);

            // Dọn overlay và đóng form
            if (!this.IsDisposed)
            {
                overlay.Dispose();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // BUG FIX: Nếu đã đổi thành công thì trả OK, cancel trước khi đổi trả Cancel
            this.DialogResult = _changeSucceeded ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        // ── Eye toggle ────────────────────────────────────────────
        private void btnEye1_Click(object sender, EventArgs e) => ToggleEye(txtOldPass, btnEye1);
        private void btnEye2_Click(object sender, EventArgs e) => ToggleEye(txtNewPass, btnEye2);
        private void btnEye3_Click(object sender, EventArgs e) => ToggleEye(txtConfirm, btnEye3);

        private static void ToggleEye(TextBox txt, Button btn)
        {
            txt.UseSystemPasswordChar = !txt.UseSystemPasswordChar;
            btn.Text = txt.UseSystemPasswordChar ? "👁" : "🙈";
        }

        // ── Helpers ───────────────────────────────────────────────
        private void ShowError(string msg)
        {
            lblError.Text = "⚠  " + msg;
            ShakePanel(panelForm);
        }

        private void SetLoading(bool loading)
        {
            btnConfirm.Enabled = !loading;
            btnConfirm.Text    = loading ? "⏳  Đang xử lý..." : "✔  Xác nhận đổi mật khẩu";
        }

        private async void ShakePanel(Panel p)
        {
            int orig    = p.Left;
            int[] steps = { -6, 6, -5, 5, -3, 3, -1, 1, 0 };
            foreach (var offset in steps)
            {
                if (p.IsDisposed) return;
                p.Left = orig + offset;
                await Task.Delay(22);
            }
            if (!p.IsDisposed) p.Left = orig;
        }
    }
}
