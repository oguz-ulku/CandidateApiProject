namespace CandidateApiProject.Models
{
    public class HashPayment
    {
        public string HashPassword { get; set; }
        public string UserCode { get; set; }
        public string Rnd { get; set; }
        public string TxnType { get; set; }
        public string TotalAmount { get; set; }
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public string OkUrl { get; set; }
        public string FailUrl { get; set; }
    }
}
