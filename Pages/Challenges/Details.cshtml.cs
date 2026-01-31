using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Pages.Challenges
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Challenge Challenge { get; set; } = default!;
        public List<Member> Participants { get; set; } = new();
        public bool IsUserParticipant { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                    .ThenInclude(p => p.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            Challenge = challenge;
            Participants = challenge.Participants.Select(p => p.Member).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostJoinAsync(int id)
        {
            var challenge = await _context.Challenges
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            if (challenge.Status == "Closed" || challenge.Status == "Completed")
            {
                ModelState.AddModelError("", "Kèo này không còn mở để tham gia!");
                return RedirectToPage(new { id });
            }

            // Lấy IdentityUserId của user hiện tại
            var userId = User?.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "Bạn cần đăng nhập để tham gia kèo!");
                return RedirectToPage(new { id });
            }

            // Tìm Member tương ứng với IdentityUserId
            var member = await _context.Members.FirstOrDefaultAsync(m => m.IdentityUserId == userId && m.Status == "Active");
            if (member == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin hội viên của bạn!");
                return RedirectToPage(new { id });
            }

            // Kiểm tra đã tham gia chưa
            bool alreadyJoined = challenge.Participants.Any(p => p.MemberId == member.Id);
            if (alreadyJoined)
            {
                ModelState.AddModelError("", "Bạn đã tham gia kèo này!");
                return RedirectToPage(new { id });
            }

            // Thêm participant mới
            var participant = new Participant
            {
                ChallengeId = challenge.Id,
                MemberId = member.Id,
                JoinedDate = DateTime.Now
            };
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
            return RedirectToPage(new { id });
        }
    }
}
