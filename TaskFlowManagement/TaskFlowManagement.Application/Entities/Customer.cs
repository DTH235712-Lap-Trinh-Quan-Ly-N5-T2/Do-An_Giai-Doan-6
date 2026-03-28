using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    /// <summary>
    /// Khách hàng - đơn vị yêu cầu phát triển dự án
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContactName { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /* ================= RELATIONSHIPS ================= */
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
