using Classes.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.PermissionService;

namespace UI.Controllers
{
    public class PermissionController : Controller
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionService _permissionService;
        public PermissionController(ILogger<PermissionController> logger, IPermissionService permissionService)
        {
            _logger = logger;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Permissions()
        { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var res = await _permissionService.GetAllPermissions();
            return Json(res);
        }
        [HttpGet("permission/GetPermissionById/{permissionId}")]
        public async Task<IActionResult> GetPermissionById(int permissionId)
        {
            var res = await _permissionService.GetPermissionById(permissionId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPermission([FromBody] PermissionDTO request)
        {
            var res = await _permissionService.InsertPermission(request);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePermission([FromBody] PermissionDTO request)
        {
            var res = await _permissionService.UpdatePermission(request);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionTypes()
        {
            var res = await _permissionService.GetAllPermissionTypes();
            return Json(res);
        }
    }
}
