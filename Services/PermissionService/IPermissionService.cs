using Classes.DTO;
using Classes.Request.UserRequest;
using Services.Response;

namespace Services.PermissionService
{
    public interface IPermissionService
    {
        Task<BaseResponse> GetAllPermissions();
        Task<BaseResponse> GetPermissionById(int permissionId);
        Task<BaseResponse> InsertPermission(PermissionDTO request);
        Task<BaseResponse> UpdatePermission(PermissionDTO request);
        Task<BaseResponse> GetAllPermissionTypes();
        Task<BaseResponse> DeletePermission(int id);
    }
}
