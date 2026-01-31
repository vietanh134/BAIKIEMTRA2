using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_Bookings")]
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndTime { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Foreign Key
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}
