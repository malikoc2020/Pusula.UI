using Classes.Request.AuthenticationRequest;
using Classes.Request.UserRequest;
using Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    public interface IUserService
    {
        Task<BaseResponse> VerifyPhone(VerifyRequest verifyRequest);
        Task<BaseResponse> SendVerifyCode();
        Task<BaseResponse> GetAllUsers();
        Task<BaseResponse> GetUserById(string userId);
        Task<BaseResponse> GetUserByIdForUserEdit(string userId);
        Task<BaseResponse> UpdateUser(UserUpdateRequest request);

    }
}
