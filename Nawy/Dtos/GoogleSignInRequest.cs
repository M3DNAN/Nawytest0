using System.ComponentModel.DataAnnotations;

namespace Nawy.Dtos
{
    public class GoogleSignInRequest
    {
        [Required]
        public string token { get; set; }
    }
}
