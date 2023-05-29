using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text;
using CandidateApiProject.Interface;
using StringConverter = CandidateApiProject.Utils.StringConverter;
using CandidateApiProject.Models;
using Microsoft.Extensions.Options;

namespace CandidateApiProject.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationSettings _config;

        public string BaseUrl { get; set; }

        public HttpService(HttpClient httpClient, IOptions<ApplicationSettings> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = createRequest(HttpMethod.Post, uri, value);
            await sendRequest(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = createRequest(HttpMethod.Post, uri, value);
            return await sendRequest<T>(request);
        }

        public async Task Put(string uri, object value)
        {
            var request = createRequest(HttpMethod.Put, uri, value);
            await sendRequest(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = createRequest(HttpMethod.Put, uri, value);
            return await sendRequest<T>(request);
        }

        public async Task Delete(string uri)
        {
            var request = createRequest(HttpMethod.Delete, uri);
            await sendRequest(request);
        }

        public async Task<T> Delete<T>(string uri)
        {
            var request = createRequest(HttpMethod.Delete, uri);
            return await sendRequest<T>(request);
        }

        // helper methods

        private HttpRequestMessage createRequest(HttpMethod method, string uri, object value = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (value != null)
                request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return request;
        }

        private async Task sendRequest(HttpRequestMessage request, string token = "")
        {
            await addJwtHeader(request);

            // send request
            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return;
            }

            await handleErrors(response);
        }

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
            if(!request.RequestUri.AbsolutePath.Equals("/api/ppg/Securities/authenticationMerchant"))
                await addJwtHeader(request);

            // send request
            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return default;
            }

            await handleErrors(response);

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new StringConverter());


            return await response.Content.ReadFromJsonAsync<T>(options);
        }

        private async Task addJwtHeader(HttpRequestMessage request)
        {

            var tokenValue = await GetPayzeeToken();

            if (!String.IsNullOrEmpty(tokenValue))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
        }

        private async Task handleErrors(HttpResponseMessage response)
        {
            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {

            }
        }

        private async Task<string> GetPayzeeToken()
        {
            // Payzee Authentication servisine istek yapılır ve token alınır

            try
            {
                var loginRequest = new LoginRequest()
                {
                    ApiKey = _config.Password,
                    Email = _config.Email,
                    Lang = _config.Lang
                };

                var response = await Post<ApiResponse<LoginResponse>>(_config.TokenUrl, loginRequest);


                if (response.StatusCode == 200 && response.Result.Token != null)
                {
                    return response.Result.Token;
                }
                else
                {
                    throw new Exception("Payzee token alınamadı.");
                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }
    }
}
