using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Infrastructure.Data;

namespace TaskFlowManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Triển khai ICustomerRepository – thao tác dữ liệu cho Customer (Khách hàng).
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public CustomerRepository(IDbContextFactory<AppDbContext> contextFactory)
            => _contextFactory = contextFactory;

        public async Task<Customer?> GetByIdAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        // Danh sách khách hàng, sắp xếp A-Z theo tên công ty
        public async Task<List<Customer>> GetAllAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Customers.AsNoTracking().OrderBy(c => c.CompanyName).ToListAsync();
        }

        // -------------------------------------------------------
        // Tìm kiếm full-text đơn giản trên 3 cột
        // Contains → sinh ra SQL: WHERE CompanyName LIKE '%keyword%'
        // Nullable check trước khi Contains để tránh NullReferenceException
        // -------------------------------------------------------
        public async Task<List<Customer>> SearchAsync(string keyword)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Customers
                .AsNoTracking()
                .Where(c => c.CompanyName.Contains(keyword)
                         || (c.ContactName != null && c.ContactName.Contains(keyword))
                         || (c.Email       != null && c.Email.Contains(keyword)))
                .OrderBy(c => c.CompanyName)
                .ToListAsync();
        }

        // -------------------------------------------------------
        // Lấy khách hàng kèm danh sách dự án (dùng cho form chi tiết)
        // ThenInclude Owner để hiển thị PM của từng dự án
        // -------------------------------------------------------
        public async Task<Customer?> GetWithProjectsAsync(int customerId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Customers
                .AsNoTracking()
                .Include(c => c.Projects).ThenInclude(p => p.Owner)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task AddAsync(Customer customer)
        {
            using var ctx = _contextFactory.CreateDbContext();
            customer.CreatedAt = DateTime.UtcNow;
            await ctx.Customers.AddAsync(customer);
            await ctx.SaveChangesAsync();
        }

        // Cập nhật thông tin khách hàng, bảo vệ CreatedAt
        public async Task UpdateAsync(Customer customer)
        {
            using var ctx = _contextFactory.CreateDbContext();
            ctx.Customers.Attach(customer);
            ctx.Entry(customer).State = EntityState.Modified;
            ctx.Entry(customer).Property(c => c.CreatedAt).IsModified = false;
            await ctx.SaveChangesAsync();
        }

        // Hard delete – có thể đổi thành soft delete nếu cần giữ lịch sử
        public async Task DeleteAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.Customers.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
