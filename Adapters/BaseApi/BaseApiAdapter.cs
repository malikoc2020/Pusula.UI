using Classes.Request.AuthenticationRequest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Classes.Request.UserRequest;
using Classes.DTO;

namespace Adapters.BaseApi
{
    public class BaseApiAdapter : IBaseApiAdapter
    {
        private readonly ILogger<BaseApiAdapter> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiURL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseApiAdapter(ILogger<BaseApiAdapter> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetClaimByType(httpContextAccessor, "AccessToken"));
            _apiURL = configuration["BaseApiURL"] ?? "";
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetClaimByType(IHttpContextAccessor httpContextAccessor, string claimType)
        {
            var claims = httpContextAccessor.HttpContext.User.Claims;
            var accessTokenClaim = claims.FirstOrDefault(c => c.Type == claimType);
            return accessTokenClaim != null ? accessTokenClaim.Value : "";
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


        #region User
        public async Task<HttpResponseMessage> GetAllUsers()
        {
            return await _httpClient.GetAsync($"{_apiURL}/User/GetAllUsers");
        }
        public async Task<HttpResponseMessage> GetUserById(string userId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/User/GetUserById/{userId}");
        }
        public async Task<HttpResponseMessage> GetUserByIdForUserEdit(string userId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/User/GetUserByIdForUserEdit/{userId}");
        }
        public async Task<HttpResponseMessage> VerifyPhone(VerifyRequest verifyRequest)
        {
            verifyRequest.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/User/verifyPhone", verifyRequest);
        }
        public async Task<HttpResponseMessage> SendVerifyCode()
        {
            string phoneNumber = GetClaimByType(_httpContextAccessor, ClaimTypes.MobilePhone);
            return await _httpClient.GetAsync($"{_apiURL}/User/sendVerifyCode/{phoneNumber}");
        }
        #endregion

        public async Task<HttpResponseMessage> UpdateUser(UserUpdateRequest request)
        {
            request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/User/updateUser", request);
        }

        public async Task<HttpResponseMessage> GetAllPermissions()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Permission/GetAllPermissions");
        }
        public async Task<HttpResponseMessage> GetPermissionById(int permissionId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/Permission/GetPermissionById/{permissionId}");
        }
        public async Task<HttpResponseMessage> InsertPermission(PermissionDTO request)
        {
            request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Permission/InsertPermission", request);
        }
        public async Task<HttpResponseMessage> UpdatePermission(PermissionDTO request)
        {
            request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Permission/UpdatePermission", request);
        }
        public async Task<HttpResponseMessage> GetAllPermissionTypes()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Permission/GetAllPermissionTypes");
        }
    }
}
