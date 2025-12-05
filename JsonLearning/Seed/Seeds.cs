using Microsoft.EntityFrameworkCore;

namespace JsonLearning.Seed;

public class Seeds
{
    public static async Task SeedAsync()
    {
        using var db = new ShopContext();
        await db.Database.MigrateAsync();

        if (!await db.Students.AnyAsync())
        {
            db.Students.AddRange( 
                new Student { FirstName = "Selma", LastName ="Johansson", ClassName = "CHAD12" },
                new Student { FirstName = "John", LastName ="Smith", ClassName = "CHAD12" },
                new Student { FirstName = "Clasius", LastName ="Clay", ClassName = "CHAD12" },
                new Student { FirstName = "Sara", LastName ="Jacob", ClassName = "CHAD12" }
            );
            await db.SaveChangesAsync();
            Console.WriteLine("Student Created");
        }
        
        if(!await db.Grades.AnyAsync())
        {
            db.Grades.AddRange(
                new Grades{ StudentId = 1, CourseName = "Databases", GradeValue = 2 },
                new Grades{ StudentId = 1, CourseName = "Core Programming", GradeValue = 3 },
                new Grades{ StudentId = 1, CourseName = "Web Development", GradeValue = 4 },
                new Grades{ StudentId = 1, CourseName = "Security Analysis", GradeValue = 2 },
                
                new Grades{ StudentId = 2, CourseName = "Databases", GradeValue = 4 },
                new Grades{ StudentId = 2, CourseName = "Core Programming", GradeValue = 3 },
                new Grades{ StudentId = 2, CourseName = "Web Development", GradeValue = 5 },
                new Grades{ StudentId = 2, CourseName = "Security Analysis", GradeValue = 3 },
                
                new Grades{ StudentId = 3, CourseName = "Databases", GradeValue = 4 },
                new Grades{ StudentId = 3, CourseName = "Core Programming", GradeValue = 3 },
                new Grades{ StudentId = 3, CourseName = "Web Development", GradeValue = 4 },
                new Grades{ StudentId = 3, CourseName = "Security Analysis", GradeValue = 2 },
                
                new Grades{ StudentId = 4, CourseName = "Databases", GradeValue = 5 },
                new Grades{ StudentId = 4, CourseName = "Core Programming", GradeValue = 3 },
                new Grades{ StudentId = 4, CourseName = "Web Development", GradeValue = 4 },
                new Grades{ StudentId = 4, CourseName = "Security Analysis", GradeValue = 5 }
            );
            await db.SaveChangesAsync();
            Console.WriteLine("Grade Created");
        }
    }
}