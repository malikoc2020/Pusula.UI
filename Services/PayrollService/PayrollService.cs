using Adapters.BaseApi;
using Classes.DTO;
using Classes.Request.UserRequest;
using Classes.Response.PermissionResponse;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Response;
using System.Net;

namespace Services.PayrollService
{
    public class PayrollService : IPayrollService
    {
        private readonly ILogger<PayrollService> _logger;
        private readonly IBaseApiAdapter _baseApiAdapter;
        public PayrollService(ILogger<PayrollService> logger, IBaseApiAdapter baseApiAdapter)
        {
            _logger = logger;
            _baseApiAdapter = baseApiAdapter;

        }

        public async Task<BaseResponse> GetAllPayrollSettings()
        {
            var response = await _baseApiAdapter.GetAllPayrollSettings();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<PayrollSettingDTO> users = JsonConvert.DeserializeObject<List<PayrollSettingDTO>>(baseApiResponse.Result.ToString()) ?? new List<PayrollSettingDTO>();
                baseApiResponse.Result = users;

                return baseApiResponse;
            }else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false,"",null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetPayrollSettingById(int payrollSettingId)
        {
            var response = await _baseApiAdapter.GetPayrollSettingById(payrollSettingId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                PayrollSettingDTO payrollSetting = JsonConvert.DeserializeObject<PayrollSettingDTO>(baseApiResponse.Result.ToString()) ?? new PayrollSettingDTO();
                baseApiResponse.Result = payrollSetting;

                return baseApiResponse;
            }else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            { 
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> InsertPayrollSetting(PayrollSettingDTO request)
        {
            var response = await _baseApiAdapter.InsertPayrollSetting(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdatePayrollSetting(PayrollSettingDTO request)
        {
            var response = await _baseApiAdapter.UpdatePayrollSetting(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> DeletePayrollSetting(int id)
        {
            var response = await _baseApiAdapter.DeletePayrollSetting(id);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse?.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetAllPayrolls()
        {
            var response = await _baseApiAdapter.GetAllPayrolls();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<PayrollDTO> users = JsonConvert.DeserializeObject<List<PayrollDTO>>(baseApiResponse.Result.ToString()) ?? new List<PayrollDTO>();
                baseApiResponse.Result = users;

                return baseApiResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetPayrollById(int payrollId)
        {
            var response = await _baseApiAdapter.GetPayrollById(payrollId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                PayrollDTO payroll = JsonConvert.DeserializeObject<PayrollDTO>(baseApiResponse.Result.ToString()) ?? new PayrollDTO();
                baseApiResponse.Result = payroll;

                return baseApiResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> InsertPayroll(PayrollDTO request)
        {
            var response = await _baseApiAdapter.InsertPayroll(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdatePayroll(PayrollDTO request)
        {
            var response = await _baseApiAdapter.UpdatePayroll(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> DeletePayroll(int id)
        {
            var response = await _baseApiAdapter.DeletePayroll(id);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse?.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetAllPayrollTemps()
        {
            var response = await _baseApiAdapter.GetAllPayrollTemps();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<PayrollTempDTO> users = JsonConvert.DeserializeObject<List<PayrollTempDTO>>(baseApiResponse.Result.ToString()) ?? new List<PayrollTempDTO>();
                baseApiResponse.Result = users;

                return baseApiResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetPayrollTempById(int payrollTempId)
        {
            var response = await _baseApiAdapter.GetPayrollTempById(payrollTempId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                PayrollTempDTO payrollTemp = JsonConvert.DeserializeObject<PayrollTempDTO>(baseApiResponse.Result.ToString()) ?? new PayrollTempDTO();
                baseApiResponse.Result = payrollTemp;

                return baseApiResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource. Please login.");
            }
            if (baseApiResponse is null)
            {
                baseApiResponse = new BaseResponse(false, "", null);
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> InsertPayrollTemp(PayrollTempDTO request)
        {
            var response = await _baseApiAdapter.InsertPayrollTemp(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdatePayrollTemp(PayrollTempDTO request)
        {
            var response = await _baseApiAdapter.UpdatePayrollTemp(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> DeletePayrollTemp(int id)
        {
            var response = await _baseApiAdapter.DeletePayrollTemp(id);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse?.Message}";
            return baseApiResponse;
        }
    }
}
