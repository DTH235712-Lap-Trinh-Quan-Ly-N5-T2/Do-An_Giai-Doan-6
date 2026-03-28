using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Service quản lý dự án – CRUD + thành viên + đổi trạng thái.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>Danh sách dự án theo quyền (Manager: tất cả, Developer: chỉ dự án mình).</summary>
        Task<List<Project>> GetProjectsForUserAsync(int userId, bool isManager);

        /// <summary>Chi tiết dự án đầy đủ (Members, Tasks, Expenses).</summary>
        Task<Project?> GetProjectDetailsAsync(int projectId);

        Task<(bool Success, string Message)> CreateProjectAsync(Project project);
        Task<(bool Success, string Message)> UpdateProjectAsync(Project project);

        /// <summary>Xóa dự án – chỉ cho phép khi chưa có task.</summary>
        Task<(bool Success, string Message)> DeleteProjectAsync(int projectId);

        /// <summary>Đổi trạng thái dự án (NotStarted/InProgress/OnHold/Completed/Cancelled).</summary>
        Task<(bool Success, string Message)> ChangeStatusAsync(int projectId, string newStatus);

        // ── Quản lý thành viên ────────────────────────────────

        /// <summary>Danh sách thành viên đang active của dự án.</summary>
        Task<List<ProjectMember>> GetMembersAsync(int projectId);

        /// <summary>Thêm developer vào dự án với vai trò chỉ định.</summary>
        Task<(bool Success, string Message)> AddMemberAsync(int projectId, int userId, string projectRole);

        /// <summary>Xóa thành viên khỏi dự án (soft delete: set LeftAt).</summary>
        Task<(bool Success, string Message)> RemoveMemberAsync(int projectId, int userId);
    }
}
