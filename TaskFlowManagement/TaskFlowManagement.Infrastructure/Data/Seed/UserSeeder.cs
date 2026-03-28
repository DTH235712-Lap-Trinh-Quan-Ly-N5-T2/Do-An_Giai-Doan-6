using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seed dữ liệu người dùng mặc định.
    ///
    /// Mật khẩu được hash bằng BCrypt (WorkFactor 12).
    /// Tài khoản test:
    ///   admin     / Admin@123
    ///   manager1  / Manager@123
    ///   dev1      / Dev@123
    /// </summary>
    internal static class UserSeeder
    {
        internal static List<User> GetUsers()
        {
            // Pre-compute hash 1 lần cho mỗi loại mật khẩu
            // (tránh gọi BCrypt 15 lần → seed nhanh hơn)
            var adminHash   = SeedHelper.Hash("Admin@123");
            var managerHash = SeedHelper.Hash("Manager@123");
            var devHash     = SeedHelper.Hash("Dev@123");

            return new List<User>
            {
                // 1 Admin
                new() { Username = "admin",    FullName = "Quản trị viên",    Email = "admin@taskflow.com",    PasswordHash = adminHash,   IsActive = true },
                // 3 Managers
                new() { Username = "manager1", FullName = "Nguyễn Văn Minh",  Email = "minh.nv@taskflow.com",  PasswordHash = managerHash, IsActive = true },
                new() { Username = "manager2", FullName = "Trần Thị Lan",     Email = "lan.tt@taskflow.com",   PasswordHash = managerHash, IsActive = true },
                new() { Username = "manager3", FullName = "Lê Quang Huy",     Email = "huy.lq@taskflow.com",   PasswordHash = managerHash, IsActive = true },
                // 11 Developers
                new() { Username = "dev1",  FullName = "Phạm Văn An",    Email = "an.pv@taskflow.com",    PasswordHash = devHash, IsActive = true },
                new() { Username = "dev2",  FullName = "Hoàng Thị Bình", Email = "binh.ht@taskflow.com",  PasswordHash = devHash, IsActive = true },
                new() { Username = "dev3",  FullName = "Vũ Minh Cường",  Email = "cuong.vm@taskflow.com", PasswordHash = devHash, IsActive = true },
                new() { Username = "dev4",  FullName = "Đặng Thị Dung",  Email = "dung.dt@taskflow.com",  PasswordHash = devHash, IsActive = true },
                new() { Username = "dev5",  FullName = "Ngô Văn Em",     Email = "em.nv@taskflow.com",    PasswordHash = devHash, IsActive = true },
                new() { Username = "dev6",  FullName = "Bùi Thị Giang",  Email = "giang.bt@taskflow.com", PasswordHash = devHash, IsActive = true },
                new() { Username = "dev7",  FullName = "Dương Văn Hải",  Email = "hai.dv@taskflow.com",   PasswordHash = devHash, IsActive = true },
                new() { Username = "dev8",  FullName = "Lý Thị Hoa",     Email = "hoa.lt@taskflow.com",   PasswordHash = devHash, IsActive = true },
                new() { Username = "dev9",  FullName = "Mai Văn Khoa",   Email = "khoa.mv@taskflow.com",  PasswordHash = devHash, IsActive = true },
                new() { Username = "dev10", FullName = "Phan Thị Lan",   Email = "lan.pt@taskflow.com",   PasswordHash = devHash, IsActive = true },
                new() { Username = "dev11", FullName = "Trịnh Văn Nam",  Email = "nam.tv@taskflow.com",   PasswordHash = devHash, IsActive = true },
            };
        }
    }
}
