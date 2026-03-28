namespace TaskFlowManagement.Core.DTOs
{
    // =====================================================
    // DTO (Data Transfer Object) – đối tượng truyền dữ liệu giữa các tầng
    //
    // Tại sao cần DTO thay vì truyền Entity thẳng?
    //   Entity chứa tất cả cột DB (kể cả PasswordHash, CreatedAt...)
    //   DTO chỉ chứa đúng dữ liệu tầng trên cần biết
    //   → an toàn hơn, giảm coupling giữa tầng
    // =====================================================

    /// <summary>
    /// Kết quả trả về sau khi gọi IAuthService.LoginAsync().
    ///
    /// Dùng Result Pattern thay vì throw Exception cho lỗi nghiệp vụ:
    ///   - Form dễ kiểm tra: if (!result.Success) { lblError.Text = result.ErrorMessage; }
    ///   - Không cần try-catch cho lỗi "sai mật khẩu" (đây không phải exception)
    ///   - Exception chỉ dành cho lỗi hạ tầng (DB mất kết nối, ...)
    /// </summary>
    public class LoginResult
    {
        public bool   Success      { get; init; }
        public string? ErrorMessage { get; init; } // null khi thành công
        public UserSessionDto? User { get; init; } // null khi thất bại

        // Factory methods – tạo kết quả rõ nghĩa, không new trực tiếp
        public static LoginResult Ok(UserSessionDto user)
            => new() { Success = true, User = user };

        public static LoginResult Fail(string message)
            => new() { Success = false, ErrorMessage = message };
    }

    /// <summary>
    /// Thông tin user cần thiết sau khi đăng nhập – lưu vào AppSession.
    ///
    /// Chủ động KHÔNG có PasswordHash để:
    ///   - Không vô tình lộ hash ra Form/UI
    ///   - Giảm dữ liệu không cần thiết trong bộ nhớ
    /// </summary>
    public class UserSessionDto
    {
        public int    Id         { get; init; }
        public string Username   { get; init; } = string.Empty;
        public string FullName   { get; init; } = string.Empty;
        public string Email      { get; init; } = string.Empty;
        public string? AvatarPath { get; init; }

        // Danh sách tên role: ["Admin"], ["Manager"], ["Developer"]
        // Dùng để kiểm tra quyền trong AppSession (IsAdmin, IsManager...)
        public List<string> Roles { get; init; } = new();
    }
}
