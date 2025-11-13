using Microsoft.EntityFrameworkCore;
using PayMopIntegration.Entities;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Payment)
                .WithOne(p => p.Enrollment)
                .HasForeignKey<Payment>(p => p.EnrollmentId);
        }
    }
}
