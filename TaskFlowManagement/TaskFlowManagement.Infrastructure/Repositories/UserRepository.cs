using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Infrastructure.Data;

namespace TaskFlowManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public UserRepository(IDbContextFactory<AppDbContext> contextFactory)
            => _contextFactory = contextFactory;

        public async Task<User?> GetByIdAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        // Chỉ active — dùng cho dropdown/assign task, KHÔNG dùng cho màn hình quản lý
        public async Task<List<User>> GetAllAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users
                .AsNoTracking()
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }

        // Active + inactive, kèm Roles — dùng cho màn hình Admin quản lý tài khoản
        public async Task<List<User>> GetAllWithRolesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users
                .AsNoTracking()
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderBy(u => u.IsActive ? 0 : 1).ThenBy(u => u.FullName)
                .ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetWithRolesAsync(int userId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users
                .AsNoTracking()
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Users.AnyAsync(u => u.Username == username);
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Users.Where(u => u.Id == userId)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.LastLogin, DateTime.UtcNow));
        }

        public async Task AddAsync(User user)
        {
            using var ctx = _contextFactory.CreateDbContext();
            user.CreatedAt = DateTime.UtcNow;
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
        }

        // UpdateAsync bảo vệ PasswordHash, CreatedAt, IsActive và LastLogin
        public async Task UpdateAsync(User user)
        {
            using var ctx = _contextFactory.CreateDbContext();
            ctx.Users.Attach(user);
            ctx.Entry(user).State = EntityState.Modified;
            ctx.Entry(user).Property(u => u.CreatedAt).IsModified    = false;
            ctx.Entry(user).Property(u => u.PasswordHash).IsModified = false;
            ctx.Entry(user).Property(u => u.IsActive).IsModified     = false;
            ctx.Entry(user).Property(u => u.LastLogin).IsModified    = false;
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Users.Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.IsActive, false));
        }

        public async Task SetActiveAsync(int userId, bool isActive)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Users.Where(u => u.Id == userId)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.IsActive, isActive));
        }

        public async Task UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Users.Where(u => u.Id == userId)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.PasswordHash, newPasswordHash));
        }

        public async Task AddWithRoleAsync(User user, string roleName)
        {
            using var ctx = _contextFactory.CreateDbContext();
            user.CreatedAt = DateTime.UtcNow;

            var role = await ctx.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
                throw new InvalidOperationException($"Role '{roleName}' không tồn tại trong hệ thống.");

            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();

            ctx.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Đổi role cho user: xóa tất cả role cũ, gán role mới.
        /// Ví dụ: Developer → Manager, Manager → Admin, v.v.
        /// </summary>
        public async Task ChangeRoleAsync(int userId, string newRoleName)
        {
            using var ctx = _contextFactory.CreateDbContext();

            // Tìm role mới
            var newRole = await ctx.Roles.FirstOrDefaultAsync(r => r.Name == newRoleName);
            if (newRole == null)
                throw new InvalidOperationException($"Role '{newRoleName}' không tồn tại trong hệ thống.");

            // Xóa tất cả role cũ của user
            await ctx.UserRoles
                .Where(ur => ur.UserId == userId)
                .ExecuteDeleteAsync();

            // Gán role mới
            ctx.UserRoles.Add(new UserRole { UserId = userId, RoleId = newRole.Id });
            await ctx.SaveChangesAsync();
        }
    }
}
