using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.Core.Constants;
using TaskFlowManagement.Core.DTOs;

// Namespace "Tasks" (số nhiều) vì "Task" trùng System.Threading.Tasks.Task
namespace TaskFlowManagement.Core.Services.Tasks
{
    /// <summary>
    /// Triển khai ITaskService cho GD4.
    ///
    /// THỰC TẾ SEED DATA (LookupSeeder.cs — thứ tự insert = Id):
    ///   Roles:      Id=1 "Admin" | Id=2 "Manager" | Id=3 "Developer"
    ///   Statuses:   Id=1 "CREATED" | Id=2 "ASSIGNED" | Id=3 "IN-PROGRESS" | Id=4 "FAILED"
    ///               Id=5 "REVIEW-1" | Id=6 "REVIEW-2" | Id=7 "APPROVED" | Id=8 "IN-TEST"
    ///               Id=9 "RESOLVED" | Id=10 "CLOSED"
    ///   Priorities: Id=1 "Low" | Id=2 "Medium" | Id=3 "High" | Id=4 "Critical"
    ///
    /// PHÂN QUYỀN UpdateStatusAsync:
    ///   Admin / Manager → bỏ qua workflow, đổi sang BẤT KỲ status nào
    ///   Developer       → chỉ được đổi task được giao cho mình (AssignedToId == UserId)
    ///
    /// SO SÁNH ROLE: dùng OrdinalIgnoreCase để tránh lỗi casing
    ///   DB seed: "Admin", "Manager", "Developer" (hoa chữ cái đầu)
    ///   AppSession.Roles có thể chứa bất kỳ casing nào → luôn dùng IgnoreCase
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IUserRepository _userRepo;

        public event EventHandler? TaskDataChanged;

        public TaskService(ITaskRepository taskRepo, IUserRepository userRepo)
        {
            _taskRepo = taskRepo ?? throw new ArgumentNullException(nameof(taskRepo));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        private void NotifyTaskDataChanged()
        {
            TaskDataChanged?.Invoke(this, EventArgs.Empty);
        }

        // ══════════════════════════════════════════════════════
        // TRUY VẤN — giữ nguyên 100%
        // ══════════════════════════════════════════════════════

        public Task<TaskItem?> GetByIdAsync(int taskId)
            => _taskRepo.GetByIdWithDetailsAsync(taskId);

        public Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize,
            int? projectId = null, int? assignedToId = null,
            int? statusId  = null, int? priorityId   = null,
            int? categoryId = null, string? keyword   = null)
            => _taskRepo.GetPagedAsync(page, pageSize,
                projectId, assignedToId, statusId, priorityId, categoryId, keyword);

        public Task<List<TaskItem>> GetMyTasksAsync(int userId)
            => _taskRepo.GetAssignedToUserAsync(userId);

        public Task<List<TaskItem>> GetTasksForReviewer1Async(int reviewerId)
            => _taskRepo.GetByReviewer1Async(reviewerId);

        public Task<List<TaskItem>> GetTasksForReviewer2Async(int reviewerId)
            => _taskRepo.GetByReviewer2Async(reviewerId);

        public Task<List<TaskItem>> GetTasksForTesterAsync(int testerId)
            => _taskRepo.GetByTesterAsync(testerId);

        public Task<List<TaskItem>> GetOverdueTasksAsync()
            => _taskRepo.GetOverdueAsync();

        public Task<List<TaskItem>> GetDueSoonTasksAsync(int days = 7)
            => _taskRepo.GetDueSoonAsync(days);

        public Task<Dictionary<string, int>> GetStatusSummaryAsync(int projectId)
            => _taskRepo.GetStatusSummaryByProjectAsync(projectId);

        public Task<DashboardStatsDto> GetDashboardStatsAsync(int? projectId = null)
        {
            return _taskRepo.GetDashboardStatsAsync(projectId);
        }

        public Task<List<BudgetReportDto>> GetBudgetReportAsync(int? projectId = null)
        {
            return _taskRepo.GetBudgetReportAsync(projectId);
        }

        public Task<List<ProgressReportDto>> GetProgressReportAsync(int? projectId = null)
        {
            return _taskRepo.GetProgressReportAsync(projectId);
        }

        public Task<List<TaskItem>> GetAllByProjectAsync(int projectId)
        {
            return _taskRepo.GetByProjectAsync(projectId);
        }

        public Task<List<TaskItem>> GetBoardTasksAsync(int projectId)
        {
            return _taskRepo.GetByProjectAsync(projectId);
        }

        // ══════════════════════════════════════════════════════
        // CRUD
        // ══════════════════════════════════════════════════════

