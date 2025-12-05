using System.ComponentModel.DataAnnotations;

namespace JsonLearning.Models;

public class Grades
{
    public int GradeId { get; set; }
    [Required]
    public int StudentId { get; set; }
    [Required]
    public string CourseName { get; set; } = string.Empty;
    [Required] 
    public int? GradeValue { get; set; }
    
    public Student? Student { get; set; }
}