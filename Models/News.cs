using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMSystem.Models
{
    [Table("044_News")]
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài viết là bắt buộc")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nội dung bài viết là bắt buộc")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string Author { get; set; }

        public bool IsPublished { get; set; } = true;
    }
}
