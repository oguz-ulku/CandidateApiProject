using System.Text.Json.Serialization;

namespace CandidateApiProject.Models
{
    public class Transaction: BaseEntity
    {
        [JsonPropertyName("transactionId")]
        public virtual int TransactionId { get; set; }
        [JsonPropertyName("customerId")]
        public virtual int CustomerId { get; set; }
        [JsonPropertyName("orderId")]
        public virtual int OrderId { get; set; }
        [JsonPropertyName("typeId")]
        public virtual string TypeId { get; set; }
        [JsonPropertyName("amount")]
        public virtual string Amount { get; set; }
        [JsonPropertyName("cardPan")]
        public virtual string CardPan { get; set; }
        [JsonPropertyName("responseCode")]
        public virtual string ResponseCode { get; set; }
        [JsonPropertyName("responseMessage")]
        public virtual string ResponseMessage { get; set; }
        [JsonPropertyName("status")]
        public virtual bool Status { get; set; }
    }
}
