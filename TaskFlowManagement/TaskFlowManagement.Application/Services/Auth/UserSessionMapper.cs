using TaskFlowManagement.Core.DTOs;
using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Services.Auth
{
    /// <summary>
    /// Chuyển đổi User entity → UserSessionDto.
    ///
    /// Tách ra file riêng vì:
    ///   - AuthService không nên chứa mapping logic (Single Responsibility)
    ///   - Dễ tái dùng khi cần refresh session sau này
    ///   - Dễ unit test mapping độc lập với logic login
    ///
    /// UserSessionDto chỉ chứa thông tin cần thiết cho UI,
    /// đặc biệt KHÔNG có PasswordHash → an toàn hơn khi lưu vào AppSession.
    /// </summary>
    internal static class UserSessionMapper
    {
        internal static UserSessionDto ToDto(User user) => new()
        {
            Id         = user.Id,
            Username   = user.Username,
            FullName   = user.FullName,
            Email      = user.Email,
            AvatarPath = user.AvatarPath,
            // Lấy tên các Role từ navigation property UserRoles → Role
            // Ví dụ kết quả: ["Admin"] hoặc ["Manager"] hoặc ["Developer"]
            Roles      = user.UserRoles?
                .Select(ur => ur.Role.Name)
                .ToList()
                ?? new List<string>()
        };
    }
}
