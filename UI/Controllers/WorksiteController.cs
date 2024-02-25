using Classes.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.WorksiteService;

namespace UI.Controllers
{
    public class WorksiteController : Controller
    {
        private readonly ILogger<WorksiteController> _logger;
        private readonly IWorksiteService _worksiteService;
        public WorksiteController(ILogger<WorksiteController> logger, IWorksiteService worksiteService)
        {
            _logger = logger;
            _worksiteService = worksiteService;
        }

        public async Task<IActionResult> Worksites()
        { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWorksites()
        {
            var res = await _worksiteService.GetAllWorksites();
            return Json(res);
        }
        [HttpGet("worksite/GetWorksiteById/{worksiteId}")]
        public async Task<IActionResult> GetWorksiteById(int worksiteId)
        {
            var res = await _worksiteService.GetWorksiteById(worksiteId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertWorksite([FromBody] WorksiteDTO request)
        {
            var res = await _worksiteService.InsertWorksite(request);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWorksite([FromBody] WorksiteDTO request)
        {
            var res = await _worksiteService.UpdateWorksite(request);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWorksiteWorkerTypes()
        {
            var res = await _worksiteService.GetAllWorksiteWorkerTypes();
            return Json(res);
        }
    }
}
