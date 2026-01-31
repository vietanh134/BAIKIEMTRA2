using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Models;

namespace PCMSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cấu hình Foreign Keys và Relationships
            // Member - Challenges (1 to Many)
            builder.Entity<Challenge>()
                .HasOne(c => c.Creator)
                .WithMany(m => m.ChallengesCreated)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Challenge - Participants (1 to Many)
            builder.Entity<Participant>()
                .HasOne(p => p.Challenge)
                .WithMany(c => c.Participants)
                .HasForeignKey(p => p.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Member - Participants (1 to Many)
            builder.Entity<Participant>()
                .HasOne(p => p.Member)
                .WithMany(m => m.Participants)
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Challenge - Matches (1 to Many)
            builder.Entity<Match>()
                .HasOne(m => m.Challenge)
                .WithMany(c => c.Matches)
                .HasForeignKey(m => m.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Member - Match Winners/Losers
            builder.Entity<Match>()
                .HasOne(m => m.Winner1)
                .WithMany(mb => mb.MatchesAsWinner1)
                .HasForeignKey(m => m.Winner1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Winner2)
                .WithMany(mb => mb.MatchesAsWinner2)
                .HasForeignKey(m => m.Winner2Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Loser1)
                .WithMany(mb => mb.MatchesAsLoser1)
                .HasForeignKey(m => m.Loser1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Loser2)
                .WithMany(mb => mb.MatchesAsLoser2)
                .HasForeignKey(m => m.Loser2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // TransactionCategory - Transactions
            builder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(tc => tc.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Member - Bookings
            builder.Entity<Booking>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu ban đầu cho TransactionCategories
            var seedDate = new DateTime(2026, 1, 1);
            builder.Entity<TransactionCategory>().HasData(
                new TransactionCategory { Id = 1, CategoryName = "Tiền sân", Type = "Chi", CreatedDate = seedDate },
                new TransactionCategory { Id = 2, CategoryName = "Mua bóng", Type = "Chi", CreatedDate = seedDate },
                new TransactionCategory { Id = 3, CategoryName = "Nước & đồ uống", Type = "Chi", CreatedDate = seedDate },
                new TransactionCategory { Id = 4, CategoryName = "Quỹ tháng", Type = "Thu", CreatedDate = seedDate },
                new TransactionCategory { Id = 5, CategoryName = "Tài trợ", Type = "Thu", CreatedDate = seedDate },
                new TransactionCategory { Id = 6, CategoryName = "Thu giải đấu", Type = "Thu", CreatedDate = seedDate }
            );
        }
    }
}
