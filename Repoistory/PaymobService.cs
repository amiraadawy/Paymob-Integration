using PayMopIntegration.Entities;
using PayMopIntegration.Interfaces;
using PayMopIntegration.Models;
using System.Text;
using System.Text.Json;

namespace PayMopIntegration.Repoistory
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;
        public PaymobService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiBaseUrl = _configuration["Paymob:BaseUrl"];
        }

       
        public async Task<int> CreateOrderAsync (string token, decimal amountCents, string merchantOrderId)
        {
            System.Net.ServicePointManager.SecurityProtocol =
        System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

            var body = new
            {
                auth_token = token,
                delivery_needed = false,
                amount_cents = (int)amountCents,
                currency = "EGP",
                merchant_order_id = merchantOrderId,
                items = Array.Empty<object>()
            };

            Console.WriteLine($"Sending order to: {_apiBaseUrl}/ecommerce/orders");
            Console.WriteLine($"Body: {JsonSerializer.Serialize(body)}");

            var res = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/ecommerce/orders", body);

            Console.WriteLine($"Response: {res.StatusCode}");
            var json = await res.Content.ReadAsStringAsync();
            Console.WriteLine($"Response body: {json}");

            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadAsStringAsync();
            Console.WriteLine($"[PAYMOB ORDER RESPONSE]: {res.StatusCode} - {data}");
            return JsonSerializer.Deserialize<PaymobOrderResponse>(data)?.OrderId ?? 0;
           
        }

        public async Task<string> GeneratePaymentKeyAsync(string token, int orderId, decimal amountCents, Student student)
        {
            var body= new
            {
                auth_token = token,
                amount_cents = (int)amountCents,
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    apartment = "NA",
                    email = student.Email,
                    floor = "NA",
                    first_name = student.FirstName,
                    street = "NA",
                    building = "NA",
                    phone_number ="01010101011",
                    shipping_method = "NA",
                    postal_code = "NA",
                    city = "NA",
                    country = "NA",
                    last_name = student.LastName,
                    state = "NA"
                },
                currency = "EGP",
                integration_id = int.Parse(_configuration["Paymob:IntegrationId"]!)
            };
            var res = await _httpClient.PostAsync($"{_apiBaseUrl}/acceptance/payment_keys",
             new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PaymentKeyResponse>(data)?.Token!;
         
        }

        public async Task<string> GetAuthTokenAsync()
        {
            var body = new
            {
                api_key = _configuration["Paymob:ApiKey"]
            };
            var response =await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/tokens", body);
            response.EnsureSuccessStatusCode();
            var data =await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PaymobAuthResponse>(data)?.Token!;
         
        }

    }
}
