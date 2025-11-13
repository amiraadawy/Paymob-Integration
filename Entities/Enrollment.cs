using System.ComponentModel.DataAnnotations;

namespace PayMopIntegration.Entities
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public  Student  student{ get; set; }

        public int CourseId { get; set; }
        public Course course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Active";

        
        public Payment Payment { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}
