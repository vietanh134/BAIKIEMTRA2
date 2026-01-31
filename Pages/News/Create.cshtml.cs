using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Title { get; set; } = "";

        [BindProperty]
        public string Content { get; set; } = "";

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Manual validation
            if (string.IsNullOrWhiteSpace(Title))
            {
                ErrorMessage = "❌ Tiêu đề bài viết là bắt buộc!";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                ErrorMessage = "❌ Nội dung bài viết là bắt buộc!";
                return Page();
            }

            if (Title.Length > 500)
            {
                ErrorMessage = "❌ Tiêu đề không được vượt quá 500 ký tự!";
                return Page();
            }

            try
            {
                var news = new Models.News
                {
                    Title = Title.Trim(),
                    Content = Content.Trim(),
                    Author = "Admin",
                    PostedDate = DateTime.Now,
                    IsPublished = true
                };

                _context.News.Add(news);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"❌ Lỗi: {ex.Message}";
                return Page();
            }
        }
    }
}
