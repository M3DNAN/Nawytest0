using System.ComponentModel.DataAnnotations;

namespace Nawy.Dtos
{
    public class RefreshTokenRequestinpage
    {
        [Required]
        public string refreshToken { get; set; }
        [Required]
        public string NowToken { get; set; }
    }
}
