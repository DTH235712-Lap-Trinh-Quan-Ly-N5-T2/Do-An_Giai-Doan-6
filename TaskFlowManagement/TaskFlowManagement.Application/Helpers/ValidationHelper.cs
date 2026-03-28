using System.Net.Mail;
using System.Text.RegularExpressions;

namespace TaskFlowManagement.Core.Helpers
{
    /// <summary>
    /// Tập trung toàn bộ quy tắc validate dùng chung cho các Service.
    /// Đặt 1 chỗ để khi thay đổi quy tắc chỉ sửa ở đây, áp dụng toàn app.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>Mật khẩu hợp lệ: tối thiểu 6 ký tự, không chỉ khoảng trắng.</summary>
        public static bool IsPasswordStrong(string password)
            => !string.IsNullOrWhiteSpace(password) && password.Length >= 6;

        /// <summary>Email hợp lệ theo chuẩn RFC (dùng MailAddress để parse).</summary>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch { return false; }
        }

        /// <summary>
        /// Username hợp lệ: 3-50 ký tự, chỉ chữ cái, số và dấu gạch dưới.
        /// Regex: ^[a-zA-Z0-9_]{3,50}$
        /// </summary>
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,50}$");
        }
    }
}
