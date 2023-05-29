using System.Text.Json.Serialization;

namespace CandidateApiProject.Models
{
    public class Customer:BaseEntity
    {
        [JsonPropertyName("name")]
        public virtual string Name { get; set; }
        [JsonPropertyName("surname")]
        public virtual string Surname { get; set; }
        [JsonPropertyName("birthDate")]
        public virtual string BirthDate { get; set; }
        [JsonPropertyName("identityNo")]
        public virtual string IdentityNo { get; set; }
        [JsonPropertyName("identityNoVerified")]
        public virtual bool IdentityNoVerified { get; set; }
        [JsonPropertyName("status")]
        public virtual bool Status { get; set; }
    }
}
