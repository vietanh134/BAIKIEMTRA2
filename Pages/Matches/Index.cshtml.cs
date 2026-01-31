using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Match> Matches { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Matches = await _context.Matches
                .Include(m => m.Winner1)
                .Include(m => m.Winner2)
                .Include(m => m.Loser1)
                .Include(m => m.Loser2)
                .Include(m => m.Challenge)
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();
        }
    }
}
