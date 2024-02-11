using Adapters.BaseApi;
using Classes.DTO;
using Classes.Request.UserRequest;
using Classes.Response.PermissionResponse;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Response;
using System.Net;

namespace Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly ILogger<PermissionService> _logger;
        private readonly IBaseApiAdapter _baseApiAdapter;
        public PermissionService(ILogger<PermissionService> logger, IBaseApiAdapter baseApiAdapter)
        {
            _logger = logger;
            _baseApiAdapter = baseApiAdapter;

        }

        public async Task<BaseResponse> GetAllPermissions()
        {
            var response = await _baseApiAdapter.GetAllPermissions();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<PermissionDTO> users = JsonConvert.DeserializeObject<List<PermissionDTO>>(baseApiResponse.Result.ToString()) ?? new List<PermissionDTO>();
                baseApiResponse.Result = users;

                return baseApiResponse;
            }else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false,"",null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetPermissionById(int permissionId)
        {
            var response = await _baseApiAdapter.GetPermissionById(permissionId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                PermissionDTO permission = JsonConvert.DeserializeObject<PermissionDTO>(baseApiResponse.Result.ToString()) ?? new PermissionDTO();
                baseApiResponse.Result = permission;

                return baseApiResponse;
            }else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            { 
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> InsertPermission(PermissionDTO request)
        {
            var response = await _baseApiAdapter.InsertPermission(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdatePermission(PermissionDTO request)
        {
            var response = await _baseApiAdapter.UpdatePermission(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetAllPermissionTypes()
        {
            var response = await _baseApiAdapter.GetAllPermissionTypes();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<PermissionTypeDTO> users = JsonConvert.DeserializeObject<List<PermissionTypeDTO>>(baseApiResponse.Result.ToString()) ?? new List<PermissionTypeDTO>();
                baseApiResponse.Result = users;

                return baseApiResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
    }
}
