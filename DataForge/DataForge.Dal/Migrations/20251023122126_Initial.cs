#nullable disable

namespace DataForge.Dal.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Lessons",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Unit = table.Column<int>(type: "int", nullable: false),
                TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Lessons", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Masters",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Age = table.Column<int>(type: "int", nullable: false),
                NationalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 20, nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 20, nullable: true),
                Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                FullName = table.Column<string>(type: "nvarchar(50)", nullable: true, computedColumnSql: "[FirstName] + ', ' + [LastName]", stored: true),
                Graduation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Masters", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Presentations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                MasterId = table.Column<int>(type: "int", nullable: false),
                LessonId = table.Column<int>(type: "int", nullable: false),
                DayHold = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Presentations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Presentations_Lessons_LessonId",
                    column: x => x.LessonId,
                    principalTable: "Lessons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Presentations_Masters_MasterId",
                    column: x => x.MasterId,
                    principalTable: "Masters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.Sql(@"CREATE VIEW [dbo].[View_LessonInformation] AS
                                   SELECT
                                       L.Name as 'Lesson',
                                       M.FullName as 'Master',
                                       P.DayHold as 'DayHold',
                                       P.StartTime as 'StartTime',
                                       P.EndTime as 'EndTime'  
                                   FROM Presentations P
                                   INNER JOIN Lessons L ON L.Id = P.LessonId
                                   INNER JOIN Masters M ON M.Id = P.MasterId");

        migrationBuilder.CreateIndex(
            name: "IX_Presentations_LessonId",
            table: "Presentations",
            column: "LessonId");

        migrationBuilder.CreateIndex(
            name: "IX_Presentations_MasterId",
            table: "Presentations",
            column: "MasterId");

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Presentations");

        migrationBuilder.DropTable(
            name: "Lessons");

        migrationBuilder.DropTable(
            name: "Masters");

        migrationBuilder.Sql(@"DROP VIEW [dbo].[View_LessonInformation]");
    }
}