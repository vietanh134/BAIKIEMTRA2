using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCMSystem.Pages.Challenges
{
    public class AddParticipantsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddParticipantsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int ChallengeId { get; set; }
        public string ChallengeTitle { get; set; } = string.Empty;
        public List<Member> AvailableMembers { get; set; } = new();

        [BindProperty]
        public List<int> SelectedMemberIds { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int challengeId)
        {
            ChallengeId = challengeId;
            var challenge = await _context.Challenges
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Id == challengeId);
            if (challenge == null)
                return NotFound();
            ChallengeTitle = challenge.Title;
            // Lấy các hội viên chưa tham gia kèo này
            var joinedIds = challenge.Participants.Select(p => p.MemberId).ToList();
            AvailableMembers = await _context.Members
                .Where(m => m.Status == "Active" && !joinedIds.Contains(m.Id))
                .OrderBy(m => m.FullName)
                .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var challenge = await _context.Challenges
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Id == ChallengeId);
            if (challenge == null)
                return NotFound();
            if (SelectedMemberIds != null && SelectedMemberIds.Count > 0)
            {
                foreach (var memberId in SelectedMemberIds)
                {
                    if (!challenge.Participants.Any(p => p.MemberId == memberId))
                    {
                        _context.Participants.Add(new Participant
                        {
                            ChallengeId = ChallengeId,
                            MemberId = memberId,
                            JoinedDate = System.DateTime.Now
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Details", new { id = ChallengeId });
        }
    }
}
