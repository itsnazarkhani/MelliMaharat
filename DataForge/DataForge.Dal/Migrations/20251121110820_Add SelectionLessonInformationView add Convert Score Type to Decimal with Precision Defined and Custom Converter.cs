using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataForge.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectionLessonInformationViewaddConvertScoreTypetoDecimalwithPrecisionDefinedandCustomConverter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "Selections",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Selections",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);
        }
    }
}
