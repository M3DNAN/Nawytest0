using RepositoryModel.ConstEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class Developer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SizeOFCompany? SizeOfCompany { get; set; }

        public string? PhoneNumber { get; set; }

        // السجل العقارى
        public string? RealEstateRegistry { get; set; }

        // relation of unit
        public ICollection<Unit>? Units { get; set; }=new List<Unit>();
    }
}
