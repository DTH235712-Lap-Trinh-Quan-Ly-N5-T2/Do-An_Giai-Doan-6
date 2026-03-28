using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces
{
    /// <summary>
    /// Interface cho Customer repository.
    /// Bổ sung Search và GetWithProjects ngoài CRUD cơ bản.
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// Tìm kiếm khách hàng theo từ khóa trên CompanyName, ContactName, Email.
        /// Dùng Contains → sinh ra LIKE %keyword% trong SQL.
        /// </summary>
        Task<List<Customer>> SearchAsync(string keyword);

        /// <summary>
        /// Lấy thông tin khách hàng kèm danh sách dự án (eager load Projects → Owner).
        /// Dùng cho form chi tiết khách hàng, tránh N+1 query.
        /// </summary>
        Task<Customer?> GetWithProjectsAsync(int customerId);
    }
}
