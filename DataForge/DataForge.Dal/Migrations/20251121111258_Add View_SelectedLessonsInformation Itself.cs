using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataForge.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddView_SelectedLessonsInformationItself : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
                (
                    @"
                        CREATE VIEW [dbo].[View_SelectedLessonsInformation] AS
                            SELECT 
                                Students.FullName as 'StudentName',
                                Lessons.Name as 'LessonName',
                                Masters.FullName as 'MasterName',
                                Presentations.StartTime as 'LessonStartTime',
                                Presentations.EndTime as 'LessonEndTime',
                                Lessons.Unit as 'LessonUnit',
                                Selections.EducationYear as 'EducationYear',
                                Selections.Score as 'Score'
                            FROM Students
                                INNER JOIN Selections on Selections.StudentId = Students.Id
                                INNER JOIN Presentations on Presentations.Id = Selections.PresentationId
                                INNER JOIN Masters on Masters.Id = Presentations.MasterId
                                INNER JOIN Lessons on Lessons.Id = Presentations.LessonId 
                    "
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[View_SelectedLessonsInformation]");
        }
    }
}
