using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Challenges
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
        public int Mode { get; set; }

        [BindProperty]
        public string RewardDescription { get; set; } = "";

        [BindProperty]
        public string Description { get; set; } = "";

        [BindProperty]
        public decimal EntryFee { get; set; } = 0;

        public IList<Member> Members { get; set; } = new List<Member>();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(Title))
            {
                ErrorMessage = "❌ Tiêu đề kèo không được để trống!";
                Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(RewardDescription))
            {
                ErrorMessage = "❌ Mô tả phần thưởng không được để trống!";
                Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
                return Page();
            }

            if (Mode < 0 || Mode > 2)
            {
                ErrorMessage = "❌ Vui lòng chọn thể thức!";
                Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
                return Page();
            }

            // Nếu là Mini-game, check EntryFee
            if (Mode == 2 && EntryFee <= 0)
            {
                ErrorMessage = "❌ Lệ phí Mini-game phải lớn hơn 0!";
                Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
                return Page();
            }

            try
            {
                // Lấy member đầu tiên làm Creator
                var creator = await _context.Members.FirstOrDefaultAsync(m => m.Status == "Active");
                if (creator == null)
                {
                    ErrorMessage = "❌ Cần ít nhất 1 hội viên để tạo kèo!";
                    Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
                    return Page();
                }

                var challenge = new Challenge
                {
                    Title = Title.Trim(),
                    Description = Description.Trim(),
                    Mode = Mode,
                    RewardDescription = RewardDescription.Trim(),
                    EntryFee = EntryFee,
                    CreatorId = creator.Id,
                    CreatedDate = DateTime.Now,
                    Status = "Open"
                };

                _context.Challenges.Add(challenge);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"❌ Lỗi: {ex.Message}";
                Members = await _context.Members.Where(m => m.Status == "Active").ToListAsync();
                return Page();
            }
        }
    }
}
