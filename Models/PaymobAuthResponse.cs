using System.Text.Json.Serialization;

namespace PayMopIntegration.Models
{
    public class PaymobAuthResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

    public class PaymobOrderResponse
    {
        [JsonPropertyName("id")]
        public int OrderId { get; set; }

        [JsonPropertyName("url")]
        public string PaymentUrl { get; set; }

        [JsonPropertyName("payment_status")]
        public string PaymentStatus { get; set; }

        [JsonPropertyName("merchant_order_id")]
        public string MerchantOrderId { get; set; }
    }

    public class PaymentKeyResponse
    {
        [JsonPropertyName("token")]
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
