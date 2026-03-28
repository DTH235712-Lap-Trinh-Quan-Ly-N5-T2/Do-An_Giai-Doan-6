using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlowManagement.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        // ---- Khách hàng ----
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // ---- Người sở hữu / PM ----
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        // ---- Timeline ----
        public DateOnly StartDate { get; set; }
        public DateOnly? PlannedEndDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }

        // ---- Ngân sách ----
        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; } = 0;

        // ---- Trạng thái & Ưu tiên ----
        /// <summary>NotStarted | InProgress | OnHold | Completed | Cancelled</summary>
        [Required, MaxLength(30)]
        public string Status { get; set; } = "NotStarted";

        /// <summary>1=Low | 2=Medium | 3=High | 4=Critical</summary>
        public byte Priority { get; set; } = 2;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /* ================= RELATIONSHIPS ================= */
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        /* ================= COMPUTED (không map DB) ================= */
        [NotMapped]
        public decimal TotalExpense => Expenses?.Sum(e => e.Amount) ?? 0;

        [NotMapped]
        public int ProgressPercent
        {
            get
            {
                var rootTasks = Tasks?.Where(t => t.ParentTaskId == null).ToList();
                if (rootTasks == null || rootTasks.Count == 0) return 0;
                return (int)rootTasks.Average(t => t.ProgressPercent);
            }
        }
    }
}
