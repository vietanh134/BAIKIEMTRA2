using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Matches
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int? ChallengeId { get; set; }

        [BindProperty]
        public int Format { get; set; }

        [BindProperty]
        public int Winner1Id { get; set; }

        [BindProperty]
        public int? Winner2Id { get; set; }

        [BindProperty]
        public int Loser1Id { get; set; }

        [BindProperty]
        public int? Loser2Id { get; set; }

        [BindProperty]
        public bool IsRanked { get; set; }

        [BindProperty]
        public string Notes { get; set; } = "";

        public IList<Challenge> Challenges { get; set; } = new List<Challenge>();
        public IList<Member> Members { get; set; } = new List<Member>();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            Challenges = await _context.Challenges
                .Where(c => c.Status == "Open")
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            Members = await _context.Members
                .Where(m => m.Status == "Active")
                .OrderBy(m => m.FullName)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload data cho case error
            Challenges = await _context.Challenges
                .Where(c => c.Status == "Open")
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            Members = await _context.Members
                .Where(m => m.Status == "Active")
                .OrderBy(m => m.FullName)
                .ToListAsync();

            // Validate người chơi
            if (Format == 0) // Singles
            {
                if (Winner1Id <= 0 || Loser1Id <= 0)
                {
                    ErrorMessage = "❌ Vui lòng chọn người chơi!";
                    return Page();
                }

                if (Winner1Id == Loser1Id)
                {
                    ErrorMessage = "❌ Người thắng và người thua không thể giống nhau!";
                    return Page();
                }

                Winner2Id = null;
                Loser2Id = null;
            }
            else if (Format == 1) // Doubles
            {
                if (Winner1Id <= 0 || Winner2Id <= 0 || Loser1Id <= 0 || Loser2Id <= 0)
                {
                    ErrorMessage = "❌ Vui lòng chọn 4 người chơi!";
                    return Page();
                }

                // Check duplicate
                var playerIds = new List<int> { Winner1Id, Winner2Id.Value, Loser1Id, Loser2Id.Value };
                if (playerIds.Distinct().Count() != 4)
                {
                    ErrorMessage = "❌ Một người không thể xuất hiện 2 lần trong cùng một trận!";
                    return Page();
                }
            }
            else
            {
                ErrorMessage = "❌ Vui lòng chọn thể thức!";
                return Page();
            }

            try
            {
                var match = new Match
                {
                    MatchDate = DateTime.Now,
                    ChallengeId = ChallengeId > 0 ? ChallengeId : null,
                    Format = Format,
                    Winner1Id = Winner1Id,
                    Winner2Id = Winner2Id,
                    Loser1Id = Loser1Id,
                    Loser2Id = Loser2Id,
                    IsRanked = IsRanked,
                    Notes = Notes.Trim()
                };

                // Cập nhật rank nếu là Ranked match
                if (IsRanked)
                {
                    var winner1 = await _context.Members.FindAsync(Winner1Id);
                    var loser1 = await _context.Members.FindAsync(Loser1Id);

                    if (winner1 != null)
                        winner1.RankLevel = Math.Min(10.0, winner1.RankLevel + 0.1);
                    if (loser1 != null)
                        loser1.RankLevel = Math.Max(0.0, loser1.RankLevel - 0.1);

                    if (Format == 1) // Doubles
                    {
                        var winner2 = await _context.Members.FindAsync(Winner2Id);
                        var loser2 = await _context.Members.FindAsync(Loser2Id);

                        if (winner2 != null)
                            winner2.RankLevel = Math.Min(10.0, winner2.RankLevel + 0.1);
                        if (loser2 != null)
                            loser2.RankLevel = Math.Max(0.0, loser2.RankLevel - 0.1);
                    }
                }

                // Đóng kèo nếu là Mini-game
                if (ChallengeId.HasValue && ChallengeId > 0)
                {
                    var challenge = await _context.Challenges
                        .Include(c => c.Participants)
                        .FirstOrDefaultAsync(c => c.Id == ChallengeId);

                    if (challenge != null && challenge.Mode == 2) // Mini-game
                    {
                        challenge.Status = "Completed";
                        challenge.CompletedDate = DateTime.Now;
                    }
                }

                _context.Matches.Add(match);
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
