using System.ComponentModel.DataAnnotations;

namespace Classes.Request.UserRequest
{
    public class VerifyRequest
    {
        public string UserId { get; set; } = "";
        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [Display(Name = "Code")]
        public int? Code { get; set; }

        public int? TempVerifyCode { get; set; }
    }
}
