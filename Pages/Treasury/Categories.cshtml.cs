using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Treasury
{
    public class CategoriesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TransactionCategory> Categories { get; set; } = default!;

        [BindProperty]
        public TransactionCategory NewCategory { get; set; } = new();

        public async Task OnGetAsync()
        {
            Categories = await _context.TransactionCategories
                .OrderBy(c => c.Type)
                .ThenBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (ModelState.IsValid)
            {
                _context.TransactionCategories.Add(NewCategory);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var category = await _context.TransactionCategories
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                if (category.Transactions.Count > 0)
                {
                    ModelState.AddModelError("", "Không thể xóa danh mục đang được sử dụng!");
                    await OnGetAsync();
                    return Page();
                }

                _context.TransactionCategories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
