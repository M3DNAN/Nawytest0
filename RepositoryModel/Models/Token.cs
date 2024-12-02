using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    [PrimaryKey(nameof(TokenId))]
    public class Token
    {
        public string TokenId { get; set; }

        public string ApplicationUserId { get; set; }

        public string TokenEncode { get; set; }
        // should not be null
        public string RefreshToken { get; set; }

        public DateTime TokenExpiryTime { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime TimeOfLog { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
