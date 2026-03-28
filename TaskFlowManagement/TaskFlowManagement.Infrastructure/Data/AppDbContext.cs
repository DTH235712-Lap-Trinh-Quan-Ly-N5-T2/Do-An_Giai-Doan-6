using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Infrastructure.Data
{
    /// <summary>
    /// AppDbContext – cầu nối giữa C# entities và SQL Server database.
    /// Cấu hình quan hệ bằng Fluent API trong OnModelCreating.
    /// 
    /// GD3: Thêm relationships cho Reviewer1, Reviewer2, Tester trên TaskItem.
    /// </summary>
    public class AppDbContext : DbContext
    {
        // ── DbSets: mỗi property = 1 bảng trong DB ──────────
        public DbSet<User>          Users          { get; set; }
        public DbSet<Role>          Roles          { get; set; }
        public DbSet<UserRole>      UserRoles      { get; set; }
        public DbSet<Customer>      Customers      { get; set; }
        public DbSet<Project>       Projects       { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Expense>       Expenses       { get; set; }
        public DbSet<Category>      Categories     { get; set; }
        public DbSet<Priority>      Priorities     { get; set; }
        public DbSet<Status>        Statuses       { get; set; }
        public DbSet<TaskItem>      TaskItems      { get; set; }
        public DbSet<Comment>       Comments       { get; set; }
        public DbSet<Attachment>    Attachments    { get; set; }
        public DbSet<Tag>           Tags           { get; set; }
        public DbSet<TaskTag>       TaskTags       { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Composite Keys ────────────────────────────────
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<TaskTag>()
                .HasKey(tt => new { tt.TaskItemId, tt.TagId });

            // ── User Relationships ────────────────────────────
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User).WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role).WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            // ── Project Relationships ─────────────────────────
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner).WithMany(u => u.OwnedProjects)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Customer).WithMany(c => c.Projects)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project).WithMany(p => p.Members)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User).WithMany(u => u.ProjectMemberships)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectMember>()
                .HasIndex(pm => new { pm.ProjectId, pm.UserId }).IsUnique();

            // ── Task Relationships ────────────────────────────

            // Người tạo task
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.CreatedBy).WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Người được giao thực hiện
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedTo).WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            // GD3: Reviewer lần 1 (peer review)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Reviewer1).WithMany(u => u.Review1Tasks)
                .HasForeignKey(t => t.Reviewer1Id)
                .OnDelete(DeleteBehavior.Restrict);

            // GD3: Reviewer lần 2 (senior review)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Reviewer2).WithMany(u => u.Review2Tasks)
                .HasForeignKey(t => t.Reviewer2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // GD3: Tester (QA)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Tester).WithMany(u => u.TesterTasks)
                .HasForeignKey(t => t.TesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task cha → Task con (self-referencing)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.ParentTask).WithMany(t => t.SubTasks)
                .HasForeignKey(t => t.ParentTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Expense Relationships ─────────────────────────
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Project).WithMany(p => p.Expenses)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.CreatedBy).WithMany(u => u.CreatedExpenses)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Attachment & Tag ──────────────────────────────
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.UploadedBy).WithMany()
                .HasForeignKey(a => a.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskTag>()
                .HasOne(tt => tt.TaskItem).WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTag>()
                .HasOne(tt => tt.Tag).WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Indexes ───────────────────────────────────────
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.ProjectId).HasDatabaseName("IX_TaskItems_ProjectId");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.StatusId).HasDatabaseName("IX_TaskItems_StatusId");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.PriorityId).HasDatabaseName("IX_TaskItems_PriorityId");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.AssignedToId).HasDatabaseName("IX_TaskItems_AssignedToId");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.DueDate).HasDatabaseName("IX_TaskItems_DueDate");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.CreatedAt).HasDatabaseName("IX_TaskItems_CreatedAt");
            modelBuilder.Entity<TaskItem>().HasIndex(t => new { t.ProjectId, t.StatusId }).HasDatabaseName("IX_TaskItems_ProjectId_StatusId");

            // GD3: Index cho Reviewer/Tester queries
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.Reviewer1Id).HasDatabaseName("IX_TaskItems_Reviewer1Id");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.Reviewer2Id).HasDatabaseName("IX_TaskItems_Reviewer2Id");
            modelBuilder.Entity<TaskItem>().HasIndex(t => t.TesterId).HasDatabaseName("IX_TaskItems_TesterId");

            modelBuilder.Entity<Project>().HasIndex(p => p.OwnerId).HasDatabaseName("IX_Projects_OwnerId");
            modelBuilder.Entity<Project>().HasIndex(p => p.Status).HasDatabaseName("IX_Projects_Status");
            modelBuilder.Entity<Project>().HasIndex(p => p.CustomerId).HasDatabaseName("IX_Projects_CustomerId");
            modelBuilder.Entity<Comment>().HasIndex(c => c.TaskItemId).HasDatabaseName("IX_Comments_TaskItemId");
            modelBuilder.Entity<Expense>().HasIndex(e => e.ProjectId).HasDatabaseName("IX_Expenses_ProjectId");
            modelBuilder.Entity<ProjectMember>().HasIndex(pm => pm.UserId).HasDatabaseName("IX_ProjectMembers_UserId");

            // ProgressPercent: BYTE nên tối đa 255 nhưng business rule là 0-100
            modelBuilder.Entity<TaskItem>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_TaskItems_ProgressPercent",
                    "[ProgressPercent] BETWEEN 0 AND 100"));

            // EstimatedHours & ActualHours phải dương nếu có
            modelBuilder.Entity<TaskItem>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_TaskItems_EstimatedHours",
                    "[EstimatedHours] IS NULL OR [EstimatedHours] > 0"));

            modelBuilder.Entity<TaskItem>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_TaskItems_ActualHours",
                    "[ActualHours] IS NULL OR [ActualHours] > 0"));

            // Expense.Amount phải dương
            modelBuilder.Entity<Expense>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_Expenses_Amount",
                    "[Amount] > 0"));

            // Project.Budget không âm
            modelBuilder.Entity<Project>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_Projects_Budget",
                    "[Budget] >= 0"));

            // Project.Priority chỉ 1-4
            modelBuilder.Entity<Project>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_Projects_Priority",
                    "[Priority] BETWEEN 1 AND 4"));

            // Project.Status chỉ được là các giá trị định nghĩa sẵn
            modelBuilder.Entity<Project>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_Projects_Status",
                    "[Status] IN ('NotStarted','InProgress','OnHold','Completed','Cancelled')"));

            // Priority.Level chỉ 1-4
            modelBuilder.Entity<Priority>()
                .ToTable(tb => tb.HasCheckConstraint(
                    "CK_Priorities_Level",
                    "[Level] BETWEEN 1 AND 4"));

            modelBuilder.Entity<Status>()
           .HasIndex(s => s.Name).IsUnique()
           .HasDatabaseName("UQ_Statuses_Name");

            modelBuilder.Entity<Status>()
                .HasIndex(s => s.DisplayOrder).IsUnique()
                .HasDatabaseName("UQ_Statuses_DisplayOrder");

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name).IsUnique()
                .HasDatabaseName("UQ_Categories_Name");

            modelBuilder.Entity<Priority>()
                .HasIndex(p => p.Name).IsUnique()
                .HasDatabaseName("UQ_Priorities_Name");

            modelBuilder.Entity<Priority>()
                .HasIndex(p => p.Level).IsUnique()
                .HasDatabaseName("UQ_Priorities_Level");

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name).IsUnique()
                .HasDatabaseName("UQ_Roles_Name");

            // ═══════════════════════════════════════════════════════
            // Mục 8: DeleteBehavior cho Attachment & Comment
            // ═══════════════════════════════════════════════════════
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.TaskItem).WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.TaskItem).WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User).WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
