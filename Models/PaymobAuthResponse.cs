namespace PayMopIntegration.Models
{
    public class PaymobAuthResponse
    {
        public string Token { get; set; }
    }

    public class PaymobOrderResponse
    {
        public int Id { get; set; }
    }

    public class PaymentKeyResponse
    {
        public string Token { get; set; }
    }

    public class PayRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";
        public string CustomerEmail { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string OrderId { get; set; }
    }
}
