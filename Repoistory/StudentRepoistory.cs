using Microsoft.EntityFrameworkCore;
using PayMopIntegration.Context;
using PayMopIntegration.Entities;
using PayMopIntegration.Interfaces;

namespace PayMopIntegration.Repoistory
{
    public class StudentRepoistory : IStudent
    {
        private readonly AppDbContext _context;
        public StudentRepoistory(AppDbContext context)
        {
            _context = context;

        }
        public async Task<Student> GetById(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }
        // Other student-related methods can be implemented here
    }
}
