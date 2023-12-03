using Adapters.BaseApi;
using Classes.DTO;
using Classes.Request.AuthenticationRequest;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Response;
using System.Net;

namespace Services.AuthenticationService
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IBaseApiAdapter _baseApiAdapter;
        public AuthenticationService(ILogger<AuthenticationService> logger, IBaseApiAdapter baseApiAdapter)
        {
            _logger = logger;
            _baseApiAdapter = baseApiAdapter;

        }

        public async Task<BaseResponse> Login(LoginRequest loginRequest)
        {
            var response = await _baseApiAdapter.Login(loginRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
                var userDTO = baseApiResponse.Result as UserDTO;






                return baseApiResponse;
            }
            return new BaseResponse(false, $"Api Status Code : {response.StatusCode}", null);
        }
        public async Task<BaseResponse> Register(RegisterRequest registerRequest)
        {
            var baseResponse = await _baseApiAdapter.Register(registerRequest);

            if (baseResponse.StatusCode == HttpStatusCode.OK)
            {
                var content = await baseResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BaseResponse>(content);
            }
            return new BaseResponse(false, $"Api Status Code : {baseResponse.StatusCode}", null);
        }
    }
}
