using Classes.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.PayrollService;

namespace UI.Controllers
{
    public class PayrollController : Controller
    {
        private readonly ILogger<PayrollController> _logger;
        private readonly IPayrollService _payrollService;
        public PayrollController(ILogger<PayrollController> logger, IPayrollService payrollService)
        {
            _logger = logger;
            _payrollService = payrollService;
        }

        public async Task<IActionResult> PayrollSettings()
        { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayrollSettings()
        {
            var res = await _payrollService.GetAllPayrollSettings();
            return Json(res);
        }
        [HttpGet("payrollSetting/GetPayrollSettingById/{payrollSettingId}")]
        public async Task<IActionResult> GetPayrollSettingById(int payrollSettingId)
        {
            var res = await _payrollService.GetPayrollSettingById(payrollSettingId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPayrollSetting([FromBody] PayrollSettingDTO request)
        {
            var res = await _payrollService.InsertPayrollSetting(request);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePayrollSetting([FromBody] PayrollSettingDTO request)
        {
            var res = await _payrollService.UpdatePayrollSetting(request);
            return Json(res);
        }
        [HttpDelete("payrollSetting/DeletePayrollSetting/{id}")]
        public async Task<IActionResult> DeletePayrollSetting(int id)
        {
            var res = await _payrollService.DeletePayrollSetting(id);
            return Json(res);
        }
        public async Task<IActionResult> Payrolls()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayrolls(PayrollFilterDTO request)
        {
            var res = await _payrollService.GetAllPayrolls(request);
            return Json(res);
        }
        [HttpGet("payroll/GetPayrollById/{payrollId}")]
        public async Task<IActionResult> GetPayrollById(int payrollId)
        {
            var res = await _payrollService.GetPayrollById(payrollId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPayroll([FromBody] PayrollDTO request)
        {
            var res = await _payrollService.InsertPayroll(request);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePayroll([FromBody] PayrollDTO request)
        {
            var res = await _payrollService.UpdatePayroll(request);
            return Json(res);
        }
        [HttpDelete("payroll/DeletePayroll/{id}")]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            var res = await _payrollService.DeletePayroll(id);
            return Json(res);
        }
        public async Task<IActionResult> PayrollTemps()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayrollTemps(PayrollTempFilterDTO request)
        {
            var res = await _payrollService.GetAllPayrollTemps(request);
            return Json(res);
        }
        [HttpGet("payrollTemp/GetPayrollTempById/{payrollTempId}")]
        public async Task<IActionResult> GetPayrollTempById(int payrollTempId)
        {
            var res = await _payrollService.GetPayrollTempById(payrollTempId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPayrollTemp([FromBody] PayrollTempDTO request)
        {
            var res = await _payrollService.InsertPayrollTemp(request);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePayrollTemp([FromBody] PayrollTempDTO request)
        {
            var res = await _payrollService.UpdatePayrollTemp(request);
            return Json(res);
        }
        [HttpDelete("payrollTemp/DeletePayrollTemp/{id}")]
        public async Task<IActionResult> DeletePayrollTemp(int id)
        {
            var res = await _payrollService.DeletePayrollTemp(id);
            return Json(res);
        }
    }
}
