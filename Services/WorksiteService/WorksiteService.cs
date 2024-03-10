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
        public async Task<BaseResponse> GetWorksiteWorkerById(int id)
        {
            var response = await _baseApiAdapter.GetWorksiteWorkerById(id);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                WorksiteWorkerDTO worksite = JsonConvert.DeserializeObject<WorksiteWorkerDTO>(baseApiResponse.Result.ToString()) ?? new WorksiteWorkerDTO();
                baseApiResponse.Result = worksite;

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
        public async Task<BaseResponse> GetWorksiteWorkersByWorksiteId(int worksiteId)
        {
            var response = await _baseApiAdapter.GetWorksiteWorkersByWorksiteId(worksiteId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<WorksiteWorkerDTO> worksite = JsonConvert.DeserializeObject<List<WorksiteWorkerDTO>>(baseApiResponse.Result.ToString()) ?? new List<WorksiteWorkerDTO>();
                baseApiResponse.Result = worksite;

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
        public async Task<BaseResponse> InsertWorksiteWorker(WorksiteWorkerDTO request)
        {
            var response = await _baseApiAdapter.InsertWorksiteWorker(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdateWorksiteWorker(WorksiteWorkerDTO request)
        {
            var response = await _baseApiAdapter.UpdateWorksiteWorker(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> DeleteWorksiteWorker(int worksiteWorkerId)
        {
            var response = await _baseApiAdapter.DeleteWorksiteWorker(worksiteWorkerId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse?.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> GetAllWorksiteActionTypes()
        {
            var response = await _baseApiAdapter.GetAllWorksiteActionTypes();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<WorksiteActionTypeDTO> users = JsonConvert.DeserializeObject<List<WorksiteActionTypeDTO>>(baseApiResponse.Result.ToString()) ?? new List<WorksiteActionTypeDTO>();
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
        public async Task<BaseResponse> GetWorksiteActionById(int id)
        {
            var response = await _baseApiAdapter.GetWorksiteActionById(id);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                WorksiteActionDTO worksite = JsonConvert.DeserializeObject<WorksiteActionDTO>(baseApiResponse.Result.ToString()) ?? new WorksiteActionDTO();
                baseApiResponse.Result = worksite;

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
        public async Task<BaseResponse> GetWorksiteActionsByWorksiteId(int worksiteId)
        {
            var response = await _baseApiAdapter.GetWorksiteActionsByWorksiteId(worksiteId);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<WorksiteActionDTO> worksite = JsonConvert.DeserializeObject<List<WorksiteActionDTO>>(baseApiResponse.Result.ToString()) ?? new List<WorksiteActionDTO>();
                baseApiResponse.Result = worksite;

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
        public async Task<BaseResponse> InsertWorksiteAction(WorksiteActionDTO request)
        {
            var response = await _baseApiAdapter.InsertWorksiteAction(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> UpdateWorksiteAction(WorksiteActionDTO request)
        {
            var response = await _baseApiAdapter.UpdateWorksiteAction(request);
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return baseApiResponse;
            }
            baseApiResponse.Message = $"Api Status Code : {response.StatusCode} {baseApiResponse.Message}";
            return baseApiResponse;
        }
        public async Task<BaseResponse> DeleteWorksiteAction(int worksiteActionId)
        {
            var response = await _baseApiAdapter.DeleteWorksiteAction(worksiteActionId);
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
