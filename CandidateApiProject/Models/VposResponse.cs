using System.Text.Json.Serialization;

namespace CandidateApiProject.Models
{
    public class VposResponse
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }
        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }
        [JsonPropertyName("bankResponseMessage")]
        public string BankResponseMessage { get; set; }
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }
        [JsonPropertyName("bankOrderId")]
        public string BankOrderId { get; set; }
        [JsonPropertyName("txnType")]
        public string TxnType { get; set; }
        [JsonPropertyName("txnStatus")]
        public string TxnStatus { get; set; }
        [JsonPropertyName("vposId")]
        public int VposId { get; set; }
        [JsonPropertyName("vposName")]
        public string VposName { get; set; }
        [JsonPropertyName("authCode")]
        public string AuthCode { get; set; }
        [JsonPropertyName("hostReference")]
        public string HostReference { get; set; }
        [JsonPropertyName("totalAmount")]
        public string TotalAmount { get; set; }
        [JsonPropertyName("hideResponseTarget")]
        public bool HideResponseTarget { get; set; }
        [JsonPropertyName("saleDate")]
        public int SaleDate { get; set; }
        [JsonPropertyName("paymentSystem")]
        public string PaymentSystem { get; set; }
        [JsonPropertyName("responseHash")]
        public string ResponseHash { get; set; }
        [JsonPropertyName("installmentCount")]
        public int InstallmentCount { get; set; }
    }
}
