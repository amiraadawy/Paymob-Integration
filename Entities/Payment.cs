using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PayMopIntegration.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; }

        public string TransactionId { get; set; }
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
