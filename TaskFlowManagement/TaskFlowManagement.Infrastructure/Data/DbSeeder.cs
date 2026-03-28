using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Infrastructure.Data.Seed;

namespace TaskFlowManagement.Infrastructure.Data
{
    /// <summary>
    /// Orchestrator seed data – gọi các Seeder nhỏ theo đúng thứ tự FK.
    ///
    /// THỨ TỰ BẮT BUỘC (do Foreign Key):
    ///   1. Lookup tables (Roles, Priorities, Statuses, Categories, Tags)
    ///   2. Users
    ///   3. UserRoles (cần UserId + RoleId)
    ///   4. Customers
    ///   5. Projects (cần UserId + CustomerId)
    ///   6. ProjectMembers (cần ProjectId + UserId)
    ///   7. TaskItems (cần ProjectId + UserId + StatusId + PriorityId + CategoryId)
    ///
    /// ══════════════════════════════════════════════════════
    /// ⚠️  KHI NÀO CẦN RESET DATABASE?
    ///
    ///   Nếu trước đây chạy version dùng SHA-256 (hash cũ),
    ///   DB đang có hash sai format → login sẽ báo sai mật khẩu.
    ///
    ///   CÁCH RESET (chọn 1 trong 3):
    ///
    ///   [A] SQL Server Management Studio (SSMS):
    ///       Kết nối → chuột phải TaskFlowManagementDb → Delete → OK
    ///
    ///   [B] Package Manager Console (Visual Studio):
    ///       Tools → NuGet Package Manager → Package Manager Console
    ///       Chọn Default project: TaskFlowManagement.Infrastructure
    ///       Gõ lệnh:
    ///           Drop-Database
    ///       Xác nhận Y → Enter
    ///
    ///   [C] Terminal / CMD:
    ///       cd TaskFlowManagement.Infrastructure
    ///       dotnet ef database drop --force
    ///
    ///   Sau khi drop → chạy lại app → DB tự tạo mới + seed BCrypt.
    /// ══════════════════════════════════════════════════════
    /// </summary>
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Guard: chỉ seed khi DB hoàn toàn trống
            // Nếu đã có user → bỏ qua toàn bộ (idempotent)
            if (await context.Users.AnyAsync()) return;

            // =====================================================
            // BƯỚC 1: LOOKUP TABLES
            // Phải commit trước để có ID cho các bảng phụ thuộc
            // =====================================================
            var roles      = LookupSeeder.GetRoles();
            var priorities = LookupSeeder.GetPriorities();
            var statuses   = LookupSeeder.GetStatuses();
            var categories = LookupSeeder.GetCategories();
            var tags       = LookupSeeder.GetTags();

            context.AddRange(roles);
            context.AddRange(priorities);
            context.AddRange(statuses);
            context.AddRange(categories);
            context.AddRange(tags);
            await context.SaveChangesAsync();
            // Sau SaveChanges: roles[0].Id, priorities[0].Id, ... đã có giá trị từ DB

            // =====================================================
            // BƯỚC 2: USERS (password hash bằng BCrypt – xem SeedHelper)
            // =====================================================
            var users = UserSeeder.GetUsers();
            context.AddRange(users);
            await context.SaveChangesAsync();
            // Sau đây users[i].Id đã có giá trị

            // =====================================================
            // BƯỚC 3: USER ROLES – gán quyền theo username pattern
            // =====================================================
            var adminRole   = roles.First(r => r.Name == "Admin");
            var managerRole = roles.First(r => r.Name == "Manager");
            var devRole     = roles.First(r => r.Name == "Developer");

            var userRoles = users.Select(u => new UserRole
            {
                UserId = u.Id,
                RoleId = u.Username switch
                {
                    "admin"                              => adminRole.Id,
                    var n when n.StartsWith("manager")  => managerRole.Id,
                    _                                   => devRole.Id
                }
            }).ToList();

            context.AddRange(userRoles);
            await context.SaveChangesAsync();

            // =====================================================
            // BƯỚC 4: CUSTOMERS + PROJECTS
            // =====================================================
            var customers = ProjectSeeder.GetCustomers();
            context.AddRange(customers);
            await context.SaveChangesAsync();

