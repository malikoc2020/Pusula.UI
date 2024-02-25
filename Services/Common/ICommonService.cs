using Services.Response;

namespace Services.CommonService
{
    public interface ICommonService
    {
        Task<BaseResponse> GetAllGetAllProvinces();
        Task<BaseResponse> GetAllDistricts();
    }
}
