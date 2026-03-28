using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required, MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
