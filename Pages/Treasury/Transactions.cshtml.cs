using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Treasury
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transactions { get; set; } = default!;
        public decimal CurrentBalance { get; set; }
        public string AlertClass { get; set; } = "";

        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            // Tính tổng quỹ
            var incomes = await _context.Transactions
                .Where(t => t.Category.Type == "Thu")
                .SumAsync(t => t.Amount);

            var expenses = await _context.Transactions
                .Where(t => t.Category.Type == "Chi")
                .SumAsync(t => t.Amount);

            CurrentBalance = incomes - expenses;

            // Cảnh báo nếu quỹ âm
            if (CurrentBalance < 0)
            {
                AlertClass = "alert-danger";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
