using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;
using PCMSystem.Models;

namespace PCMSystem.Controllers
{
    [Route("api/challenges")]
    [ApiController]
    public class ChallengesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChallengesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/challenges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges()
        {
            return await _context.Challenges
                .Include(c => c.Creator)
                .ToListAsync();
        }

        // GET: api/challenges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallenge(int id)
        {
            var challenge = await _context.Challenges
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            return challenge;
        }

        // PUT: api/challenges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallenge(int id, Challenge challenge)
        {
            if (id != challenge.Id)
            {
                return BadRequest();
            }

            _context.Entry(challenge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/challenges
        [HttpPost]
        public async Task<ActionResult<Challenge>> PostChallenge(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChallenge", new { id = challenge.Id }, challenge);
        }

        // DELETE: api/challenges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            _context.Challenges.Remove(challenge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChallengeExists(int id)
        {
            return _context.Challenges.Any(e => e.Id == id);
        }
    }
}
