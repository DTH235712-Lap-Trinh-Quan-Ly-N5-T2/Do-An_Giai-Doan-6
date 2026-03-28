using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    /// <summary>
    /// Trạng thái công việc — workflow 10 bước quản lý dự án phần mềm.
    /// 
    /// Luồng chính:
    ///   CREATED → ASSIGNED → IN-PROGRESS → REVIEW-1 → REVIEW-2 → APPROVED → IN-TEST → RESOLVED → CLOSED
    /// Luồng reject:
    ///   REVIEW-1 / REVIEW-2 / IN-TEST → FAILED → IN-PROGRESS (sửa lại)
    /// </summary>
    public class Status
    {
        public int Id { get; set; }

        /// <summary>Tên trạng thái (VD: CREATED, IN-PROGRESS, REVIEW-1...).</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Mô tả công dụng của trạng thái (hiển thị trên UI).</summary>
        [MaxLength(300)]
        public string? Description { get; set; }

        /// <summary>Thứ tự hiển thị trên Kanban / danh sách (0 = đầu tiên).</summary>
        public byte DisplayOrder { get; set; } = 0;

        /// <summary>Mã màu hex để tô màu trên giao diện (VD: #4CAF50).</summary>
        [MaxLength(7)]
        public string? ColorHex { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
