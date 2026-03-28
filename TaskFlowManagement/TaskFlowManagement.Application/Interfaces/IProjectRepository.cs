using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces
{
    /// <summary>
    /// Repository cho Project – CRUD dự án + quản lý thành viên.
    /// </summary>
    public interface IProjectRepository : IRepository<Project>
    {
        /// <summary>Dự án do user sở hữu (Owner/PM).</summary>
        Task<List<Project>> GetByOwnerAsync(int ownerId);

        /// <summary>Dự án mà user là thành viên (qua ProjectMembers, LeftAt == null).</summary>
        Task<List<Project>> GetByMemberAsync(int userId);

        /// <summary>Chi tiết dự án kèm Owner, Customer, Members, Tasks, Expenses.</summary>
        Task<Project?> GetWithDetailsAsync(int projectId);

        /// <summary>Dự án đang hoạt động (InProgress / NotStarted), sắp theo deadline.</summary>
        Task<List<Project>> GetActiveProjectsAsync();

        /// <summary>Tổng chi phí thực tế từ bảng Expenses (SUM trên DB).</summary>
        Task<decimal> GetTotalExpenseAsync(int projectId);

        /// <summary>Cập nhật Status + UpdatedAt (ExecuteUpdateAsync, không load entity).</summary>
        Task UpdateStatusAsync(int projectId, string status);

        // ── Giai đoạn 3: Quản lý thành viên ──────────────────

        /// <summary>Kiểm tra dự án có task chưa (dùng trước khi xóa).</summary>
        Task<bool> HasTasksAsync(int projectId);

        /// <summary>Danh sách thành viên đang active (LeftAt == null) kèm thông tin User.</summary>
        Task<List<ProjectMember>> GetMembersAsync(int projectId);

        /// <summary>Thêm thành viên vào dự án.</summary>
        Task AddMemberAsync(ProjectMember member);

        /// <summary>Đánh dấu thành viên rời dự án (set LeftAt, không xóa thật).</summary>
        Task RemoveMemberAsync(int projectId, int userId);
    }
}
