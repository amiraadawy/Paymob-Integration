using Microsoft.EntityFrameworkCore;
using PayMopIntegration.Context;
using PayMopIntegration.Entities;
using PayMopIntegration.Interfaces;

namespace PayMopIntegration.Repoistory
{
    public class PaymentRepoistory : IPayment
    {
   
        private readonly AppDbContext _context;
        public PaymentRepoistory(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await  _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> GetPaymentByEnrollmentIdAsync(int enrollmentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.EnrollmentId == enrollmentId);
        }

        public Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            return _context.SaveChangesAsync();
        }
    }
}
