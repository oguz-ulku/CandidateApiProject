using System.Text.Json.Serialization;

namespace CandidateApiProject.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("result")]
        public T Result { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }
        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }
    }
}
