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
        [HttpGet("worksite/GetWorksiteWorkerById/{id}")]
        public async Task<IActionResult> GetWorksiteWorkerById(int id)
        {
            var res = await _worksiteService.GetWorksiteWorkerById(id);
            return Json(res);
        }
        [HttpGet("worksite/GetWorksiteWorkersByWorksiteId/{worksiteId}")]
        public async Task<IActionResult> GetWorksiteWorkersByWorksiteId(int worksiteId)
        {
            var res = await _worksiteService.GetWorksiteWorkersByWorksiteId(worksiteId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertWorksiteWorker([FromBody] WorksiteWorkerDTO request)
        {
            var res = await _worksiteService.InsertWorksiteWorker(request);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWorksiteWorker([FromBody] WorksiteWorkerDTO request)
        {
            var res = await _worksiteService.UpdateWorksiteWorker(request);
            return Json(res);
        }
        [HttpDelete("worksite/DeleteWorksiteWorker/{worksiteWorkerId}")]
        public async Task<IActionResult> DeleteWorksiteWorker(int worksiteWorkerId)
        {
            var res = await _worksiteService.DeleteWorksiteWorker(worksiteWorkerId);
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
