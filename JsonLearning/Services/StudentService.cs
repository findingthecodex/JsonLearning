using JsonLearning.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JsonLearning.Services;

public class StudentService
{
    public static async Task ListStudentAsync()
    {
        using var db = new ShopContext();
        
        var student = await db.Students
            .AsNoTracking()
            .OrderBy(x => x.StudentId)
            .ToListAsync();
        
        Console.WriteLine("StudentId | FirstName | LastName | Grade | ");
        foreach (var students in student)
        {
            Console.WriteLine($"{students.StudentId} | {students.FirstName} | {students.ClassName} ");
        }
    }
    
    public static async Task ListGradesAsync()
    {
        using var db = new ShopContext();
        
        var Grade = await db.Grades
            .AsNoTracking()
            .OrderBy(x => x.GradeId)
            .Include(x => x.Student)
            .ToListAsync();
        
        Console.WriteLine("StudentId | FirstName | LastName | Grade | ");
        foreach (var grades in Grade)
        {
            Console.WriteLine($"{grades.GradeId} | {grades.Student?.FirstName} | {grades.StudentId} | {grades.GradeValue} | ");
        }
    }

    public static async Task StudentGradeSummaryAsync()
    {
        using var db = new ShopContext();
        
        var gradesummary = await db.StudentGradeSummaries
            .AsNoTracking()
            .OrderBy(x => x.StudentId)
            .ToListAsync();
        
        Console.WriteLine("StudentId | FullName8 | AverageGrade");
        foreach (var grade in gradesummary)
        {
            Console.WriteLine($"{grade.StudentId} | {grade.FullName} | {grade.GradeAverage}");
        }
        Console.WriteLine(" ");
    }
}