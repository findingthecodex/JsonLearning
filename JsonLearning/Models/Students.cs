using System.ComponentModel.DataAnnotations;

namespace JsonLearning.Models;

public class Student
{
    public int StudentId { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(50)]
    public string LastName { get; set; }  = string.Empty;
    [Required, MaxLength(50)]
    public string ClassName { get; set; }  = string.Empty;

    public List<Grades>? Grades { get; set; } = new();
    
}