using PayMopIntegration.Entities;

namespace PayMopIntegration.Interfaces
{
    public interface IPayment
    {
        public Task<Payment> CreatePaymentAsync(Payment payment);
        public Task<Payment> GetPaymentByEnrollmentIdAsync(int enrollmentId);
        public Task UpdatePaymentAsync(Payment payment);
    }
}
