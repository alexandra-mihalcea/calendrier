using System.Text.Json.Serialization;

namespace Calendrier.Models
{
    public class OrderData
    {
        [JsonPropertyName("orders")]
        public List<OrderItem>? Orders { get; set; }

        [JsonPropertyName("paymentmethods")]
        public List<PaymentMethodItem>? PaymentMethods { get; set; }

        [JsonPropertyName("producttypes")]
        public List<ProductTypeItem> ProductTypes { get; set; } = new();

    }
}
