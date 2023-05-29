using System.Text.Json.Serialization;

namespace CandidateApiProject.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
