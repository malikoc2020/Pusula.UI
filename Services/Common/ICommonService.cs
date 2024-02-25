using Services.Response;

namespace Services.CommonService
{
    public interface ICommonService
    {
        Task<BaseResponse> GetAllProvinces();
        Task<BaseResponse> GetAllDistricts();
    }
}
