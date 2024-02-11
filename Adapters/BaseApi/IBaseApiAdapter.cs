using Classes.DTO;
using Classes.Request.AuthenticationRequest;
using Classes.Request.UserRequest;

namespace Adapters.BaseApi
{
    public interface IBaseApiAdapter
    {
        Task<HttpResponseMessage> Login(LoginRequest loginRequest);
        Task<HttpResponseMessage> Register(RegisterRequest registerRequest);
        Task<HttpResponseMessage> GetAllUsers();
        Task<HttpResponseMessage> GetUserById(string userId);
        Task<HttpResponseMessage> GetUserByIdForUserEdit(string userId);
        Task<HttpResponseMessage> VerifyPhone(VerifyRequest verifyRequest);
        Task<HttpResponseMessage> SendVerifyCode();
        Task<HttpResponseMessage> UpdateUser(UserUpdateRequest request);
        Task<HttpResponseMessage> GetAllPermissions();
        Task<HttpResponseMessage> GetPermissionById(int permissionId);
        Task<HttpResponseMessage> InsertPermission(PermissionDTO request);
        Task<HttpResponseMessage> UpdatePermission(PermissionDTO request);
        Task<HttpResponseMessage> GetAllPermissionTypes();
    }
}
