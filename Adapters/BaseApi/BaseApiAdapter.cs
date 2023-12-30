using Classes.Request.AuthenticationRequest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Adapters.BaseApi
{
    public class BaseApiAdapter : IBaseApiAdapter
    {
        private readonly ILogger<BaseApiAdapter> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiURL;
        public BaseApiAdapter(ILogger<BaseApiAdapter> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetAccessToken(httpContextAccessor));
            _apiURL = configuration["BaseApiURL"] ?? "";
        }

        private string GetAccessToken(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext.User.Claims;
            var accessTokenClaim = claims.FirstOrDefault(c => c.Type == "AccessToken");
            return accessTokenClaim != null ? accessTokenClaim.Value : "";
            //return httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];

        }


        #region Authencitation
        public async Task<HttpResponseMessage> Login(LoginRequest loginRequest)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Authentication/login", loginRequest);
        }
        public async Task<HttpResponseMessage> Register(RegisterRequest registerRequest)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Authentication/register", registerRequest);
        }
        #endregion

 

        #region Authencitation
        public async Task<HttpResponseMessage> GetAllUsers()
        {
            return await _httpClient.GetAsync($"{_apiURL}/User/GetAllUsers");

 
        }
        public async Task<HttpResponseMessage> GetUserById(string userId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/User/GetUserById/{userId}");
        }
        #endregion
    }
}
