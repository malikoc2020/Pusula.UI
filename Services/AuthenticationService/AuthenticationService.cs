using Adapters.BaseApi;
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
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> Register(RegisterRequest registerRequest)
        {
            var response = await _baseApiAdapter.Register(registerRequest);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
    }
}
