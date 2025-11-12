using PayMopIntegration.Entities;

namespace PayMopIntegration.Interfaces
{
    public interface IEnrollment
    {
        Task<Enrollment> EnrollStudentInCourse(int studentId, int courseId);
    }
}
