using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Member> Members { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Members = await _context.Members
                .Where(m => m.Status == "Active")
                .OrderByDescending(m => m.RankLevel)
                .ToListAsync();
        }
    }
}
