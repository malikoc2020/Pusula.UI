using Classes.Request.AuthenticationRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Adapters.BaseApi
{
    public class BaseApiAdapter : IBaseApiAdapter
    {
        private readonly ILogger<BaseApiAdapter> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiURL;
        public BaseApiAdapter(ILogger<BaseApiAdapter> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiURL = configuration["BaseApiURL"]??"";
        }
        public async Task<HttpResponseMessage> Login(LoginRequest loginRequest)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Authentication/login", loginRequest);
        }
        public async Task<HttpResponseMessage> Register(RegisterRequest registerRequest)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Authentication/register", registerRequest);
        }
    }
}
