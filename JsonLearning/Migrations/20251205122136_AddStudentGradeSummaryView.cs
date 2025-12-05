using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonLearning.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentGradeSummaryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW IF NOT EXISTS StudentGradeSummaryView AS
            SELECT
                 s.StudentId,
                 (s.FirstName || ' ' || s.LastName) AS FullName,
                 s.ClassName,
                 COUNT(g.GradeId) AS CourseCount,
                 AVG(g.GradeValue) AS GradeAverage
            FROM Students s
            LEFT JOIN Grades g ON s.StudentId = g.StudentId
            GROUP BY s.StudentId, s.FirstName, s.LastName, s.ClassName
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP VIEW IF EXISTS StudentGradeSummaryView
            ");
        }
    }
}
