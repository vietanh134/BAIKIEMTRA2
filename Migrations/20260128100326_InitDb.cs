using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCMSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_335_Bookings_335_Members_MemberId",
                table: "335_Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Challenges_335_Members_CreatorId",
                table: "335_Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Matches_335_Challenges_ChallengeId",
                table: "335_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Matches_335_Members_Loser1Id",
                table: "335_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Matches_335_Members_Loser2Id",
                table: "335_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Matches_335_Members_Winner1Id",
                table: "335_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Matches_335_Members_Winner2Id",
                table: "335_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Participants_335_Challenges_ChallengeId",
                table: "335_Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Participants_335_Members_MemberId",
                table: "335_Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_335_Transactions_335_TransactionCategories_CategoryId",
                table: "335_Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_Transactions",
                table: "335_Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_TransactionCategories",
                table: "335_TransactionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_Participants",
                table: "335_Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_News",
                table: "335_News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_Members",
                table: "335_Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_Matches",
                table: "335_Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_Challenges",
                table: "335_Challenges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_335_Bookings",
                table: "335_Bookings");

            migrationBuilder.RenameTable(
                name: "335_Transactions",
                newName: "044_Transactions");

            migrationBuilder.RenameTable(
                name: "335_TransactionCategories",
                newName: "044_TransactionCategories");

            migrationBuilder.RenameTable(
                name: "335_Participants",
                newName: "044_Participants");

            migrationBuilder.RenameTable(
                name: "335_News",
                newName: "044_News");

            migrationBuilder.RenameTable(
                name: "335_Members",
                newName: "044_Members");

            migrationBuilder.RenameTable(
                name: "335_Matches",
                newName: "044_Matches");

            migrationBuilder.RenameTable(
                name: "335_Challenges",
                newName: "044_Challenges");

            migrationBuilder.RenameTable(
                name: "335_Bookings",
                newName: "044_Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_335_Transactions_CategoryId",
                table: "044_Transactions",
                newName: "IX_044_Transactions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_335_Participants_MemberId",
                table: "044_Participants",
                newName: "IX_044_Participants_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_335_Participants_ChallengeId",
                table: "044_Participants",
                newName: "IX_044_Participants_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_335_Matches_Winner2Id",
                table: "044_Matches",
                newName: "IX_044_Matches_Winner2Id");

            migrationBuilder.RenameIndex(
                name: "IX_335_Matches_Winner1Id",
                table: "044_Matches",
                newName: "IX_044_Matches_Winner1Id");

            migrationBuilder.RenameIndex(
                name: "IX_335_Matches_Loser2Id",
                table: "044_Matches",
                newName: "IX_044_Matches_Loser2Id");

            migrationBuilder.RenameIndex(
                name: "IX_335_Matches_Loser1Id",
                table: "044_Matches",
                newName: "IX_044_Matches_Loser1Id");

            migrationBuilder.RenameIndex(
                name: "IX_335_Matches_ChallengeId",
                table: "044_Matches",
                newName: "IX_044_Matches_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_335_Challenges_CreatorId",
                table: "044_Challenges",
                newName: "IX_044_Challenges_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_335_Bookings_MemberId",
                table: "044_Bookings",
                newName: "IX_044_Bookings_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_Transactions",
                table: "044_Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_TransactionCategories",
                table: "044_TransactionCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_Participants",
                table: "044_Participants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_News",
                table: "044_News",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_Members",
                table: "044_Members",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_Matches",
                table: "044_Matches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_Challenges",
                table: "044_Challenges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_044_Bookings",
                table: "044_Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_044_Bookings_044_Members_MemberId",
                table: "044_Bookings",
                column: "MemberId",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Challenges_044_Members_CreatorId",
                table: "044_Challenges",
                column: "CreatorId",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Matches_044_Challenges_ChallengeId",
                table: "044_Matches",
                column: "ChallengeId",
                principalTable: "044_Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Matches_044_Members_Loser1Id",
                table: "044_Matches",
                column: "Loser1Id",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Matches_044_Members_Loser2Id",
                table: "044_Matches",
                column: "Loser2Id",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Matches_044_Members_Winner1Id",
                table: "044_Matches",
                column: "Winner1Id",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Matches_044_Members_Winner2Id",
                table: "044_Matches",
                column: "Winner2Id",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Participants_044_Challenges_ChallengeId",
                table: "044_Participants",
                column: "ChallengeId",
                principalTable: "044_Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Participants_044_Members_MemberId",
                table: "044_Participants",
                column: "MemberId",
                principalTable: "044_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_044_Transactions_044_TransactionCategories_CategoryId",
                table: "044_Transactions",
                column: "CategoryId",
                principalTable: "044_TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_044_Bookings_044_Members_MemberId",
                table: "044_Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Challenges_044_Members_CreatorId",
                table: "044_Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Matches_044_Challenges_ChallengeId",
                table: "044_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Matches_044_Members_Loser1Id",
                table: "044_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Matches_044_Members_Loser2Id",
                table: "044_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Matches_044_Members_Winner1Id",
                table: "044_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Matches_044_Members_Winner2Id",
                table: "044_Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Participants_044_Challenges_ChallengeId",
                table: "044_Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Participants_044_Members_MemberId",
                table: "044_Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_044_Transactions_044_TransactionCategories_CategoryId",
                table: "044_Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_Transactions",
                table: "044_Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_TransactionCategories",
                table: "044_TransactionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_Participants",
                table: "044_Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_News",
                table: "044_News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_Members",
                table: "044_Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_Matches",
                table: "044_Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_Challenges",
                table: "044_Challenges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_044_Bookings",
                table: "044_Bookings");

            migrationBuilder.RenameTable(
                name: "044_Transactions",
                newName: "335_Transactions");

            migrationBuilder.RenameTable(
                name: "044_TransactionCategories",
                newName: "335_TransactionCategories");

            migrationBuilder.RenameTable(
                name: "044_Participants",
                newName: "335_Participants");

            migrationBuilder.RenameTable(
                name: "044_News",
                newName: "335_News");

            migrationBuilder.RenameTable(
                name: "044_Members",
                newName: "335_Members");

            migrationBuilder.RenameTable(
                name: "044_Matches",
                newName: "335_Matches");

            migrationBuilder.RenameTable(
                name: "044_Challenges",
                newName: "335_Challenges");

            migrationBuilder.RenameTable(
                name: "044_Bookings",
                newName: "335_Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_044_Transactions_CategoryId",
                table: "335_Transactions",
                newName: "IX_335_Transactions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_044_Participants_MemberId",
                table: "335_Participants",
                newName: "IX_335_Participants_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_044_Participants_ChallengeId",
                table: "335_Participants",
                newName: "IX_335_Participants_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_044_Matches_Winner2Id",
                table: "335_Matches",
                newName: "IX_335_Matches_Winner2Id");

            migrationBuilder.RenameIndex(
                name: "IX_044_Matches_Winner1Id",
                table: "335_Matches",
                newName: "IX_335_Matches_Winner1Id");

            migrationBuilder.RenameIndex(
                name: "IX_044_Matches_Loser2Id",
                table: "335_Matches",
                newName: "IX_335_Matches_Loser2Id");

            migrationBuilder.RenameIndex(
                name: "IX_044_Matches_Loser1Id",
                table: "335_Matches",
                newName: "IX_335_Matches_Loser1Id");

            migrationBuilder.RenameIndex(
                name: "IX_044_Matches_ChallengeId",
                table: "335_Matches",
                newName: "IX_335_Matches_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_044_Challenges_CreatorId",
                table: "335_Challenges",
                newName: "IX_335_Challenges_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_044_Bookings_MemberId",
                table: "335_Bookings",
                newName: "IX_335_Bookings_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_Transactions",
                table: "335_Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_TransactionCategories",
                table: "335_TransactionCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_Participants",
                table: "335_Participants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_News",
                table: "335_News",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_Members",
                table: "335_Members",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_Matches",
                table: "335_Matches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_Challenges",
                table: "335_Challenges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_335_Bookings",
                table: "335_Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_335_Bookings_335_Members_MemberId",
                table: "335_Bookings",
                column: "MemberId",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Challenges_335_Members_CreatorId",
                table: "335_Challenges",
                column: "CreatorId",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Matches_335_Challenges_ChallengeId",
                table: "335_Matches",
                column: "ChallengeId",
                principalTable: "335_Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Matches_335_Members_Loser1Id",
                table: "335_Matches",
                column: "Loser1Id",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Matches_335_Members_Loser2Id",
                table: "335_Matches",
                column: "Loser2Id",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Matches_335_Members_Winner1Id",
                table: "335_Matches",
                column: "Winner1Id",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Matches_335_Members_Winner2Id",
                table: "335_Matches",
                column: "Winner2Id",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Participants_335_Challenges_ChallengeId",
                table: "335_Participants",
                column: "ChallengeId",
                principalTable: "335_Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Participants_335_Members_MemberId",
                table: "335_Participants",
                column: "MemberId",
                principalTable: "335_Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_335_Transactions_335_TransactionCategories_CategoryId",
                table: "335_Transactions",
                column: "CategoryId",
                principalTable: "335_TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
