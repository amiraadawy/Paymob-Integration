using Microsoft.EntityFrameworkCore;

namespace PayMopIntegration.Context
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Student> Students { get; set; }
        public DbSet<Entities.Course> Courses { get; set; }
        public DbSet<Entities.Enrollment> Enrollments { get; set; }
        public DbSet<Entities.Payment> Payments { get; set; }
    }
}
