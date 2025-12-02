using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelliMaharat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class TermToSelectionTimeNToMRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SelectionTimes_TermId",
                table: "SelectionTimes");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionTimes_TermId",
                table: "SelectionTimes",
                column: "TermId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SelectionTimes_TermId",
                table: "SelectionTimes");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionTimes_TermId",
                table: "SelectionTimes",
                column: "TermId",
                unique: true);
        }
    }
}
