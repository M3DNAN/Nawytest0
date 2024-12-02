using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class UnitImages

    {
        public int UnitImagesId { get; set; }
        public string UnitId { get; set; }
        public string PhotoUrl { get; set; }
        public Unit Unit { get; set; }
    }
}
