using System.Text.Json.Serialization;

namespace CandidateApiProject.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("lang")]
        public string Lang { get; set; }
            
    }
}
