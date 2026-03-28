using TaskFlowManagement.Core.DTOs;
using TaskFlowManagement.Core.Helpers;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;

namespace TaskFlowManagement.Core.Services.Auth
{
    /// <summary>
    /// Triển khai IAuthService – xác thực người dùng (Login).
    ///
    /// Luồng đăng nhập:
    ///   1. Validate username/password không rỗng
    ///   2. Tìm user trong DB (chỉ user active)
    ///   3. Verify BCrypt password qua PasswordHelper
    ///   4. Load danh sách Roles để phân quyền
    ///   5. Map User → UserSessionDto (không expose PasswordHash)
    ///   6. UpdateLastLogin fire-and-forget (không block UI)
    ///   7. Trả LoginResult.Ok hoặc LoginResult.Fail
    ///
    /// Dùng Result Pattern (LoginResult) thay vì throw Exception
    /// cho lỗi nghiệp vụ → Form xử lý gọn hơn, không cần try-catch.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        // Inject IUserRepository qua constructor (DI Container cung cấp)
        public AuthService(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            // --- Bước 1: Validate đầu vào (guard clauses) ---
            if (string.IsNullOrWhiteSpace(username))
                return LoginResult.Fail("⚠ Vui lòng nhập tên đăng nhập.");

            if (string.IsNullOrWhiteSpace(password))
                return LoginResult.Fail("⚠ Vui lòng nhập mật khẩu.");

            // --- Bước 2: Tìm user trong DB ---
            // GetByUsernameAsync chỉ trả user có IsActive = true
            var user = await _userRepo.GetByUsernameAsync(username.Trim());

            // --- Bước 3: Verify BCrypt password ---
            // Dùng thông báo chung cho cả 2 trường hợp sai username / sai password
            // → tránh tiết lộ username có tồn tại trong hệ thống không (security)
            if (user == null || !PasswordHelper.Verify(password, user.PasswordHash))
                return LoginResult.Fail("⚠ Tên đăng nhập hoặc mật khẩu không đúng.");

            // --- Bước 4: Load Roles để phân quyền (eager load) ---
            var userWithRoles = await _userRepo.GetWithRolesAsync(user.Id);
            if (userWithRoles == null)
                return LoginResult.Fail("⚠ Không thể tải thông tin quyền. Vui lòng thử lại.");

            // --- Bước 5: Map → DTO (không trả User entity thô ra ngoài Service) ---
            var dto = UserSessionMapper.ToDto(userWithRoles);

            // --- Bước 6: Cập nhật LastLogin (fire-and-forget, không await) ---
            // _ = bỏ qua Task trả về, không block thread UI
            _ = _userRepo.UpdateLastLoginAsync(user.Id);

            return LoginResult.Ok(dto);
        }

        // Delegate sang PasswordHelper – AuthService không chứa BCrypt logic trực tiếp
        // Nguyên tắc Single Responsibility: AuthService lo flow, PasswordHelper lo thuật toán
        public string HashPassword(string plainPassword) => PasswordHelper.Hash(plainPassword);
        public bool VerifyPassword(string plain, string hashed) => PasswordHelper.Verify(plain, hashed);
    }
}
