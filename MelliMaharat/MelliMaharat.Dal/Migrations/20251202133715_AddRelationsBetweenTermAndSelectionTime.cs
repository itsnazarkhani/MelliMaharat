using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelliMaharat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsBetweenTermAndSelectionTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TermId",
                table: "SelectionTimes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SelectionTimes_TermId",
                table: "SelectionTimes",
                column: "TermId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectionTimes_Terms_TermId",
                table: "SelectionTimes",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectionTimes_Terms_TermId",
                table: "SelectionTimes");

            migrationBuilder.DropIndex(
                name: "IX_SelectionTimes_TermId",
                table: "SelectionTimes");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "SelectionTimes");
        }
    }
}
