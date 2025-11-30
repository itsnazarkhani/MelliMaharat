using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelliMaharat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class PersonInformationRoleNamedProperly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonInformation_Role",
                table: "Students",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "PersonInformation_IsDeleted",
                table: "Students",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "PersonInformation_Role",
                table: "Masters",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "PersonInformation_IsDeleted",
                table: "Masters",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Selections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Presentations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Selections");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Students",
                newName: "PersonInformation_Role");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Students",
                newName: "PersonInformation_IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Masters",
                newName: "PersonInformation_Role");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Masters",
                newName: "PersonInformation_IsDeleted");

            migrationBuilder.AlterColumn<int>(
                name: "PersonInformation_Role",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonInformation_Role",
                table: "Masters",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
