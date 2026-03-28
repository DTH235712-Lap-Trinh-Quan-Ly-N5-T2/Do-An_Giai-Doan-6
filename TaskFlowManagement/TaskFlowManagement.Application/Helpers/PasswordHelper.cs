namespace TaskFlowManagement.Core.Helpers
{
    /// <summary>
    /// Tập trung logic hash/verify mật khẩu bằng BCrypt.
    ///
    /// Tại sao BCrypt thay vì SHA-256?
    ///   SHA-256: thuật toán hash nhanh, GPU có thể thử 10 tỷ hash/giây → dễ brute-force
    ///   BCrypt:  cố tình chậm (~300ms/hash), có cost factor điều chỉnh được
    ///            tự sinh salt ngẫu nhiên cho mỗi password → chống rainbow table
    ///
    /// Tại sao tách ra Helper riêng?
    ///   AuthService và UserService đều cần hash/verify
    ///   → đặt 1 chỗ tránh duplicate code, dễ đổi thuật toán sau này
    /// </summary>
    public static class PasswordHelper
    {
        // WorkFactor 12 = 2^12 = 4096 vòng lặp nội bộ → ~300ms trên CPU thông thường
        // Tăng lên 13-14 nếu server mạnh hơn (mỗi +1 tăng gấp đôi thời gian hash)
        private const int WorkFactor = 12;

        /// <summary>
        /// Hash mật khẩu bằng BCrypt. Salt ngẫu nhiên được tạo tự động,
        /// nhúng vào trong chuỗi hash → không cần lưu salt riêng.
        /// </summary>
        public static string Hash(string plainPassword)
            => BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);

        /// <summary>
        /// Xác minh mật khẩu nhập vào với hash đã lưu trong DB.
        /// BCrypt tự extract salt từ hash → truyền vào đúng 2 tham số là đủ.
        /// </summary>
        public static bool Verify(string plainPassword, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
            }
            catch
            {
                // Bắt trường hợp hash format cũ (SHA-256) vẫn còn trong DB
                // → trả false thay vì crash app
                return false;
            }
        }
    }
}
