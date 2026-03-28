using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seed dữ liệu mẫu cho Customer và Project.
    /// Nhận danh sách Users đã lưu để gán OwnerId đúng.
    /// </summary>
    internal static class ProjectSeeder
    {
        internal static List<Customer> GetCustomers() => new()
        {
            new() { CompanyName = "FPT Software",        ContactName = "Nguyễn Minh Tuấn", Email = "contact@fpt.com",       Phone = "0901234001" },
            new() { CompanyName = "VNG Corporation",     ContactName = "Trần Thị Hương",   Email = "contact@vng.com.vn",    Phone = "0901234002" },
            new() { CompanyName = "VNPT Technology",     ContactName = "Lê Văn Đức",       Email = "contact@vnpt-it.com.vn",Phone = "0901234003" },
            new() { CompanyName = "Tiki Corporation",    ContactName = "Phạm Thị Thu",     Email = "contact@tiki.vn",       Phone = "0901234004" },
            new() { CompanyName = "Momo E-Wallet",       ContactName = "Hoàng Văn Long",   Email = "contact@momo.vn",       Phone = "0901234005" },
        };

        internal static List<Project> GetProjects(
            List<User> users, List<Customer> customers)
        {
            // Lấy managers để gán Owner
            var m1 = users.First(u => u.Username == "manager1");
            var m2 = users.First(u => u.Username == "manager2");
            var m3 = users.First(u => u.Username == "manager3");

            var now = DateTime.UtcNow;

            return new List<Project>
            {
                new() {
                    Name          = "Hệ thống quản lý nhân sự FPT",
                    Description   = "Xây dựng phần mềm quản lý nhân sự cho FPT Software",
                    OwnerId       = m1.Id,
                    CustomerId    = customers[0].Id,
                    StartDate     = DateOnly.FromDateTime(now.AddMonths(-3)),
                    PlannedEndDate= DateOnly.FromDateTime(now.AddMonths(3)),
                    Status        = "InProgress",
                    Priority      = 3,
                    Budget        = 500_000_000,
                    CreatedAt     = now.AddMonths(-3),
                    UpdatedAt     = now
                },
                new() {
                    Name          = "App thanh toán VNG Pay",
                    Description   = "Phát triển ứng dụng thanh toán di động cho VNG",
                    OwnerId       = m1.Id,
                    CustomerId    = customers[1].Id,
                    StartDate     = DateOnly.FromDateTime(now.AddMonths(-2)),
                    PlannedEndDate= DateOnly.FromDateTime(now.AddMonths(4)),
                    Status        = "InProgress",
                    Priority      = 4,
                    Budget        = 800_000_000,
                    CreatedAt     = now.AddMonths(-2),
                    UpdatedAt     = now
                },
                new() {
                    Name          = "Cổng dịch vụ công VNPT",
                    Description   = "Tích hợp cổng dịch vụ công trực tuyến cho VNPT",
                    OwnerId       = m2.Id,
                    CustomerId    = customers[2].Id,
                    StartDate     = DateOnly.FromDateTime(now.AddMonths(-1)),
                    PlannedEndDate= DateOnly.FromDateTime(now.AddMonths(5)),
                    Status        = "NotStarted",
                    Priority      = 2,
                    Budget        = 300_000_000,
                    CreatedAt     = now.AddMonths(-1),
                    UpdatedAt     = now
                },
                new() {
                    Name          = "Tiki Recommendation Engine",
                    Description   = "Xây dựng engine gợi ý sản phẩm cho Tiki",
                    OwnerId       = m2.Id,
                    CustomerId    = customers[3].Id,
                    StartDate     = DateOnly.FromDateTime(now.AddMonths(-4)),
                    PlannedEndDate= DateOnly.FromDateTime(now.AddDays(30)),
                    Status        = "InProgress",
                    Priority      = 3,
                    Budget        = 650_000_000,
                    CreatedAt     = now.AddMonths(-4),
                    UpdatedAt     = now
                },
                new() {
                    Name          = "MoMo Super App v3",
                    Description   = "Nâng cấp toàn diện MoMo lên Super App",
                    OwnerId       = m3.Id,
                    CustomerId    = customers[4].Id,
                    StartDate     = DateOnly.FromDateTime(now.AddMonths(-6)),
                    PlannedEndDate= DateOnly.FromDateTime(now.AddMonths(2)),
                    Status        = "InProgress",
                    Priority      = 4,
                    Budget        = 1_200_000_000,
                    CreatedAt     = now.AddMonths(-6),
                    UpdatedAt     = now
                },
            };
        }
    }
}
