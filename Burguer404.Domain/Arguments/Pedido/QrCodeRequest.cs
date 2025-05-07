using System.Text.Json.Serialization;

namespace Burguer404.Domain.Arguments.Pedido
{

    public class QrCodeRequest
    {
        [JsonPropertyName("external_reference")]
        public string external_reference { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("notification_url")]
        public string notification_url { get; set; }

        [JsonPropertyName("total_amount")]
        public double total_amount { get; set; }

        [JsonPropertyName("items")]
        public List<ItemQrCode> items { get; set; } = new List<ItemQrCode>();

    }

    public class ItemQrCode
    {
        [JsonPropertyName("sku_number")]
        public string sku_number { get; set; }

        [JsonPropertyName("category")]
        public string category { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("unit_price")]
        public double unit_price { get; set; }

        [JsonPropertyName("quantity")]
        public int quantity { get; set; }

        [JsonPropertyName("unit_measure")]
        public string unit_measure { get; set; }

        [JsonPropertyName("total_amount")]
        public double total_amount { get; set; }
    }

}
