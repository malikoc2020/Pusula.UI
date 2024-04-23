using Classes.DTO;
using Services.Response;

namespace Services.PayrollService
{
    public interface IPayrollService
    {
        Task<BaseResponse> GetAllPayrollSettings();
        Task<BaseResponse> GetPayrollSettingById(int payrollSettingId);
        Task<BaseResponse> InsertPayrollSetting(PayrollSettingDTO request);
        Task<BaseResponse> UpdatePayrollSetting(PayrollSettingDTO request);
        Task<BaseResponse> DeletePayrollSetting(int id);
        Task<BaseResponse> GetAllPayrolls(PayrollFilterDTO request);
        Task<BaseResponse> GetPayrollById(int payrollId);
        Task<BaseResponse> InsertPayroll(PayrollDTO request);
        Task<BaseResponse> UpdatePayroll(PayrollDTO request);
        Task<BaseResponse> DeletePayroll(int id);
        Task<BaseResponse> GetAllPayrollTemps(PayrollTempFilterDTO request);
        Task<BaseResponse> GetPayrollTempById(int payrollTempId);
        Task<BaseResponse> InsertPayrollTemp(PayrollTempDTO request);
        Task<BaseResponse> UpdatePayrollTemp(PayrollTempDTO request);
        Task<BaseResponse> DeletePayrollTemp(int id);
        Task<BaseResponse> Transfer(PayrollTransferDTO request);
        Task<BaseResponse> Refresh(PayrollRefreshDTO request);
    }
}
