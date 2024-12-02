using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class UnitDescription
    {
        public int Id { get; set; }
        public string UnitId { get; set; }

        public int NumberOfBathrooms { get; set; }

        public int NumberOfBedrooms { get; set; }
        public int Area { get; set; }

        public  Unit Unit {get; set;}
    }
}
