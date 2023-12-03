using System.ComponentModel.DataAnnotations;

namespace Classes.Request.AuthenticationRequest
{
    public class LoginRequest
    {
        [Required]
        [Display(Name = "LoginName")]
        public string LoginName { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
        public bool RememberMe { get; set; } = false;
    }
}
