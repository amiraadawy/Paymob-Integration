using PayMopIntegration.Entities;

namespace PayMopIntegration.Interfaces
{
    public interface IPaymobService
    {
        public Task<string> GetAuthTokenAsync();
        public Task<int> CreateOrderAsync(string token, decimal amountCents, string merchantOrderId);
        public Task<string> GeneratePaymentKeyAsync(string token, int orderId, decimal amountCents, Student student);

    }
}
