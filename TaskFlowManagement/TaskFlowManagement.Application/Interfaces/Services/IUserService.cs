using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>Lấy TẤT CẢ user (active + inactive) kèm Roles — dùng cho màn hình Admin.</summary>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>Lấy user đang active — dùng cho dropdown, assign task.</summary>
        Task<List<User>> GetAllActiveUsersAsync();

        Task<User?> GetByIdAsync(int id);

        Task<(bool Success, string Message)> CreateUserAsync(
            string username, string fullName, string email,
            string password, string roleName, string? phone = null);

        Task<(bool Success, string Message)> UpdateUserAsync(User user);

        /// <summary>Soft delete – set IsActive = false.</summary>
        Task<(bool Success, string Message)> DeactivateUserAsync(int userId);

        /// <summary>Kích hoạt lại – set IsActive = true (ngược của Deactivate).</summary>
        Task<(bool Success, string Message)> ActivateUserAsync(int userId);

        /// <summary>Đổi mật khẩu – verify old pass trước khi hash new pass.</summary>
        Task<(bool Success, string Message)> ChangePasswordAsync(
            int userId, string oldPassword, string newPassword);

        /// <summary>Đổi vai trò user – chỉ Admin mới được gọi. Xóa role cũ, gán role mới.</summary>
        Task<(bool Success, string Message)> ChangeRoleAsync(int userId, string newRoleName);
    }
}
