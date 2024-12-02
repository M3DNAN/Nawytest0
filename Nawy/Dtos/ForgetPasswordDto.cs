using System.ComponentModel.DataAnnotations;

namespace Nawy.Dtos
{
    public class ForgetPasswordDto
    {
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|yahoo\.com|outlook\.com|icloud\.com)$", ErrorMessage = "not valid email")]
        [MaxLength(254, ErrorMessage = "mot valid email")]
        [Required(ErrorMessage = "email  is required")]
        public string Email { get; set; }
    }
}
