using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelliMaharat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class PersonInformationRolePropertyRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Masters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
