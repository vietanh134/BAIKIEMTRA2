using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
        public decimal Amount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // Foreign Key
        [ForeignKey("CategoryId")]
        public virtual TransactionCategory Category { get; set; }
    }
}
