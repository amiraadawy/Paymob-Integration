using PayMopIntegration.Entities;

namespace PayMopIntegration.Interfaces
{
    public interface IStudent
    {
        Task<Student> GetById(int id);
        // Other student-related methods can be added here
    }
}
