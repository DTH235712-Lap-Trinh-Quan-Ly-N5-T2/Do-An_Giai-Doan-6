using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seed dữ liệu cho các bảng Lookup: Roles, Priorities, Statuses, Categories, Tags.
    /// 
    /// ⚠️ GD3: Statuses đã được thay đổi từ 4 → 10 trạng thái.
    ///    Nếu DB cũ còn 4 status → cần DROP DB rồi chạy lại để seed mới.
    /// </summary>
    internal static class LookupSeeder
    {
        internal static List<Role> GetRoles() => new()
        {
            new() { Name = "Admin",     Description = "Toàn quyền hệ thống" },
            new() { Name = "Manager",   Description = "Quản lý dự án, khách hàng, báo cáo" },
            new() { Name = "Developer", Description = "Thực hiện công việc được giao" }
        };

        internal static List<Priority> GetPriorities() => new()
        {
            new() { Name = "Low",      Level = 1, ColorHex = "#4CAF50" },
            new() { Name = "Medium",   Level = 2, ColorHex = "#FF9800" },
            new() { Name = "High",     Level = 3, ColorHex = "#F44336" },
            new() { Name = "Critical", Level = 4, ColorHex = "#9C27B0" }
        };

        /// <summary>
        /// 10 trạng thái workflow quản lý dự án phần mềm:
        /// 
        /// Luồng chính:
        ///   CREATED → ASSIGNED → IN-PROGRESS → REVIEW-1 → REVIEW-2 → APPROVED → IN-TEST → RESOLVED → CLOSED
        /// 
        /// Luồng reject (khi không đạt yêu cầu):
        ///   REVIEW-1 / REVIEW-2 / IN-TEST → FAILED → IN-PROGRESS
        /// 
        /// Ai chuyển trạng thái:
        ///   CREATED → ASSIGNED:      Manager tạo và giao task
        ///   ASSIGNED → IN-PROGRESS:  Developer bắt đầu làm
        ///   IN-PROGRESS → REVIEW-1:  Developer xong, gán Reviewer1
        ///   REVIEW-1 → REVIEW-2:     Reviewer1 approve, gán Reviewer2
        ///   REVIEW-1 → FAILED:       Reviewer1 reject
        ///   REVIEW-2 → APPROVED:     Reviewer2 approve
        ///   REVIEW-2 → FAILED:       Reviewer2 reject
        ///   APPROVED → IN-TEST:      Tự động hoặc Manager gán Tester
        ///   IN-TEST → RESOLVED:      Tester pass
        ///   IN-TEST → FAILED:        Tester reject
        ///   RESOLVED → CLOSED:       Manager xác nhận đóng task
        ///   FAILED → IN-PROGRESS:    Developer sửa lại và làm tiếp
        /// </summary>
        internal static List<Status> GetStatuses() => new()
        {
            new() { Name = "CREATED",     DisplayOrder = 0,  ColorHex = "#9E9E9E",
                     Description = "Task vừa được tạo, chờ phân công và review yêu cầu" },
            new() { Name = "ASSIGNED",    DisplayOrder = 1,  ColorHex = "#607D8B",
                     Description = "Đã giao cho developer, sẵn sàng thực hiện" },
            new() { Name = "IN-PROGRESS", DisplayOrder = 2,  ColorHex = "#2196F3",
                     Description = "Developer đang thực hiện code" },
            new() { Name = "FAILED",      DisplayOrder = 3,  ColorHex = "#F44336",
                     Description = "Bị reject từ review hoặc test, cần sửa lại" },
            new() { Name = "REVIEW-1",    DisplayOrder = 4,  ColorHex = "#FF9800",
                     Description = "Chờ review lần 1 từ peer hoặc team lead" },
            new() { Name = "REVIEW-2",    DisplayOrder = 5,  ColorHex = "#FF5722",
                     Description = "Chờ review lần 2 từ senior developer hoặc architect" },
            new() { Name = "APPROVED",    DisplayOrder = 6,  ColorHex = "#8BC34A",
                     Description = "Code review đã approve, sẵn sàng test" },
            new() { Name = "IN-TEST",     DisplayOrder = 7,  ColorHex = "#FF9800",
                     Description = "QA đang kiểm thử và validation" },
            new() { Name = "RESOLVED",    DisplayOrder = 8,  ColorHex = "#4CAF50",
                     Description = "Test pass, chờ deploy và đóng task" },
            new() { Name = "CLOSED",      DisplayOrder = 9,  ColorHex = "#009688",
                     Description = "Hoàn thành, đã deploy và archived" },
        };

        internal static List<Category> GetCategories() => new()
        {
            new() { Name = "Bug",         Description = "Lỗi cần sửa" },
            new() { Name = "Feature",     Description = "Tính năng mới" },
            new() { Name = "Improvement", Description = "Cải tiến hiện có" },
            new() { Name = "Research",    Description = "Nghiên cứu kỹ thuật" },
            new() { Name = "Testing",     Description = "Kiểm thử" }
        };

        internal static List<Tag> GetTags() => new()
        {
            new() { Name = "Urgent" },    new() { Name = "UI" },
            new() { Name = "Backend" },   new() { Name = "Database" },
            new() { Name = "API" },       new() { Name = "Security" },
            new() { Name = "Performance" }
        };
    }
}
