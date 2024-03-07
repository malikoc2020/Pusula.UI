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
        Task<HttpResponseMessage> GetAllWorksites();
        Task<HttpResponseMessage> GetWorksiteById(int worksiteId);
        Task<HttpResponseMessage> InsertWorksite(WorksiteDTO request);
        Task<HttpResponseMessage> UpdateWorksite(WorksiteDTO request);
        Task<HttpResponseMessage> GetAllWorksiteWorkerTypes();
        Task<HttpResponseMessage> GetWorksiteWorkerById(int id);
        Task<HttpResponseMessage> GetWorksiteWorkersByWorksiteId(int worksiteId);
        Task<HttpResponseMessage> InsertWorksiteWorker(WorksiteWorkerDTO request);
        Task<HttpResponseMessage> UpdateWorksiteWorker(WorksiteWorkerDTO request);
        Task<HttpResponseMessage> DeleteWorksiteWorker(int worksiteWorkerId);
        Task<HttpResponseMessage> GetAllProvinces();
        Task<HttpResponseMessage> GetAllDistricts();
    }
}
