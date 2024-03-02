using Classes.DTO;
using Services.Response;

namespace Services.WorksiteService
{
    public interface IWorksiteService
    {
        Task<BaseResponse> GetAllWorksites();
        Task<BaseResponse> GetWorksiteById(int worksiteId);
        Task<BaseResponse> InsertWorksite(WorksiteDTO request);
        Task<BaseResponse> UpdateWorksite(WorksiteDTO request);
        Task<BaseResponse> GetAllWorksiteWorkerTypes();
        Task<BaseResponse> GetWorksiteWorkersById(int worksiteId);
        Task<BaseResponse> InsertWorksiteWorker(WorksiteWorkerDTO request);
        Task<BaseResponse> UpdateWorksiteWorker(WorksiteWorkerDTO request);
        Task<BaseResponse> DeleteWorksiteWorker(int worksiteWorkerId);
    }
}
