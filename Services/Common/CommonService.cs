using Adapters.BaseApi;
using Classes.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Response;
using System.Net;

namespace Services.CommonService
{
    public class CommonService : ICommonService
    {
        private readonly ILogger<CommonService> _logger;
        private readonly IBaseApiAdapter _baseApiAdapter;
        public CommonService(ILogger<CommonService> logger, IBaseApiAdapter baseApiAdapter)
        {
            _logger = logger;
            _baseApiAdapter = baseApiAdapter;

        }
        public async Task<BaseResponse> GetAllProvinces()
        {
            var response = await _baseApiAdapter.GetAllProvinces();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<ProvinceDTO> users = JsonConvert.DeserializeObject<List<ProvinceDTO>>(baseApiResponse.Result.ToString()) ?? new List<ProvinceDTO>();
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
        public async Task<BaseResponse> GetAllDistricts()
        {
            var response = await _baseApiAdapter.GetAllDistricts();
            var content = await response.Content.ReadAsStringAsync();
            var baseApiResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<DistrictDTO> users = JsonConvert.DeserializeObject<List<DistrictDTO>>(baseApiResponse.Result.ToString()) ?? new List<DistrictDTO>();
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
