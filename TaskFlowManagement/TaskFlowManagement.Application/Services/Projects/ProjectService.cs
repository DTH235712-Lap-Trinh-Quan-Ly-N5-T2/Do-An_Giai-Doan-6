using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;

namespace TaskFlowManagement.Core.Services.Projects
{
    /// <summary>
    /// Nghiệp vụ quản lý dự án: tạo/sửa/xóa dự án, đổi trạng thái, quản lý thành viên.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
            => _projectRepo = projectRepo;

        // Danh sách dự án theo quyền
        public Task<List<Project>> GetProjectsForUserAsync(int userId, bool isManager)
            => isManager ? _projectRepo.GetAllAsync() : _projectRepo.GetByMemberAsync(userId);

        // Chi tiết đầy đủ
        public Task<Project?> GetProjectDetailsAsync(int projectId)
            => _projectRepo.GetWithDetailsAsync(projectId);

        // Tạo dự án mới
        public async Task<(bool Success, string Message)> CreateProjectAsync(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                return (false, "Tên dự án không được để trống.");

            await _projectRepo.AddAsync(project);
            return (true, $"Dự án \"{project.Name}\" đã được tạo thành công.");
        }

        // Cập nhật dự án
        public async Task<(bool Success, string Message)> UpdateProjectAsync(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                return (false, "Tên dự án không được để trống.");

            await _projectRepo.UpdateAsync(project);
            return (true, "Cập nhật dự án thành công.");
        }

        // Xóa dự án – kiểm tra có task chưa
        public async Task<(bool Success, string Message)> DeleteProjectAsync(int projectId)
        {
            if (await _projectRepo.HasTasksAsync(projectId))
                return (false, "Không thể xóa dự án đã có công việc. Hãy xóa công việc trước hoặc chuyển trạng thái Cancelled.");

            await _projectRepo.DeleteAsync(projectId);
            return (true, "Xóa dự án thành công.");
        }

        // Đổi trạng thái dự án
        public async Task<(bool Success, string Message)> ChangeStatusAsync(int projectId, string newStatus)
        {
            var validStatuses = new[] { "NotStarted", "InProgress", "OnHold", "Completed", "Cancelled" };
            if (!validStatuses.Contains(newStatus))
                return (false, $"Trạng thái \"{newStatus}\" không hợp lệ.");

            await _projectRepo.UpdateStatusAsync(projectId, newStatus);
            return (true, $"Đã chuyển trạng thái thành {newStatus}.");
        }

        // ── Quản lý thành viên ────────────────────────────────

        public Task<List<ProjectMember>> GetMembersAsync(int projectId)
            => _projectRepo.GetMembersAsync(projectId);

        // Thêm thành viên – kiểm tra trùng
        public async Task<(bool Success, string Message)> AddMemberAsync(
            int projectId, int userId, string projectRole)
        {
            // Kiểm tra đã là thành viên chưa
            var members = await _projectRepo.GetMembersAsync(projectId);
            if (members.Any(m => m.UserId == userId))
                return (false, "Người dùng đã là thành viên của dự án này.");

            var member = new ProjectMember
            {
                ProjectId   = projectId,
                UserId      = userId,
                ProjectRole = projectRole,
                JoinedAt    = DateTime.UtcNow
            };
            await _projectRepo.AddMemberAsync(member);
            return (true, "Thêm thành viên thành công.");
        }

        // Xóa thành viên (soft delete: set LeftAt)
        public async Task<(bool Success, string Message)> RemoveMemberAsync(int projectId, int userId)
        {
            await _projectRepo.RemoveMemberAsync(projectId, userId);
            return (true, "Đã xóa thành viên khỏi dự án.");
        }
    }
}
