using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal CurrentBalance { get; set; }
        public int TotalMembers { get; set; }
        public int OpenChallenges { get; set; }
        public int TotalMatches { get; set; }
        public List<Member> TopRanking { get; set; } = new();
        public List<Models.News> LatestNews { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Tính tổng quỹ
            var incomes = await _context.Transactions
                .Where(t => t.Category.Type == "Thu")
                .SumAsync(t => t.Amount);

            var expenses = await _context.Transactions
                .Where(t => t.Category.Type == "Chi")
                .SumAsync(t => t.Amount);

            CurrentBalance = incomes - expenses;

            // Thống kê
            TotalMembers = await _context.Members.CountAsync(m => m.Status == "Active");
            OpenChallenges = await _context.Challenges.CountAsync(c => c.Status == "Open");
            TotalMatches = await _context.Matches.CountAsync();

            // Top 5 cao thủ
            TopRanking = await _context.Members
                .Where(m => m.Status == "Active")
                .OrderByDescending(m => m.RankLevel)
                .Take(5)
                .ToListAsync();

            // Tin tức mới nhất
            LatestNews = await _context.News
                .Where(n => n.IsPublished)
                .OrderByDescending(n => n.PostedDate)
                .Take(3)
                .ToListAsync();
        }
    }
}
