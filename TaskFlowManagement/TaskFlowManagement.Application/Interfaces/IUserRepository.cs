using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetWithRolesAsync(int userId);

        /// <summary>Lấy TẤT CẢ user kèm Roles (active + inactive) — dùng cho màn hình Admin.</summary>
        Task<List<User>> GetAllWithRolesAsync();

        Task<bool> IsUsernameExistsAsync(string username);
        Task UpdateLastLoginAsync(int userId);

        /// <summary>
        /// Set IsActive = true/false — dùng cho Activate/Deactivate.
        /// Tách riêng thay vì UpdateAsync để tránh ghi đè các field khác.
        /// </summary>
        Task SetActiveAsync(int userId, bool isActive);

        /// <summary>
        /// Cập nhật chỉ PasswordHash — tách riêng vì UpdateAsync bảo vệ cột này.
        /// </summary>
        Task UpdatePasswordAsync(int userId, string newPasswordHash);

        /// <summary>
        /// Tạo user và gán Role trong 1 transaction — tránh user không có Role.
        /// </summary>
        Task AddWithRoleAsync(User user, string roleName);

        /// <summary>
        /// Đổi role cho user: xóa tất cả role cũ, gán role mới.
        /// Dùng transaction để đảm bảo atomicity.
        /// </summary>
        Task ChangeRoleAsync(int userId, string newRoleName);
    }
}
