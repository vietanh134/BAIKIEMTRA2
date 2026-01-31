using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Treasury
{
    public class CreateTransactionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateTransactionModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public string Description { get; set; } = "";

        [BindProperty]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public IList<TransactionCategory> Categories { get; set; } = new List<TransactionCategory>();

        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _context.TransactionCategories.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate
            if (CategoryId <= 0)
            {
                ErrorMessage = "❌ Vui lòng chọn danh mục!";
                Categories = await _context.TransactionCategories.ToListAsync();
                return Page();
            }

            if (Amount <= 0)
            {
                ErrorMessage = "❌ Số tiền phải lớn hơn 0!";
                Categories = await _context.TransactionCategories.ToListAsync();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                ErrorMessage = "❌ Mô tả không được để trống!";
                Categories = await _context.TransactionCategories.ToListAsync();
                return Page();
            }

            try
            {
                // Tạo giao dịch mới
                var transaction = new Transaction
                {
                    CategoryId = CategoryId,
                    Amount = Amount,
                    Description = Description.Trim(),
                    TransactionDate = TransactionDate
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Transactions");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"❌ Lỗi: {ex.Message}";
                Categories = await _context.TransactionCategories.ToListAsync();
                return Page();
            }
        }
    }
}
