using BCrypt.Net;

namespace TaskFlowManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Hàm tiện ích dùng chung cho toàn bộ Seeder.
    /// Tách riêng để tránh lặp code và dễ thay đổi thuật toán hash sau này.
    /// </summary>
    internal static class SeedHelper
    {
        /// <summary>
        /// Hash mật khẩu bằng BCrypt WorkFactor 12.
        ///
        /// ⚠️ FIX: Phiên bản cũ dùng SHA-256 + Salt tĩnh.
        /// Phiên bản này đã chuyển sang BCrypt – salt tự động, khác nhau mỗi lần hash.
        ///
        /// NẾU BẠN ĐÃ TỪNG CHẠY VERSION CŨ (SHA-256):
        ///   Phải xóa DB cũ trước: DROP DATABASE TaskFlowManagementDb
        ///   Sau đó chạy lại app để seed bằng BCrypt.
        /// </summary>
        internal static string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }
}
