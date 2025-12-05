using JsonLearning.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JsonLearning;

public class ShopContext : DbContext
{

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Grades> Grades => Set<Grades>();
    public DbSet<StudentGradeSummary> StudentGradeSummaries => Set<StudentGradeSummary>(); 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, "ShopContext.db");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<StudentGradeSummary>(s =>
        {
            s.HasNoKey();
            s.ToView("StudentGradeSummaryView");
        });
            
            
        modelBuilder.Entity<Student>(s =>
        {
            s.HasKey(x => x.StudentId);
            s.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            s.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            s.Property(x => x.ClassName).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Grades>(g =>
        {
            g.HasKey(x => x.GradeId);
            g.Property(x => x.CourseName).IsRequired().HasMaxLength(50);
            g.Property(x => x.GradeValue).IsRequired();

            g.HasOne(x => x.Student)
                .WithMany(x => x.Grades)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
