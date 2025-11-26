using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelliMaharat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeletionPropertyToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PersonInformation_IsDeleted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PersonInformation_IsDeleted",
                table: "Masters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonInformation_IsDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PersonInformation_IsDeleted",
                table: "Masters");
        }
    }
}
