using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces
{
    /// <summary>
    /// Interface cho Task Repository – trung tâm của hệ thống GD4.
    ///
    /// GD4 bổ sung so với GD1/GD3:
    ///   - GetByIdWithDetailsAsync: eager load đầy đủ cho frmTaskEdit
    ///   - GetByReviewerAsync / GetByTesterAsync: cho màn hình Review/Test
    ///   - UpdateStatusAsync: chuyển trạng thái workflow 10 bước
    ///   - GetLookupDataAsync: lấy Priority, Status, Category 1 lần cho dropdown
    /// </summary>
    public interface ITaskRepository : IRepository<TaskItem>
    {
        // ── Truy vấn đơn ────────────────────────────────────────

        /// <summary>
        /// Lấy task kèm toàn bộ navigation properties.
        /// Dùng cho frmTaskEdit (cần hiển thị Priority, Status, Category, User, SubTasks).
        /// </summary>
        Task<TaskItem?> GetByIdWithDetailsAsync(int taskId);

        // ── Phân trang + lọc đa tiêu chí ───────────────────────

        /// <summary>
        /// Phân trang + lọc kết hợp nhiều tiêu chí (IQueryable filter chain).
        /// Tất cả tham số nullable = tùy chọn, chỉ WHERE khi có giá trị.
        /// Trả về (Items, TotalCount) để tính số trang ở UI.
        /// </summary>
        Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int?    projectId       = null,
            int?    assignedToId    = null,
            int?    statusId        = null,
            int?    priorityId      = null,
            int?    categoryId      = null,
            string? searchKeyword   = null);

        // ── Truy vấn theo người phụ trách ───────────────────────

        /// <summary>
        /// Task chưa hoàn thành được giao cho developer (màn hình "Công việc của tôi").
        /// Sắp xếp: DueDate tăng dần (gần deadline lên đầu).
        /// </summary>
        Task<List<TaskItem>> GetAssignedToUserAsync(int userId);

        /// <summary>
        /// Task đang chờ review lần 1 bởi reviewer chỉ định.
        /// Status phải là REVIEW-1.
        /// </summary>
        Task<List<TaskItem>> GetByReviewer1Async(int reviewer1Id);

        /// <summary>
        /// Task đang chờ review lần 2 bởi reviewer chỉ định.
        /// Status phải là REVIEW-2.
        /// </summary>
        Task<List<TaskItem>> GetByReviewer2Async(int reviewer2Id);

        /// <summary>
        /// Task đang chờ test bởi tester chỉ định.
        /// Status phải là IN-TEST.
        /// </summary>
        Task<List<TaskItem>> GetByTesterAsync(int testerId);

        // ── Truy vấn theo dự án ─────────────────────────────────

        /// <summary>
        /// Tất cả task của 1 dự án.
        /// includeSubTasks = false → chỉ lấy task gốc (ParentTaskId == null).
        /// Sắp xếp: Priority DESC → DueDate ASC.
        /// </summary>
        Task<List<TaskItem>> GetByProjectAsync(int projectId, bool includeSubTasks = false);

        // ── Cảnh báo / Dashboard ─────────────────────────────────

        /// <summary>Task đã quá hạn (DueDate &lt; Now và chưa hoàn thành).</summary>
        Task<List<TaskItem>> GetOverdueAsync();

        /// <summary>Task sắp đến hạn trong N ngày tới (mặc định 7 ngày).</summary>
        Task<List<TaskItem>> GetDueSoonAsync(int days = 7);

        // ── Cập nhật tối ưu (không cần load entity) ─────────────

        /// <summary>
        /// Cập nhật tiến độ % — tự động set IsCompleted, CompletedAt, và StatusId.
        ///   progress = 100  → IsCompleted = true,  CompletedAt = Now, StatusId = RESOLVED
        ///   progress &lt; 100  → IsCompleted = false, CompletedAt = null  (mở lại nếu nhầm)
        /// Dùng ExecuteUpdateAsync — không cần SELECT entity trước.
        /// </summary>
        Task UpdateProgressAsync(int taskId, byte progressPercent, int resolvedStatusId);

        /// <summary>
        /// Chuyển trạng thái workflow — chỉ update StatusId + UpdatedAt.
        /// Dùng ExecuteUpdateAsync — không load entity.
        /// </summary>
        Task UpdateStatusAsync(int taskId, int newStatusId);

        /// <summary>
        /// Gán người review / tester vào task đồng thời chuyển trạng thái.
        /// Dùng ExecuteUpdateAsync — 1 round-trip duy nhất.
        /// </summary>
        Task AssignReviewerAsync(int taskId, int? reviewer1Id, int? reviewer2Id, int? testerId, int newStatusId);

        // ── Thống kê cho Dashboard ────────────────────────────────

        /// <summary>
        /// Đếm task theo Status trong 1 dự án.
        /// GroupBy trực tiếp trên DB → trả về Dictionary (Status.Name → Count).
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryByProjectAsync(int projectId);

        // ── Lookup data cho dropdown ──────────────────────────────

        /// <summary>Lấy tất cả Status (10 bước), sắp theo DisplayOrder.</summary>
        Task<List<Status>> GetAllStatusesAsync();

        /// <summary>Lấy tất cả Priority (4 mức), sắp theo Level.</summary>
        Task<List<Priority>> GetAllPrioritiesAsync();

        /// <summary>Lấy tất cả Category (Bug/Feature/...).</summary>
        Task<List<Category>> GetAllCategoriesAsync();

        Task<List<TaskItem>> GetAllByProjectAsync(int projectId);
    }
}
