using System.ComponentModel.DataAnnotations;

namespace TaskFlowManagement.Core.Entities
{
    public class Priority
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>1=Low, 2=Medium, 3=High, 4=Critical</summary>
        public byte Level { get; set; }

        [MaxLength(7)]
        public string? ColorHex { get; set; } // e.g. "#FF5722"

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
