using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_Matches")]
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime MatchDate { get; set; } = DateTime.Now;

        public int? ChallengeId { get; set; } // Nullable - có thể là trận giao hữu

        [Required]
        public int Format { get; set; } // 0: Singles, 1: Doubles

        [Required]
        public bool IsRanked { get; set; } = false;

        [Required]
        public int Winner1Id { get; set; }

        public int? Winner2Id { get; set; }

        [Required]
        public int Loser1Id { get; set; }

        public int? Loser2Id { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        // Foreign Keys
        [ForeignKey("ChallengeId")]
        public virtual Challenge Challenge { get; set; }

        [ForeignKey("Winner1Id")]
        public virtual Member Winner1 { get; set; }

        [ForeignKey("Winner2Id")]
        public virtual Member Winner2 { get; set; }

        [ForeignKey("Loser1Id")]
        public virtual Member Loser1 { get; set; }

        [ForeignKey("Loser2Id")]
        public virtual Member Loser2 { get; set; }

        // Helper properties
        [NotMapped]
        public string FormatDisplay
        {
            get
            {
                return Format == 0 ? "Đơn" : "Đôi";
            }
        }

        [NotMapped]
        public string RankStatusDisplay
        {
            get
            {
                return IsRanked ? "Có" : "Không";
            }
        }
    }
}
