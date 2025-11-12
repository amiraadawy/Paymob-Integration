using Microsoft.EntityFrameworkCore;
using PayMopIntegration.Context;
using PayMopIntegration.Entities;
using PayMopIntegration.Interfaces;

namespace PayMopIntegration.Repoistory
{
    public class CoursesRepoistory : ICourses
    {
        private readonly AppDbContext _context;
        public CoursesRepoistory(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }
        // Other course-related methods can be implemented here
    }
}
