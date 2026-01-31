using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.News> NewsList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            NewsList = await _context.News
                .Where(n => n.IsPublished)
                .OrderByDescending(n => n.PostedDate)
                .ToListAsync();
        }
    }
}
