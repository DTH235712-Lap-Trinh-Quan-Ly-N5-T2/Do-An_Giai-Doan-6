using TaskFlowManagement.Core.DTOs;

namespace TaskFlowManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Interface cho Authentication Service.
    /// Định nghĩa contract – WinForms layer chỉ biết Interface này,
    /// không biết cách implement bên trong.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Xác thực username + password, trả về LoginResult.
        /// Form chỉ gọi 1 dòng này – toàn bộ logic nằm trong Service.
        /// </summary>
        Task<LoginResult> LoginAsync(string username, string password);

        /// <summary>
        /// Hash mật khẩu bằng BCrypt (dùng khi tạo/đổi mật khẩu).
        /// </summary>
        string HashPassword(string plainPassword);

        /// <summary>
        /// Xác minh mật khẩu nhập vào so với hash trong DB.
        /// </summary>
        bool VerifyPassword(string plainPassword, string hashedPassword);
    }
}
