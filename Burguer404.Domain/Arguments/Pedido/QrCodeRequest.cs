using System.Text.Json.Serialization;

namespace Burguer404.Domain.Arguments.Pedido
{

    public class QrCodeRequest
    {
        [JsonPropertyName("external_reference")]
        public string ExternalReference { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("notification_url")]
        public string NotificationUrl { get; set; }
        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
        [JsonPropertyName("items")]
        public ItemQrCode[] Itens { get; set; }

    }

    public class ItemQrCode
    {
        [JsonPropertyName("sku_number")]
        public string SkuNumber { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("unit_measure")]
        public string UnitMeasure { get; set; }
        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
    }

}
