namespace TaskFlowManagement.Core.Interfaces
{
    /// <summary>
    /// Generic repository interface – tất cả repository đều kế thừa interface này.
    /// Áp dụng Repository Pattern: tách biệt logic truy cập dữ liệu khỏi business logic.
    /// </summary>
    /// <typeparam name="T">Kiểu entity (User, Project, TaskItem, ...)</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>Lấy 1 bản ghi theo khóa chính. Trả về null nếu không tìm thấy.</summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>Lấy toàn bộ danh sách. Dùng GetPagedAsync nếu dữ liệu lớn.</summary>
        Task<List<T>> GetAllAsync();

        /// <summary>Thêm mới 1 bản ghi vào database.</summary>
        Task AddAsync(T entity);

        /// <summary>Cập nhật 1 bản ghi đã tồn tại.</summary>
        Task UpdateAsync(T entity);

        /// <summary>Xóa bản ghi theo ID (hard delete hoặc soft delete tùy repository).</summary>
        Task DeleteAsync(int id);
    }
}
