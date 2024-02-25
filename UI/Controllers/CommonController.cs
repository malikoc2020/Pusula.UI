using Classes.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.CommonService;

namespace UI.Controllers
{
    public class CommonController : Controller
    {
        private readonly ILogger<CommonController> _logger;
        private readonly ICommonService _CommonService;
        public CommonController(ILogger<CommonController> logger, ICommonService CommonService)
        {
            _logger = logger;
            _CommonService = CommonService;
        }
 
        [HttpGet]
        public async Task<IActionResult> GetAllProvinces()
        {
            var res = await _CommonService.GetAllProvinces();
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDistricts()
        {
            var res = await _CommonService.GetAllDistricts();
            return Json(res);
        }
    }
}