            var projects = ProjectSeeder.GetProjects(users, customers);
            context.AddRange(projects);
            await context.SaveChangesAsync();

            // =====================================================
            // BƯỚC 5: PROJECT MEMBERS – mỗi project 3-5 devs ngẫu nhiên
            // =====================================================
            var devUsers = users.Where(u => u.Username.StartsWith("dev")).ToList();
            var rng      = new Random(42); // seed cố định → reproducible

            var members = new List<ProjectMember>();
            foreach (var project in projects)
            {
                var assigned = devUsers
                    .OrderBy(_ => rng.Next())
                    .Take(rng.Next(3, 6))
                    .ToList();

                members.AddRange(assigned.Select(dev => new ProjectMember
                {
                    ProjectId   = project.Id,
                    UserId      = dev.Id,
                    ProjectRole = "Developer",
                    JoinedAt    = project.CreatedAt
                }));
            }
            context.AddRange(members);
            await context.SaveChangesAsync();

            // =====================================================
            // BƯỚC 6: TASK ITEMS – 10 task mẫu mỗi project
            // =====================================================
            var taskList = BuildTaskItems(
                projects, members, users, priorities, statuses, categories, rng);

            context.AddRange(taskList);
            await context.SaveChangesAsync();
        }

        // -------------------------------------------------------
        // Tách logic tạo task ra method riêng cho gọn
        // -------------------------------------------------------
        private static List<TaskItem> BuildTaskItems(
            List<Project>       projects,
            List<ProjectMember> members,
            List<User>          users,
            List<Priority>      priorities,
            List<Status>        statuses,
            List<Category>      categories,
            Random              rng)
        {
            var now = DateTime.UtcNow;

            // Template (title, category, priority)
            var templates = new[]
            {
                ("Phân tích yêu cầu hệ thống",     "Feature",     "High"),
                ("Thiết kế database schema",         "Feature",     "High"),
                ("Cài đặt môi trường development",   "Feature",     "Medium"),
                ("Xây dựng module xác thực Login",   "Feature",     "Critical"),
                ("API quản lý người dùng",           "Feature",     "High"),
                ("UI Dashboard tổng quan",           "Feature",     "Medium"),
                ("Fix bug phân quyền sai",           "Bug",         "High"),
                ("Viết unit test Auth module",       "Testing",     "Medium"),
                ("Tối ưu query performance",         "Improvement", "Medium"),
                ("Review code Pull Request #12",     "Research",    "Low"),
            };

            var result = new List<TaskItem>();

            foreach (var project in projects)
            {
                var projectDevs = members
                    .Where(m => m.ProjectId == project.Id)
                    .Select(m => users.First(u => u.Id == m.UserId))
                    .ToList();

                var owner = users.First(u => u.Id == project.OwnerId);

                for (int i = 0; i < templates.Length; i++)
                {
                    var (title, catName, priName) = templates[i];
                    var priority = priorities.First(p => p.Name == priName);
                    var status   = statuses[rng.Next(statuses.Count)];
                    var category = categories.FirstOrDefault(c => c.Name == catName)
                                   ?? categories.First();
                    var assignee = projectDevs.Count > 0
                        ? projectDevs[i % projectDevs.Count]
                        : owner;

                    var progress = status.Name == "Done"       ? (byte)100
                                 : status.Name == "InProgress" ? (byte)rng.Next(20, 80)
                                 : (byte)0;

                    result.Add(new TaskItem
                    {
                        Title           = title,
                        Description     = $"[{project.Name}] {title}",
                        ProjectId       = project.Id,
                        CreatedById     = owner.Id,
                        AssignedToId    = assignee.Id,
                        PriorityId      = priority.Id,
                        StatusId        = status.Id,
                        CategoryId      = category.Id,
                        DueDate         = now.AddDays(rng.Next(-7, 30)),
                        ProgressPercent = progress,
                        IsCompleted     = progress == 100,
                        EstimatedHours  = rng.Next(2, 20),
                        CreatedAt       = project.CreatedAt.AddDays(rng.Next(0, 10)),
                        UpdatedAt       = now,
                        CompletedAt     = progress == 100
                                          ? now.AddDays(-rng.Next(1, 7))
                                          : null
                    });
                }
            }

            return result;
        }
    }
}
