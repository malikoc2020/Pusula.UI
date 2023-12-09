using Classes.Request.AuthenticationRequest;
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
        Task<BaseResponse> GetAllUsers();
        Task<BaseResponse> GetUserById(string userId);

    }
}
