using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Helpers;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;

namespace TaskFlowManagement.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthService    _authService;

        public UserService(IUserRepository userRepo, IAuthService authService)
        {
            _userRepo    = userRepo;
            _authService = authService;
        }

        public Task<List<User>> GetAllUsersAsync()        => _userRepo.GetAllWithRolesAsync();
        public Task<List<User>> GetAllActiveUsersAsync()  => _userRepo.GetAllAsync();
        public Task<User?> GetByIdAsync(int id)           => _userRepo.GetByIdAsync(id);

        public async Task<(bool Success, string Message)> CreateUserAsync(
            string username, string fullName, string email, string password, string roleName, string? phone = null)
        {
            if (!ValidationHelper.IsValidUsername(username))
                return (false, "Username chỉ gồm chữ, số, dấu gạch dưới (3–50 ký tự).");
            if (await _userRepo.IsUsernameExistsAsync(username))
                return (false, $"Username '{username}' đã tồn tại.");
            if (!ValidationHelper.IsValidEmail(email))
                return (false, "Email không hợp lệ.");
            if (!ValidationHelper.IsPasswordStrong(password))
                return (false, "Mật khẩu phải có ít nhất 6 ký tự.");

            var user = new User
            {
                Username     = username.Trim(),
                FullName     = fullName.Trim(),
                Email        = email.Trim(),
                Phone        = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(),
                PasswordHash = _authService.HashPassword(password),
                IsActive     = true
            };
            await _userRepo.AddWithRoleAsync(user, roleName);
            return (true, $"Tạo tài khoản '{username}' thành công.");
        }

        public async Task<(bool Success, string Message)> UpdateUserAsync(User user)
        {
            await _userRepo.UpdateAsync(user);
            return (true, "Cập nhật thông tin thành công.");
        }

        public async Task<(bool Success, string Message)> DeactivateUserAsync(int userId)
        {
            await _userRepo.SetActiveAsync(userId, false);
            return (true, "Tài khoản đã bị vô hiệu hóa.");
        }

        public async Task<(bool Success, string Message)> ActivateUserAsync(int userId)
        {
            await _userRepo.SetActiveAsync(userId, true);
            return (true, "Tài khoản đã được kích hoạt lại.");
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(
            int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return (false, "Không tìm thấy tài khoản.");
            if (!_authService.VerifyPassword(oldPassword, user.PasswordHash))
                return (false, "Mật khẩu cũ không đúng.");
            if (!ValidationHelper.IsPasswordStrong(newPassword))
                return (false, "Mật khẩu mới phải có ít nhất 6 ký tự.");

            await _userRepo.UpdatePasswordAsync(userId, _authService.HashPassword(newPassword));
            return (true, "Đổi mật khẩu thành công.");
        }

        /// <summary>
        /// Đổi vai trò cho user (chỉ Admin gọi từ form).
        /// Xóa tất cả role cũ → gán role mới.
        /// </summary>
        public async Task<(bool Success, string Message)> ChangeRoleAsync(int userId, string newRoleName)
        {
            try
            {
                await _userRepo.ChangeRoleAsync(userId, newRoleName);
                return (true, $"Đã đổi vai trò thành {newRoleName}.");
            }
            catch (InvalidOperationException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi đổi vai trò: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
