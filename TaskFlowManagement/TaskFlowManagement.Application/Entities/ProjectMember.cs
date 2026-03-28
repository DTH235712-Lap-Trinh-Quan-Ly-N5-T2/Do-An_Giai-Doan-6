using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    /// <summary>
    /// Bảng trung gian: Thành viên tham gia dự án + vai trò trong dự án
    /// </summary>
    public class ProjectMember
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        /// <summary>Developer | Tester | BA | Tech Lead | PM</summary>
        [MaxLength(50)]
        public string? ProjectRole { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LeftAt { get; set; }
    }
}
