using System.Text.Json;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmLogin : Form
    {
        private readonly IAuthService _authService;
        private bool _passwordVisible = false;
        private System.Windows.Forms.Timer _loadingTimer = null!;
        private int _loadingWidth = 0;

        public frmLogin(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();
            SetupLoadingTimer();
            LoadSavedUsername();
            btnEye.Click += BtnEye_Click;
        }

        private void SetupLoadingTimer()
        {
            _loadingTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _loadingTimer.Tick += (s, e) =>
            {
                _loadingWidth += 8;
                if (_loadingWidth >= 396) { _loadingWidth = 396; _loadingTimer.Stop(); }
                panelProgress.Width = _loadingWidth;
                panelProgress.Invalidate();
            };
        }

        private void txtUsername_EnterLeave(object? sender, EventArgs e)
            => panelInputUser.Invalidate();

        private void txtPassword_EnterLeave(object? sender, EventArgs e)
            => panelInputPass.Invalidate();

        private void LoadSavedUsername()
        {
            var saved = ReadSavedUsername();
            if (!string.IsNullOrEmpty(saved))
            {
                txtUsername.Text    = saved;
                chkRemember.Checked = true;
                txtPassword.Focus();
            }
            else
            {
                txtUsername.Focus();
            }
        }

        private void BtnEye_Click(object? sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.UseSystemPasswordChar = !_passwordVisible;
            btnEye.Text = _passwordVisible ? "🙈" : "👁";
            txtPassword.Focus();
            txtPassword.SelectionStart = txtPassword.Text.Length;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text    = string.Empty;
            btnLogin.Enabled = false;
            btnLogin.Text    = "Đang xác thực...";

            _loadingWidth         = 0;
            panelProgress.Width   = 0;
            panelProgress.Visible = true;
            _loadingTimer.Start();

            try
            {
                var result = await _authService.LoginAsync(
                    txtUsername.Text.Trim(),
                    // BUG FIX: KHÔNG .Trim() mật khẩu khi login
                    // Phải truyền nguyên mật khẩu, BCrypt.Verify() sẽ so sánh đúng
                    txtPassword.Text);

                _loadingTimer.Stop();
                panelProgress.Width = 396;
                panelProgress.Invalidate();
                await Task.Delay(150);

                if (!result.Success)
                {
                    panelProgress.Visible = false;
                    panelProgress.Width   = 0;
                    lblError.Text         = result.ErrorMessage ?? "Đăng nhập thất bại.";
                    txtPassword.Clear();
                    txtPassword.Focus();
                    ShakePanel(panelRight);
                    return;
                }

                AppSession.Login(result.User!);
                SaveUsername(chkRemember.Checked ? txtUsername.Text.Trim() : "");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                _loadingTimer.Stop();
                panelProgress.Visible = false;
                lblError.Text = $"⚠  Lỗi kết nối: {ex.Message}";
            }
            finally
            {
                // BUG FIX: Chỉ reset button nếu form chưa đóng
                // Nếu login thành công → form đóng → set Enabled crash vì control disposed
                if (!this.IsDisposed)
                {
                    btnLogin.Enabled = true;
                    btnLogin.Text    = "ĐĂNG NHẬP  →";
                }
            }
        }

        private async void ShakePanel(System.Windows.Forms.Panel panel)
        {
            int original = panel.Left;
            foreach (var offset in new[] { -6, 6, -5, 5, -3, 3, 0 })
            {
                if (panel.IsDisposed) return;
                panel.Left = original + offset;
                await Task.Delay(30);
            }
            if (!panel.IsDisposed) panel.Left = original;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e);
        }

        private static readonly string SettingsPath =
            Path.Combine(AppContext.BaseDirectory, "user_prefs.json");

        private static string ReadSavedUsername()
        {
            try
            {
                if (!File.Exists(SettingsPath)) return string.Empty;
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    File.ReadAllText(SettingsPath));
                return dict?.GetValueOrDefault("SavedUsername") ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        private static void SaveUsername(string username)
        {
            try
            {
                File.WriteAllText(SettingsPath,
                    JsonSerializer.Serialize(
                        new Dictionary<string, string> { ["SavedUsername"] = username }));
            }
            catch { /* Bỏ qua lỗi ghi file */ }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _loadingTimer?.Stop();
            _loadingTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
