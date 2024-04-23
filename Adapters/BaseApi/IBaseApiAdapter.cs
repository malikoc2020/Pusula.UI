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
        Task<HttpResponseMessage> DeletePermission(int id);
        Task<HttpResponseMessage> GetAllWorksites();
        Task<HttpResponseMessage> GetWorksiteById(int worksiteId);
        Task<HttpResponseMessage> InsertWorksite(WorksiteDTO request);
        Task<HttpResponseMessage> UpdateWorksite(WorksiteDTO request);
        Task<HttpResponseMessage> GetAllProvinces();
        Task<HttpResponseMessage> GetAllDistricts();
        Task<HttpResponseMessage> GetAllYears();
        Task<HttpResponseMessage> GetAllMonths();
        Task<HttpResponseMessage> GetAllWorksiteWorkerTypes();
        Task<HttpResponseMessage> GetWorksiteWorkerById(int id);
        Task<HttpResponseMessage> GetWorksiteWorkersByWorksiteId(int worksiteId);
        Task<HttpResponseMessage> InsertWorksiteWorker(WorksiteWorkerDTO request);
        Task<HttpResponseMessage> UpdateWorksiteWorker(WorksiteWorkerDTO request);
        Task<HttpResponseMessage> DeleteWorksiteWorker(int worksiteWorkerId);
        Task<HttpResponseMessage> GetAllWorksiteActionTypes();
        Task<HttpResponseMessage> GetWorksiteActionById(int id);
        Task<HttpResponseMessage> GetWorksiteActionsByWorksiteId(int worksiteId);
        Task<HttpResponseMessage> InsertWorksiteAction(WorksiteActionDTO request);
        Task<HttpResponseMessage> UpdateWorksiteAction(WorksiteActionDTO request);
        Task<HttpResponseMessage> DeleteWorksiteAction(int worksiteActionId);
        Task<HttpResponseMessage> GetAllPayrollSettings();
        Task<HttpResponseMessage> GetPayrollSettingById(int payrollSettingId);
        Task<HttpResponseMessage> InsertPayrollSetting(PayrollSettingDTO request);
        Task<HttpResponseMessage> UpdatePayrollSetting(PayrollSettingDTO request);
        Task<HttpResponseMessage> DeletePayrollSetting(int id);
        Task<HttpResponseMessage> GetAllPayrolls(PayrollFilterDTO request);
        Task<HttpResponseMessage> GetPayrollById(int payrollId);
        Task<HttpResponseMessage> InsertPayroll(PayrollDTO request);
        Task<HttpResponseMessage> UpdatePayroll(PayrollDTO request);
        Task<HttpResponseMessage> DeletePayroll(int id);
        Task<HttpResponseMessage> GetAllPayrollTemps(PayrollTempFilterDTO request);
        Task<HttpResponseMessage> GetPayrollTempById(int payrollTempId);
        Task<HttpResponseMessage> InsertPayrollTemp(PayrollTempDTO request);
        Task<HttpResponseMessage> UpdatePayrollTemp(PayrollTempDTO request);
        Task<HttpResponseMessage> DeletePayrollTemp(int id);
        Task<HttpResponseMessage> Transfer(PayrollTransferDTO request);
        Task<HttpResponseMessage> Refresh(PayrollRefreshDTO request);

    }
}
