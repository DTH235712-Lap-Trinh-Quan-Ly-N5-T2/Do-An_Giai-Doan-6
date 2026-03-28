using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Helper dùng chung cho các DataGridView hiển thị TaskItem.
    ///
    /// Tách ra từ frmTaskList và frmMyTasks — 2 form này trước đây
    /// copy-paste nguyên hàm ApplyRowColor giống hệt nhau.
    /// Giờ cả 2 gọi GridHelper.ApplyRowColor() → sửa 1 chỗ, hiệu lực mọi nơi.
    /// </summary>
    public static class GridHelper
    {
        /// <summary>
        /// Tô màu hàng DataGridView theo trạng thái task.
        /// Ưu tiên: Quá hạn (đỏ đậm) > Status color theo workflow.
        /// </summary>
        public static void ApplyRowColor(DataGridViewRow row, TaskItem task)
        {
            // Quá hạn → đỏ đậm (ưu tiên cao nhất, kiểm tra trước tiên)
            if (task.DueDate.HasValue && task.DueDate.Value < DateTime.UtcNow && !task.IsCompleted)
            {
                row.DefaultCellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                return;
            }

            row.DefaultCellStyle.ForeColor = task.Status?.Name switch
            {
                "CLOSED"                    => Color.FromArgb(5, 150, 105),
                "RESOLVED"                  => Color.FromArgb(22, 163, 74),
                "FAILED"                    => Color.FromArgb(220, 38, 38),
                "IN-PROGRESS"               => Color.FromArgb(37, 99, 235),
                "REVIEW-1" or "REVIEW-2"    => Color.FromArgb(180, 83, 9),
                "IN-TEST"                   => Color.FromArgb(109, 40, 217),
                _                           => Color.FromArgb(30, 41, 59),
            };
        }
    }
}
