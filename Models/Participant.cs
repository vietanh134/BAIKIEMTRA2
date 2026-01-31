using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_Participants")]
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        // Foreign Keys
        [ForeignKey("ChallengeId")]
        public virtual Challenge Challenge { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}
