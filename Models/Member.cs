using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_Members")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentityUserId { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100)]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime JoinDate { get; set; }

        [Range(0.0, 10.0)]
        public double RankLevel { get; set; } = 1.0;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active, Inactive

        // Relationships
        public virtual ICollection<Challenge> ChallengesCreated { get; set; } = new List<Challenge>();
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Match> MatchesAsWinner1 { get; set; } = new List<Match>();
        public virtual ICollection<Match> MatchesAsWinner2 { get; set; } = new List<Match>();
        public virtual ICollection<Match> MatchesAsLoser1 { get; set; } = new List<Match>();
        public virtual ICollection<Match> MatchesAsLoser2 { get; set; } = new List<Match>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        // Helper property
        [NotMapped]
        public int AgeCalculated
        {
            get
            {
                if (DOB == null) return 0;
                return DateTime.Now.Year - DOB.Value.Year - (DOB.Value.AddYears(DateTime.Now.Year - DOB.Value.Year) > DateTime.Now ? 1 : 0);
            }
        }

        [NotMapped]
        public string RankBadge
        {
            get
            {
                if (RankLevel > 5.0) return "[PRO]";
                if (RankLevel < 2.0) return "[Newbie]";
                return "";
            }
        }
    }
}
