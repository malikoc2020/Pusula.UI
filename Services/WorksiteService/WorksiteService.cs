using Adapters.BaseApi;
using Classes.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Response;
using System.Net;

namespace Services.WorksiteService
{
    public class WorksiteService : IWorksiteService
    {
        private readonly ILogger<WorksiteService> _logger;
        private readonly IBaseApiAdapter _baseApiAdapter;
        public WorksiteService(ILogger<WorksiteService> logger, IBaseApiAdapter baseApiAdapter)
        {
            _logger = logger;
            _baseApiAdapter = baseApiAdapter;

        }

        public async Task<BaseResponse> GetAllWorksites()
        {
            var response = await _baseApiAdapter.GetAllWorksites();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<WorksiteDTO> users = JsonConvert.DeserializeObject<List<WorksiteDTO>>(baseApiResponse.Result.ToString()) ?? new List<WorksiteDTO>();
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
        public async Task<BaseResponse> GetWorksiteById(int worksiteId)
        {
            var response = await _baseApiAdapter.GetWorksiteById(worksiteId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                WorksiteDTO worksite = JsonConvert.DeserializeObject<WorksiteDTO>(baseApiResponse.Result.ToString()) ?? new WorksiteDTO();
                baseApiResponse.Result = worksite;

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
        public async Task<BaseResponse> InsertWorksite(WorksiteDTO request)
        {
            var response = await _baseApiAdapter.InsertWorksite(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdateWorksite(WorksiteDTO request)
        {
            var response = await _baseApiAdapter.UpdateWorksite(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetAllWorksiteWorkerTypes()
        {
            var response = await _baseApiAdapter.GetAllWorksiteWorkerTypes();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<WorksiteWorkerTypeDTO> users = JsonConvert.DeserializeObject<List<WorksiteWorkerTypeDTO>>(baseApiResponse.Result.ToString()) ?? new List<WorksiteWorkerTypeDTO>();
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
    }
}
