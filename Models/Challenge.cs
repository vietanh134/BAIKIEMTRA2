using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_Challenges")]
    public class Challenge
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Tiêu đề kèo là bắt buộc")]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Mode { get; set; } // 0: Singles, 1: Doubles, 2: MiniGame

        [StringLength(500)]
        public string RewardDescription { get; set; }

        [Range(0, double.MaxValue)]
        public decimal EntryFee { get; set; } = 0;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Open"; // Open, Closed, Completed

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? CompletedDate { get; set; }

        // Foreign Key
        [ForeignKey("CreatorId")]
        public virtual Member Creator { get; set; }

        // Relationships
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

        // Helper property
        [NotMapped]
        public decimal TotalPot
        {
            get
            {
                return Participants.Count * EntryFee;
            }
        }

        [NotMapped]
        public string ModeDisplay
        {
            get
            {
                return Mode switch
                {
                    0 => "Đơn",
                    1 => "Đôi",
                    2 => "Mini-game",
                    _ => "Không xác định"
                };
            }
        }
    }
}
