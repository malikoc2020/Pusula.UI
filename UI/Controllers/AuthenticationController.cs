using Classes.DTO;
using Classes.Request.AuthenticationRequest;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using IAuthenticationService = Services.AuthenticationService.IAuthenticationService;

namespace UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }
        public IActionResult Login()
        {
            if(TempData["LoginMessage"] is not null)
            {
                ViewData["LoginMessage"]=TempData["LoginMessage"];
                TempData["LoginMessage"] = null;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                // Perform your login logic here
                var res = await _authenticationService.Login(model);

                if (res.IsSuccess)
                {
                    var userDTO = JsonConvert.DeserializeObject<UserDTO>(res.Result.ToString());

                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,userDTO.Name),
                    new Claim(ClaimTypes.Surname,userDTO.SurName),
                    new Claim(ClaimTypes.Email,userDTO.Email),
                    new Claim(ClaimTypes.MobilePhone,userDTO.PhoneNumber),
                    new Claim("AccessToken", userDTO.Token) // Add token claim
                    //new Claim(ClaimTypes.Role,"Admin"),
                    //new Claim(ClaimTypes.Role,"User")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);

                    // Store token in local storage
                    //HttpContext.Response.Headers.Add("BaseAPI-Token", userDTO.Token);
                    // Set token cookie

                    //Response.Cookies.Append("AccessToken", userDTO.Token, new CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    Secure = true,
                    //    SameSite = SameSiteMode.None
                    //});


                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("Error",res.Message);
                }
            }

            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                // Perform your login logic here
                var res = await _authenticationService.Register(model);

                if (res.IsSuccess)
                {
                    TempData["LoginMessage"] = res.Message;
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {

                    ModelState.AddModelError("Error", res.Message);
                    if (res.Result is not null) {
                        List<IdentityError> identityErrors = JsonConvert.DeserializeObject<List<IdentityError>>(res.Result.ToString())??new List<IdentityError>();

                        foreach (var err in identityErrors)
                        {
                            ModelState.AddModelError("Error", err.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
