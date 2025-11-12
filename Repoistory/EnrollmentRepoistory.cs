using PayMopIntegration.Context;
using PayMopIntegration.Entities;
using PayMopIntegration.Interfaces;

namespace PayMopIntegration.Repoistory
{
    public class EnrollmentRepoistory:IEnrollment
    {
        private readonly IStudent _studentRepoistory;
        private readonly ICourses _courseRepoistory;
        private readonly AppDbContext _context;

        public EnrollmentRepoistory(IStudent studentRepoistory, ICourses courseRepoistory, AppDbContext context)
        {
            _studentRepoistory = studentRepoistory;
            _courseRepoistory = courseRepoistory;
            _context = context;
        }

        public async Task<Enrollment> EnrollStudentInCourse(int studentId, int courseId)
        {
            var student = await _studentRepoistory.GetById(studentId);
            var course = await _courseRepoistory.GetCourseById(courseId);

            if (student == null || course == null)
            {
                throw new Exception("Student or Course not found");
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return enrollment;
        }
    }
}
