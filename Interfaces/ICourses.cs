using PayMopIntegration.Entities;

namespace PayMopIntegration.Interfaces
{
    public interface ICourses
    {
        Task<Course > GetCourseById(int id);
        // Other course-related methods can be added here
    }
}
