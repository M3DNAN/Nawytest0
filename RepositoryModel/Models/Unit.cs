using RepositoryModel.ConstEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class Unit
    {
        public string Id { get; set; }
        public string Location { get; set; }

        public string Name { get; set; }
        public Avalibility Available { get; set; }

        public int AvailableShares { get; set; }

        // MONEY
        public decimal DownPayment { get; set; }

        public decimal StartUnitPrice { get; set; }
        public decimal CurrentUnitPrice { get; set; }

        public decimal CurrentUnitROI { get; set; }

        public decimal MonthlyPayment { get; set; }

        // end of money
        public DateTime AvilableDate { get; set; }

        public DateTime ExitDate { get; set; }

        //public string ContractImage { get; set; }

        // images
        public ICollection<UnitImages>? UnitImages { get; set; }=new List<UnitImages>();

        // description
        public UnitDescription? UnitDescription { get; set; } 

        public ICollection<UnitView>? UnitViews { get; set; } = new List<UnitView>();

        // developer relation
        public string DeveloperId { get; set; } 
        public Developer Developer { get; set; }


    }
}
