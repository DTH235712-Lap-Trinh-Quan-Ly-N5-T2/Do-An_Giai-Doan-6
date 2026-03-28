using TaskFlowManagement.Core.DTOs;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Lưu thông tin phiên làm việc của user đang đăng nhập.
    ///
    /// Dùng static class vì:
    ///   - Session là dữ liệu toàn cục, mọi Form đều cần truy cập
    ///   - WinForms không có HttpContext như web → dùng static thay thế
    ///   - Không cần inject vào constructor từng Form
    ///
    /// Nhận UserSessionDto (không có PasswordHash) từ AuthService.
    /// </summary>
    public static class AppSession
    {
        // Thông tin cơ bản của user đang đăng nhập
        public static int     UserId     { get; private set; }
        public static string  Username   { get; private set; } = string.Empty;
        public static string  FullName   { get; private set; } = string.Empty;
        public static string  Email      { get; private set; } = string.Empty;
        public static string? AvatarPath { get; private set; }

        // Danh sách role: ["Admin"], ["Manager"], ["Developer"]
        public static List<string> Roles { get; private set; } = new();

        // ---- Computed properties – kiểm tra quyền nhanh ----
        // IsAdmin: chỉ user có role "Admin"
        public static bool IsAdmin     => Roles.Contains("Admin");
        // IsManager: Manager hoặc Admin (Admin có toàn quyền)
        public static bool IsManager   => Roles.Contains("Manager") || IsAdmin;
        // IsDeveloper: user thường, quyền hạn chế nhất
        public static bool IsDeveloper => Roles.Contains("Developer");
        // IsLoggedIn: kiểm tra đã đăng nhập chưa (UserId > 0)
        public static bool IsLoggedIn  => UserId > 0;

        /// <summary>Khởi tạo session từ DTO trả về bởi AuthService.LoginAsync().</summary>
        public static void Login(UserSessionDto dto)
        {
            UserId     = dto.Id;
            Username   = dto.Username;
            FullName   = dto.FullName;
            Email      = dto.Email;
            AvatarPath = dto.AvatarPath;
            Roles      = dto.Roles;
        }

        /// <summary>Xóa toàn bộ thông tin session khi đăng xuất.</summary>
        public static void Logout()
        {
            UserId     = 0;
            Username   = string.Empty;
            FullName   = string.Empty;
            Email      = string.Empty;
            AvatarPath = null;
            Roles      = new List<string>();
        }
    }
}
