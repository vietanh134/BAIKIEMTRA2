using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_TransactionCategories")]
    public class TransactionCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Loại giao dịch là bắt buộc")]
        [StringLength(10)]
        public string Type { get; set; } // "Thu" hoặc "Chi"

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Relationships
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
