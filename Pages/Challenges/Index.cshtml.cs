using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Challenges
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Challenge> Challenges { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Challenges = await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }
    }
}
