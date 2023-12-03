using Classes.Request.AuthenticationRequest;
using Services.Response;

namespace Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<BaseResponse> Login(LoginRequest loginRequest);
        Task<BaseResponse> Register(RegisterRequest registerRequest);
    }
}
