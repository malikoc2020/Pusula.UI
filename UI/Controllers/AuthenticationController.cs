using Classes.Request.AuthenticationRequest;
using Microsoft.AspNetCore.Mvc;
using Services.AuthenticationService;

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

                }
                else
                {
                    ModelState.AddModelError("Error",res.ErrorMessage);
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

                }
                else
                {
                    ModelState.AddModelError("Error", res.ErrorMessage);
                }
            }
            return View(model);
        }
    }
}
