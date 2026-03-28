using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    /// <summary>
    /// Tài khoản người dùng trong hệ thống.
    /// Một user có thể đóng nhiều vai trò trên task: Assignee, Reviewer, Tester.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        /// <summary>Tên đăng nhập (unique, dùng làm khóa nghiệp vụ).</summary>
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        /// <summary>Họ và tên đầy đủ.</summary>
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>Email (unique).</summary>
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>Mật khẩu đã hash bằng BCrypt (WorkFactor 12).</summary>
        [Required, MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? AvatarPath { get; set; }

        /// <summary>Tài khoản active hay bị vô hiệu hóa (soft delete).</summary>
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        /* ================= RELATIONSHIPS ================= */

        /// <summary>Danh sách role của user (Admin / Manager / Developer).</summary>
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>Dự án user làm Owner/PM.</summary>
        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();

        /// <summary>Dự án user tham gia (qua bảng ProjectMembers).</summary>
        public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();

        /// <summary>Task do user tạo.</summary>
        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();

        /// <summary>Task được giao cho user thực hiện.</summary>
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

        // ── GD3: Workflow Review & Test ───────────────────────

        /// <summary>Task user làm Reviewer lần 1.</summary>
        public ICollection<TaskItem> Review1Tasks { get; set; } = new List<TaskItem>();

        /// <summary>Task user làm Reviewer lần 2.</summary>
        public ICollection<TaskItem> Review2Tasks { get; set; } = new List<TaskItem>();

        /// <summary>Task user làm Tester.</summary>
        public ICollection<TaskItem> TesterTasks { get; set; } = new List<TaskItem>();

        /// <summary>Bình luận của user.</summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>Chi phí do user tạo.</summary>
        public ICollection<Expense> CreatedExpenses { get; set; } = new List<Expense>();
    }
}
