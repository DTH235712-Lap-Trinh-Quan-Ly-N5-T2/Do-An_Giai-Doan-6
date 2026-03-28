using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    public class Attachment
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContentType { get; set; }

        public long FileSizeBytes { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public int UploadedById { get; set; }
        public User UploadedBy { get; set; } = null!;
    }
}