        public async Task<(bool Success, string Message)> CreateTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                return (false, "Tiêu đề công việc không được để trống.");
            if (task.Title.Length > 200)
                return (false, "Tiêu đề không được vượt quá 200 ký tự.");
            if (task.ProjectId <= 0)
                return (false, "Vui lòng chọn dự án cho công việc.");
            if (task.PriorityId <= 0)
                return (false, "Vui lòng chọn mức độ ưu tiên.");
            if (task.StatusId <= 0)
                return (false, "Vui lòng chọn trạng thái.");
            if (task.CategoryId <= 0)
                return (false, "Vui lòng chọn loại công việc.");
            if (task.EstimatedHours.HasValue && task.EstimatedHours.Value <= 0)
                return (false, "Số giờ ước tính phải lớn hơn 0.");
            if (task.DueDate.HasValue && task.DueDate.Value < DateTime.UtcNow.AddDays(-1))
                return (false, "Hạn chót không được là ngày trong quá khứ.");

            task.Title       = task.Title.Trim();
            task.Description = string.IsNullOrWhiteSpace(task.Description)
                ? null : task.Description.Trim();

            await _taskRepo.AddAsync(task);
            NotifyTaskDataChanged();
            return (true, $"Đã tạo công việc \"{task.Title}\" thành công.");
        }

        /// <summary>
        /// Cập nhật task — gọi 2 method Repository riêng biệt để tránh EF tracking conflict:
        ///
        ///   1. UpdateAsync(task)
        ///      Dùng Attach + State.Modified để cập nhật các trường nội dung
        ///      (Title, Description, AssignedToId, PriorityId, DueDate, v.v.)
        ///      Tuy nhiên khi entity load bằng AsNoTracking có navigation property
        ///      (task.Status, task.Priority là object), EF đôi khi không ghi FK đúng.
        ///
        ///   2. UpdateStatusAsync(task.Id, task.StatusId)
        ///      Dùng ExecuteUpdateAsync — sinh SQL "SET StatusId=? WHERE Id=?" trực tiếp,
        ///      không phụ thuộc vào tracking state → đảm bảo StatusId luôn được ghi đúng.
        ///
        /// Tương tự: AssignedToId cũng được UpdateAsync ghi đúng vì là FK thuần (int?),
        /// không có navigation conflict phức tạp như StatusId.
        /// </summary>
        public async Task<(bool Success, string Message)> UpdateTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                return (false, "Tiêu đề công việc không được để trống.");

            task.Title = task.Title.Trim();
            task.Description = string.IsNullOrWhiteSpace(task.Description) ? null : task.Description.Trim();

            try
            {
                await _taskRepo.UpdateAsync(task); // ← Một lần duy nhất, đúng hoàn toàn
                NotifyTaskDataChanged();
                return (true, "Cập nhật công việc thành công.");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi lưu vào Database: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteTaskAsync(int taskId)
        {
            var task = await _taskRepo.GetByIdWithDetailsAsync(taskId);
            if (task == null)
                return (false, "Không tìm thấy công việc.");
            if (task.SubTasks.Count > 0)
                return (false,
                    $"Không thể xóa vì công việc này có {task.SubTasks.Count} công việc con. " +
                    "Hãy xóa các công việc con trước.");

            await _taskRepo.DeleteAsync(taskId);
            NotifyTaskDataChanged();
            return (true, $"Đã xóa công việc \"{task.Title}\".");
        }

        // ══════════════════════════════════════════════════════
        // CẬP NHẬT TIẾN ĐỘ
        // ══════════════════════════════════════════════════════
        
        private const int ResolvedStatusId = 9;
        private const int ClosedStatusId = 10;
        public async Task<(bool Success, string Message)> UpdateProgressAsync(
            int taskId, byte progress)
        {
            if (progress > 100)
                return (false, "Tiến độ phải từ 0 đến 100.");
            await _taskRepo.UpdateProgressAsync(taskId, progress, ResolvedStatusId);
            NotifyTaskDataChanged();

            return progress == 100
                ? (true, "🎉 Công việc đã hoàn thành! Trạng thái chuyển sang RESOLVED.")
                : (true, $"Đã cập nhật tiến độ {progress}%.");
        }

        // ══════════════════════════════════════════════════════
        // CẬP NHẬT TRẠNG THÁI — PHÂN QUYỀN LINH HOẠT
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Chuyển trạng thái workflow với phân quyền linh hoạt.
        ///
        /// PHÂN QUYỀN:
        ///   Admin / Manager → được đổi sang BẤT KỲ trong 10 status, bỏ qua workflow.
        ///   Developer       → chỉ được đổi task được giao cho mình (AssignedToId == UserId).
        ///
        /// THAM SỐ statusId:
        ///   Truyền Id trực tiếp từ ComboBox (KHÔNG dùng tên string) để tránh lỗi
        ///   so khớp tên khi DB seed hoặc casing thay đổi.
        ///
        /// SO SÁNH ROLE (quan trọng):
        ///   DB seed: "Admin", "Manager", "Developer"
        ///   AppSession.Roles: List&lt;string&gt; — dùng OrdinalIgnoreCase để không lỗi
        ///   nếu source nào đó trả về "admin", "MANAGER", "developer"...
        /// </summary>
        public async Task<(bool Success, string Message)> UpdateStatusAsync(
            int taskId, int statusId,
            int requesterId, IList<string> requesterRoles)
        {
            // Validate nhanh bằng range (seed data: 1-10)
            if (statusId is < 1 or > 10)
                return (false, $"Trạng thái không hợp lệ (Id={statusId}). Phải từ 1 đến 10.");

            // Lấy tên status để hiển thị message — chỉ khi cần (sau khi auth pass)
            bool isManagerOrAbove =
                requesterRoles.Any(r => r.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                                        r.Equals("Manager", StringComparison.OrdinalIgnoreCase));

            if (isManagerOrAbove)
            {
                await _taskRepo.UpdateStatusAsync(taskId, statusId);
                NotifyTaskDataChanged();
                return (true, $"Đã chuyển trạng thái sang \"{WorkflowConstants.GetStatusName(statusId)}\".");
            }

            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
                return (false, "Không tìm thấy công việc.");

            if (task.AssignedToId != requesterId)
                return (false,
                    "Bạn chỉ có thể thay đổi trạng thái công việc được giao cho mình.\n" +
                    "Liên hệ Manager nếu cần thay đổi task khác.");

            await _taskRepo.UpdateStatusAsync(taskId, statusId);
            NotifyTaskDataChanged();
            return (true, $"Đã chuyển trạng thái sang \"{WorkflowConstants.GetStatusName(statusId)}\".");
        }

        // ══════════════════════════════════════════════════════
        // ASSIGN & TRANSITION
        // ══════════════════════════════════════════════════════

        public async Task<(bool Success, string Message)> AssignAndTransitionAsync(
            int taskId, string newStatus,
            int? reviewer1Id = null, int? reviewer2Id = null, int? testerId = null)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                return (false, "Tên trạng thái không được để trống.");

            var statuses = await _taskRepo.GetAllStatusesAsync();
            // OrdinalIgnoreCase cho AssignAndTransition vì caller truyền tên string
            var target   = statuses.FirstOrDefault(
                s => s.Name.Equals(newStatus, StringComparison.OrdinalIgnoreCase));

            if (target == null)
                return (false, $"Trạng thái \"{newStatus}\" không hợp lệ.");

            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
                return (false, "Không tìm thấy công việc.");

            if (reviewer1Id.HasValue)
            {
                if (reviewer1Id.Value <= 0)
                    return (false, "Reviewer lần 1 không hợp lệ.");
                var r1 = await _userRepo.GetByIdAsync(reviewer1Id.Value);
                if (r1 == null || !r1.IsActive)
                    return (false, $"Reviewer 1 (Id={reviewer1Id.Value}) không tồn tại hoặc đã bị khóa.");
            }

            if (reviewer2Id.HasValue)
            {
                if (reviewer2Id.Value <= 0)
                    return (false, "Reviewer lần 2 không hợp lệ.");
                var r2 = await _userRepo.GetByIdAsync(reviewer2Id.Value);
                if (r2 == null || !r2.IsActive)
                    return (false, $"Reviewer 2 (Id={reviewer2Id.Value}) không tồn tại hoặc đã bị khóa.");
            }

            if (testerId.HasValue)
            {
                if (testerId.Value <= 0)
                    return (false, "Tester không hợp lệ.");
                var t = await _userRepo.GetByIdAsync(testerId.Value);
                if (t == null || !t.IsActive)
                    return (false, $"Tester (Id={testerId.Value}) không tồn tại hoặc đã bị khóa.");
            }

            await _taskRepo.AssignReviewerAsync(
                taskId, reviewer1Id, reviewer2Id, testerId, target.Id);

            var parts = new List<string>();
            if (reviewer1Id.HasValue) parts.Add("Reviewer 1");
            if (reviewer2Id.HasValue) parts.Add("Reviewer 2");
            if (testerId.HasValue)    parts.Add("Tester");

            var extra = parts.Count > 0 ? $" — đã gán {string.Join(", ", parts)}" : string.Empty;
            NotifyTaskDataChanged();
            return (true, $"Chuyển sang {newStatus}{extra} thành công.");
        }

        // ══════════════════════════════════════════════════════
        // LOOKUP
        // ══════════════════════════════════════════════════════

        public Task<List<Status>>   GetAllStatusesAsync()   => _taskRepo.GetAllStatusesAsync();
        public Task<List<Priority>> GetAllPrioritiesAsync() => _taskRepo.GetAllPrioritiesAsync();
        public Task<List<Category>> GetAllCategoriesAsync() => _taskRepo.GetAllCategoriesAsync();
    }
}
