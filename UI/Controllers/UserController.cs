using Classes.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Response;
using System.Security.Claims;
using Classes.Request.UserRequest;
using Services.UserService;
using Classes.Response.UserResponse;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task<IActionResult> VerifyPhone()
        {
            var model = new VerifyRequest();
            var verifyCodeResponse = await _userService.SendVerifyCode();
            if (verifyCodeResponse.IsSuccess)
            {
                var verifyCode = JsonConvert.DeserializeObject<VerifyResponse>(verifyCodeResponse.Result.ToString());
                model.TempVerifyCode = verifyCode.Code;
              
                model.PhoneNumber = verifyCode.PhoneNumber;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            if (TempData["VerifyMessage"] is not null)
            {
                ViewData["VerifyMessage"] = TempData["VerifyMessage"];
                TempData["VerifyMessage"] = null;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyPhone([FromForm] VerifyRequest model)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.VerifyPhone(model);

                if (res.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", res.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Users()
        { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _userService.GetAllUsers();
            return Json(res);
        }
        [HttpGet("user/GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var res = await _userService.GetUserById(userId);
            return Json(res);
        }
        [HttpGet("user/GetUserByIdForUserEdit/{userId}")]
        public async Task<IActionResult> GetUserByIdForUserEdit(string userId)
        {
            var res = await _userService.GetUserByIdForUserEdit(userId);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UserUpdate([FromBody] UserUpdateRequest request)
        {
            var res = await _userService.UpdateUser(request);
            return Json(res);
        }
    }
}
