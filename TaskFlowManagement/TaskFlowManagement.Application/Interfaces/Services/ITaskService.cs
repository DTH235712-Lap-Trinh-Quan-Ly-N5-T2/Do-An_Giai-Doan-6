using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Interface cho Task Service – nghiệp vụ quản lý công việc GD4.
    ///
    /// GD4 bổ sung so với GD1/GD3:
    ///   - CreateTaskAsync / UpdateTaskAsync: validate đầy đủ
    ///   - UpdateProgressAsync: tự chuyển Status → RESOLVED khi progress = 100
    ///   - UpdateStatusAsync: chuyển trạng thái workflow 10 bước với validate
    ///   - GetLookupDataAsync: lấy Priority, Status, Category cho dropdown frmTaskEdit
    ///   - GetTasksForReviewerAsync / GetTasksForTesterAsync: cho màn hình Review/Test
    /// </summary>
    public interface ITaskService
    {
        // ── Truy vấn đơn ─────────────────────────────────────────

        /// <summary>
        /// Chi tiết task với đầy đủ navigation properties.
        /// Dùng cho frmTaskEdit — cần Priority, Status, Category, Users, SubTasks.
        /// </summary>
        Task<TaskItem?> GetByIdAsync(int taskId);

        // ── Phân trang + lọc ─────────────────────────────────────

        /// <summary>
        /// Phân trang + lọc kết hợp — dùng cho frmTaskList.
        /// Tất cả tham số nullable = tùy chọn.
        /// Trả về (Items, TotalCount) để tính số trang ở UI.
        /// </summary>
        Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int     page,
            int     pageSize,
            int?    projectId     = null,
            int?    assignedToId  = null,
            int?    statusId      = null,
            int?    priorityId    = null,
            int?    categoryId    = null,
            string? keyword       = null);

        // ── Truy vấn theo người phụ trách ────────────────────────

        /// <summary>
        /// Task chưa hoàn thành được giao cho developer (màn hình "Công việc của tôi").
        /// Developer chỉ thấy task của chính mình.
        /// </summary>
        Task<List<TaskItem>> GetMyTasksAsync(int userId);

        /// <summary>
        /// Task đang chờ review lần 1 bởi reviewer.
        /// Dùng cho tab "Review" trong frmMyTasks hoặc frmTaskList.
        /// </summary>
        Task<List<TaskItem>> GetTasksForReviewer1Async(int reviewerId);

        /// <summary>
        /// Task đang chờ review lần 2 bởi reviewer.
        /// </summary>
        Task<List<TaskItem>> GetTasksForReviewer2Async(int reviewerId);

        /// <summary>
        /// Task đang chờ test bởi tester.
        /// Dùng cho tab "Testing" trong frmMyTasks.
        /// </summary>
        Task<List<TaskItem>> GetTasksForTesterAsync(int testerId);

        // ── Cảnh báo / Dashboard ──────────────────────────────────

        /// <summary>Task đã quá hạn — widget cảnh báo đỏ Dashboard.</summary>
        Task<List<TaskItem>> GetOverdueTasksAsync();

        /// <summary>Task sắp đến hạn trong N ngày — widget nhắc nhở vàng Dashboard.</summary>
        Task<List<TaskItem>> GetDueSoonTasksAsync(int days = 7);

        /// <summary>
        /// Tóm tắt số task theo Status trong 1 dự án.
        /// Kết quả: { "CREATED": 3, "IN-PROGRESS": 7, ... }
        /// Dùng cho pie chart Dashboard.
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync(int projectId);

        // ── CRUD ─────────────────────────────────────────────────

        /// <summary>
        /// Tạo task mới.
        /// Validate: Title không trống, ProjectId > 0, PriorityId/StatusId/CategoryId hợp lệ.
        /// </summary>
        Task<(bool Success, string Message)> CreateTaskAsync(TaskItem task);

        /// <summary>
        /// Cập nhật task.
        /// Validate: Title không trống, task phải tồn tại.
        /// </summary>
        Task<(bool Success, string Message)> UpdateTaskAsync(TaskItem task);

        /// <summary>
        /// Xóa task.
        /// Validate: task không có sub-task (nếu có thì không cho xóa).
        /// </summary>
        Task<(bool Success, string Message)> DeleteTaskAsync(int taskId);

        // ── Cập nhật tiến độ & trạng thái ────────────────────────

        /// <summary>
        /// Cập nhật tiến độ % (0–100).
        ///   progress = 100 → 🎉 Task hoàn thành! IsCompleted = true, Status → RESOLVED
        ///   progress &lt; 100 → Cập nhật bình thường, IsCompleted = false
        /// </summary>
        Task<(bool Success, string Message)> UpdateProgressAsync(int taskId, byte progress);

        /// <summary>
        /// Chuyển trạng thái workflow với phân quyền.
        ///   requesterId    — AppSession.UserId
        ///   requesterRoles — AppSession.Roles
        /// </summary>
        Task<(bool Success, string Message)> UpdateStatusAsync(
            int taskId,
            int statusId,
            int requesterId,
            IList<string> requesterRoles);

        /// <summary>
        /// Gán reviewer/tester và chuyển trạng thái trong 1 thao tác.
        ///
        /// Ví dụ: Developer xong → gán Reviewer1, chuyển sang REVIEW-1.
        ///   AssignAndTransitionAsync(taskId, reviewer1Id: 5, newStatus: "REVIEW-1")
        ///
        /// Validate: newStatus hợp lệ, userId gán vào phải là user active.
        /// </summary>
        Task<(bool Success, string Message)> AssignAndTransitionAsync(
            int     taskId,
            string  newStatus,
            int?    reviewer1Id = null,
            int?    reviewer2Id = null,
            int?    testerId    = null);

        // ── Lookup data cho dropdown ──────────────────────────────

        /// <summary>
        /// Lấy tất cả Statuses (10 bước), sắp theo DisplayOrder.
        /// Dùng cho dropdown Status trong frmTaskEdit và frmTaskList filter.
        /// </summary>
        Task<List<Status>> GetAllStatusesAsync();

        /// <summary>
        /// Lấy tất cả Priorities (4 mức), sắp theo Level.
        /// Dùng cho dropdown Priority trong frmTaskEdit.
        /// </summary>
        Task<List<Priority>> GetAllPrioritiesAsync();

        /// <summary>
        /// Lấy tất cả Categories.
        /// Dùng cho dropdown Category trong frmTaskEdit.
        /// </summary>
        Task<List<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Lấy toàn bộ task của dự án để hiển thị lên Kanban Board.
        /// Đã bao gồm logic sắp xếp: Priority (Critical -> Low) và DueDate (Gần -> Xa).
        /// </summary>
        Task<List<TaskItem>> GetBoardTasksAsync(int projectId);
    }
}
