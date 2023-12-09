using Adapters.BaseApi;
using Classes.DTO;
using Classes.Request.AuthenticationRequest;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.AuthenticationService;
using Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                List<UserDTO> users = JsonConvert.DeserializeObject<List<UserDTO>>(baseApiResponse.Result.ToString()) ?? new List<UserDTO>();
                baseApiResponse.Result = users;

                return baseApiResponse;
            }
            return new BaseResponse(false, $"Api Status Code : {response.StatusCode}", null);
        }
        public async Task<BaseResponse> GetUserById(string userId)
        {
            var response = await _baseApiAdapter.GetUserById(userId);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(baseApiResponse.Result.ToString()) ?? new UserDTO();
                baseApiResponse.Result = user;

                return baseApiResponse;
            }
            return new BaseResponse(false, $"Api Status Code : {response.StatusCode}", null);
        }
    }
}
