using Classes.Request.AuthenticationRequest;
using Classes.Request.UserRequest;
using Services.Response;

namespace Adapters.BaseApi
{
    public interface IBaseApiAdapter
    {
        Task<HttpResponseMessage> Login(LoginRequest loginRequest);
        Task<HttpResponseMessage> Register(RegisterRequest registerRequest);
        Task<HttpResponseMessage> GetAllUsers();
        Task<HttpResponseMessage> GetUserById(string userId);
        Task<HttpResponseMessage> VerifyPhone(VerifyRequest verifyRequest);
        Task<HttpResponseMessage> SendVerifyCode();
        Task<HttpResponseMessage> UpdateUser(UserUpdateRequest request);
    }
}
