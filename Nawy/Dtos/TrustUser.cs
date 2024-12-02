using System.ComponentModel.DataAnnotations;

namespace Nawy.Dtos
{
    public class TrustUser
    {
        [RegularExpression(@"^[23]\d{13}$", ErrorMessage = "not valid nationalId")]
        [Required(ErrorMessage = "NationalId  is required")]
        public string NationalId { get; set; }
    }
}
