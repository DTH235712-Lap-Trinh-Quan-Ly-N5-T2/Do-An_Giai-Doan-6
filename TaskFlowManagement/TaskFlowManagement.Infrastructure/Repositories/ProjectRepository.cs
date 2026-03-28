using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Infrastructure.Data;

namespace TaskFlowManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository cho Project – CRUD dự án + quản lý thành viên.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public ProjectRepository(IDbContextFactory<AppDbContext> contextFactory)
            => _contextFactory = contextFactory;

        // Lấy project kèm Owner + Customer
        public async Task<Project?> GetByIdAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Projects.AsNoTracking()
                .Include(p => p.Owner).Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Chi tiết đầy đủ: Owner, Customer, Members, Tasks, Expenses
        public async Task<Project?> GetWithDetailsAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Projects.AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.Customer)
                .Include(p => p.Members.Where(m => m.LeftAt == null)).ThenInclude(m => m.User)
                .Include(p => p.Tasks).ThenInclude(t => t.Status)
                .Include(p => p.Tasks).ThenInclude(t => t.AssignedTo)
                .Include(p => p.Expenses)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        // Tất cả dự án, mới nhất lên đầu
        public async Task<List<Project>> GetAllAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Projects.AsNoTracking()
                .Include(p => p.Owner).Include(p => p.Customer)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        // Dự án theo Owner
        public async Task<List<Project>> GetByOwnerAsync(int ownerId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Projects.AsNoTracking()
                .Include(p => p.Customer)
                .Where(p => p.OwnerId == ownerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        // Dự án theo thành viên (Developer xem dự án mình tham gia)
        public async Task<List<Project>> GetByMemberAsync(int userId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.ProjectMembers.AsNoTracking()
                .Include(pm => pm.Project).ThenInclude(p => p.Customer)
                .Include(pm => pm.Project).ThenInclude(p => p.Owner)
                .Where(pm => pm.UserId == userId && pm.LeftAt == null)
                .Select(pm => pm.Project)
                .Distinct()
                .ToListAsync();
        }

        // Dự án đang active
        public async Task<List<Project>> GetActiveProjectsAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Projects.AsNoTracking()
                .Include(p => p.Owner).Include(p => p.Customer)
                .Where(p => p.Status == "InProgress" || p.Status == "NotStarted")
                .OrderBy(p => p.PlannedEndDate)
                .ToListAsync();
        }

        // Tổng chi phí
        public async Task<decimal> GetTotalExpenseAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Expenses
                .Where(e => e.ProjectId == projectId)
                .SumAsync(e => e.Amount);
        }

        // Cập nhật trạng thái
        public async Task UpdateStatusAsync(int projectId, string status)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Projects.Where(p => p.Id == projectId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Status, status)
                    .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));
        }

        // Thêm dự án mới
        public async Task AddAsync(Project project)
        {
            using var ctx = _contextFactory.CreateDbContext();
            project.CreatedAt = DateTime.UtcNow;
            project.UpdatedAt = DateTime.UtcNow;
            await ctx.Projects.AddAsync(project);
            await ctx.SaveChangesAsync();
        }

        // Cập nhật dự án, bảo vệ CreatedAt
        public async Task UpdateAsync(Project project)
        {
            using var ctx = _contextFactory.CreateDbContext();
            project.UpdatedAt = DateTime.UtcNow;
            ctx.Projects.Attach(project);
            ctx.Entry(project).State = EntityState.Modified;
            ctx.Entry(project).Property(p => p.CreatedAt).IsModified = false;
            await ctx.SaveChangesAsync();
        }

        // Xóa dự án (cascade xóa Members, Expenses)
        public async Task DeleteAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        // ── Giai đoạn 3: Quản lý thành viên ──────────────────

        // Kiểm tra dự án có task không (dùng trước khi xóa)
        public async Task<bool> HasTasksAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems.AnyAsync(t => t.ProjectId == projectId);
        }

        // Danh sách thành viên đang active
        public async Task<List<ProjectMember>> GetMembersAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.ProjectMembers.AsNoTracking()
                .Include(m => m.User)
                .Where(m => m.ProjectId == projectId && m.LeftAt == null)
                .OrderBy(m => m.JoinedAt)
                .ToListAsync();
        }

        // Thêm thành viên
        public async Task AddMemberAsync(ProjectMember member)
        {
            using var ctx = _contextFactory.CreateDbContext();
            member.JoinedAt = DateTime.UtcNow;
            await ctx.ProjectMembers.AddAsync(member);
            await ctx.SaveChangesAsync();
        }

        // Xóa thành viên (soft delete: set LeftAt)
        public async Task RemoveMemberAsync(int projectId, int userId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.ProjectMembers
                .Where(m => m.ProjectId == projectId && m.UserId == userId && m.LeftAt == null)
                .ExecuteUpdateAsync(s => s.SetProperty(m => m.LeftAt, DateTime.UtcNow));
        }
    }
}
