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

        public async Task<HttpResponseMessage> UpdateUser(UserUpdateRequest request)
        {
            request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/User/updateUser", request);
        }
        #endregion

        #region Permission
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
            //request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Permission/InsertPermission", request);
        }
        public async Task<HttpResponseMessage> UpdatePermission(PermissionDTO request)
        {
            //request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Permission/UpdatePermission", request);
        }
        public async Task<HttpResponseMessage> GetAllPermissionTypes()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Permission/GetAllPermissionTypes");
        }
        public async Task<HttpResponseMessage> DeletePermission(int id)
        {
            return await _httpClient.DeleteAsync($"{_apiURL}/Permission/DeletePermission/{id}");
        }
        #endregion

        #region Worksite
        public async Task<HttpResponseMessage> GetAllWorksites()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetAllWorksites");
        }
        public async Task<HttpResponseMessage> GetWorksiteById(int worksiteId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetWorksiteById/{worksiteId}");
        }
        public async Task<HttpResponseMessage> InsertWorksite(WorksiteDTO request)
        {
            request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Worksite/InsertWorksite", request);
        }
        public async Task<HttpResponseMessage> UpdateWorksite(WorksiteDTO request)
        {
            request.UserId = GetClaimByType(_httpContextAccessor, ClaimTypes.PrimarySid);
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Worksite/UpdateWorksite", request);
        }

        public async Task<HttpResponseMessage> GetAllProvinces()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Common/GetAllProvinces");
        }
        public async Task<HttpResponseMessage> GetAllDistricts()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Common/GetAllDistricts");
        }
        public async Task<HttpResponseMessage> GetAllYears()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Common/GetAllYears");
        }
        public async Task<HttpResponseMessage> GetAllMonths()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Common/GetAllMonths");
        }
        #endregion

        #region WorksiteWorker
        public async Task<HttpResponseMessage> GetAllWorksiteWorkerTypes()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetAllWorksiteWorkerTypes");
        }
        public async Task<HttpResponseMessage> GetWorksiteWorkerById(int id)
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetWorksiteWorkerById/{id}");
        }
        public async Task<HttpResponseMessage> GetWorksiteWorkersByWorksiteId(int worksiteId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetWorksiteWorkersByWorksiteId/{worksiteId}");
        }
        public async Task<HttpResponseMessage> InsertWorksiteWorker(WorksiteWorkerDTO request)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Worksite/InsertWorksiteWorker", request);
        }
        public async Task<HttpResponseMessage> UpdateWorksiteWorker(WorksiteWorkerDTO request)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Worksite/UpdateWorksiteWorker", request);
        }
        public async Task<HttpResponseMessage> DeleteWorksiteWorker(int worksiteWorkerId)
        {
            return await _httpClient.DeleteAsync($"{_apiURL}/Worksite/DeleteWorksiteWorker/{worksiteWorkerId}");
        }
        #endregion

        #region WorksiteAction
        public async Task<HttpResponseMessage> GetAllWorksiteActionTypes()
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetAllWorksiteActionTypes");
        }
        public async Task<HttpResponseMessage> GetWorksiteActionById(int id)
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetWorksiteActionById/{id}");
        }
        public async Task<HttpResponseMessage> GetWorksiteActionsByWorksiteId(int worksiteId)
        {
            return await _httpClient.GetAsync($"{_apiURL}/Worksite/GetWorksiteActionsByWorksiteId/{worksiteId}");
        }
        public async Task<HttpResponseMessage> InsertWorksiteAction(WorksiteActionDTO request)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Worksite/InsertWorksiteAction", request);
        }
        public async Task<HttpResponseMessage> UpdateWorksiteAction(WorksiteActionDTO request)
        {
            return await _httpClient.PostAsJsonAsync($"{_apiURL}/Worksite/UpdateWorksiteAction", request);
        }
        public async Task<HttpResponseMessage> DeleteWorksiteAction(int worksiteActionId)
        {
            return await _httpClient.DeleteAsync($"{_apiURL}/Worksite/DeleteWorksiteAction/{worksiteActionId}");
        }
        #endregion
    }
}
