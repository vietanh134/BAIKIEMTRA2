using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Members
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string FullName { get; set; } = "";

        [BindProperty]
        public string PhoneNumber { get; set; } = "";

        [BindProperty]
        public DateTime? DOB { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra validation cơ bản
            if (string.IsNullOrWhiteSpace(FullName))
            {
                ErrorMessage = "❌ Họ tên là bắt buộc!";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                ErrorMessage = "❌ Điện thoại là bắt buộc!";
                return Page();
            }

            // Validate tuổi nếu nhập ngày sinh
            if (DOB.HasValue)
            {
                int age = DateTime.Now.Year - DOB.Value.Year;
                if (age < 10 || age > 80)
                {
                    ErrorMessage = "⚠️ Cẩn thận chấn thương nhé! Tuổi phải từ 10-80.";
                    return Page();
                }
            }

            try
            {
                // Tạo hội viên mới
                var member = new Member
                {
                    FullName = FullName.Trim(),
                    PhoneNumber = PhoneNumber.Trim(),
                    DOB = DOB,
                    IdentityUserId = Guid.NewGuid().ToString(),
                    JoinDate = DateTime.Now,
                    RankLevel = 1.0,
                    Status = "Active"
                };

                _context.Members.Add(member);
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
