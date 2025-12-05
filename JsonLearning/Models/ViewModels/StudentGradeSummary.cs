using Microsoft.EntityFrameworkCore;

namespace JsonLearning.Models.ViewModels;

[Keyless]
public class StudentGradeSummary
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ClassName { get; set; }  = string.Empty;
    public int CourseCount { get; set; }
    public double? GradeAverage { get; set; }
}