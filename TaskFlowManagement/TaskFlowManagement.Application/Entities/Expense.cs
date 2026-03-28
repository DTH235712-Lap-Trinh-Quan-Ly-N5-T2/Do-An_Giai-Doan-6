using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlowManagement.Core.Entities
{
    /// <summary>
    /// Chi phí phát sinh trong dự án
    /// </summary>
    public class Expense
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        [Required, MaxLength(100)]
        public string ExpenseType { get; set; } = string.Empty; // Nhân công / Phần mềm / Hạ tầng

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateOnly ExpenseDate { get; set; }

        [MaxLength(300)]
        public string? Note { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
