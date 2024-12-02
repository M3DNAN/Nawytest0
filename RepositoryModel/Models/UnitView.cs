using RepositoryModel.ConstEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class UnitView
    {
        public int Id { get; set; }
        public string UnitId { get; set; }
        public ViewSpecial Name { get; set; }

        public Unit Unit { get; set; }  
    }
}
