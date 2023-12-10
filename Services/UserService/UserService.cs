using Adapters.BaseApi;
using Classes.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Response;
using System.Net;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseApiAdapter _baseApiAdapter;
        public UserService(ILogger<UserService> logger, IBaseApiAdapter baseApiAdapter)
        {
            _logger = logger;
            _baseApiAdapter = baseApiAdapter;

        }

        public async Task<BaseResponse> GetAllUsers()
        {
            var response = await _baseApiAdapter.GetAllUsers();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<UserDTO> users = JsonConvert.DeserializeObject<List<UserDTO>>(baseApiResponse.Result.ToString()) ?? new List<UserDTO>();
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
        public async Task<BaseResponse> GetUserById(string userId)
        {
            var response = await _baseApiAdapter.GetUserById(userId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(baseApiResponse.Result.ToString()) ?? new UserDTO();
                baseApiResponse.Result = user;

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
    }
}
