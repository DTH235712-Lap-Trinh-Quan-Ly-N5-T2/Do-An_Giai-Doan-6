using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlowManagement.Core.Entities
{
    /// <summary>
    /// Công việc (Task) trong dự án.
    /// 
    /// Workflow 10 trạng thái:
    ///   CREATED → ASSIGNED → IN-PROGRESS → REVIEW-1 → REVIEW-2 → APPROVED → IN-TEST → RESOLVED → CLOSED
    ///                             ↑             ↓          ↓                     ↓
    ///                             └──────── FAILED ←───────┘←────────────────────┘
    /// 
    /// Mỗi giai đoạn có người phụ trách riêng:
    ///   - AssignedTo: người làm task (Developer)
    ///   - Reviewer1: người review lần 1 (peer/team lead)
    ///   - Reviewer2: người review lần 2 (senior/architect)
    ///   - Tester: người kiểm thử (QA)
    /// 
    /// Luồng gán người:
    ///   Tạo task → gán Assignee
    ///   Assignee xong → chuyển REVIEW-1 + gán Reviewer1
    ///   Reviewer1 pass → chuyển REVIEW-2 + gán Reviewer2
    ///   Reviewer2 pass → APPROVED → IN-TEST + gán Tester
    ///   Bất kỳ ai (Reviewer1/2, Tester) reject → FAILED → Assignee sửa lại
    /// </summary>
    public class TaskItem
    {
        public int Id { get; set; }

        /// <summary>Tiêu đề công việc (bắt buộc, tối đa 200 ký tự).</summary>
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>Mô tả chi tiết công việc (tùy chọn, tối đa 2000 ký tự).</summary>
        [MaxLength(2000)]
        public string? Description { get; set; }

        /// <summary>Đánh dấu task đã hoàn thành (tự động set khi ProgressPercent = 100).</summary>
        public bool IsCompleted { get; set; }

        /// <summary>Phần trăm hoàn thành: 0–100. Khi = 100 → IsCompleted = true.</summary>
        [Range(0, 100)]
        public byte ProgressPercent { get; set; } = 0;

        /// <summary>Số giờ ước tính để hoàn thành task.</summary>
        [Column(TypeName = "decimal(6,1)")]
        public decimal? EstimatedHours { get; set; }

        /// <summary>Số giờ thực tế đã làm.</summary>
        [Column(TypeName = "decimal(6,1)")]
        public decimal? ActualHours { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Hạn chót hoàn thành task.</summary>
        public DateTime? DueDate { get; set; }

        /// <summary>Thời điểm task được đánh dấu hoàn thành (status = RESOLVED/CLOSED).</summary>
        public DateTime? CompletedAt { get; set; }

        /* ================= RELATIONSHIPS ================= */

        /// <summary>Dự án chứa task này.</summary>
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        /// <summary>Task cha (hỗ trợ sub-task lồng nhau, null = task gốc).</summary>
        public int? ParentTaskId { get; set; }
        public TaskItem? ParentTask { get; set; }
        public ICollection<TaskItem> SubTasks { get; set; } = new List<TaskItem>();

        /// <summary>Người tạo task.</summary>
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        /// <summary>Người được giao thực hiện task (Developer chính).</summary>
        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }

        // ── GD3: Workflow Review & Test ───────────────────────

        /// <summary>
        /// Người review lần 1 (peer review / team lead).
        /// Được gán khi Assignee chuyển task sang REVIEW-1.
        /// </summary>
        public int? Reviewer1Id { get; set; }
        public User? Reviewer1 { get; set; }

        /// <summary>
        /// Người review lần 2 (senior developer / architect).
        /// Được gán khi Reviewer1 approve và chuyển sang REVIEW-2.
        /// </summary>
        public int? Reviewer2Id { get; set; }
        public User? Reviewer2 { get; set; }

        /// <summary>
        /// Người kiểm thử (QA / Tester).
        /// Được gán khi Reviewer2 approve và chuyển sang IN-TEST.
        /// </summary>
        public int? TesterId { get; set; }
        public User? Tester { get; set; }

        // ── Lookup tables ─────────────────────────────────────

        /// <summary>Mức độ ưu tiên (Low / Medium / High / Critical).</summary>
        public int PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;

        /// <summary>Trạng thái hiện tại trong workflow 10 bước.</summary>
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

        /// <summary>Phân loại task (Bug / Feature / Improvement / Research / Testing).</summary>
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // ── Collections ───────────────────────────────────────

        /// <summary>Danh sách bình luận trên task.</summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>Danh sách file đính kèm.</summary>
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        /// <summary>Danh sách tag gắn trên task.</summary>
        public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
    }
}
