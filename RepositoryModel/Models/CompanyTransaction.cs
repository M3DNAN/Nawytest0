using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class CompanyTransaction
    {
        public string Id { get; set; }

        public string ApplicationUserId { get; set; } // comapny main accout ,, role admin

        public string UnitId { get; set; }

        public DateTime DownPaymentDate { get; set; }

        // paymentFromCompany
        public decimal DownPayment { get; set; }

        public  ApplicationUser ApplicationUser { get; set; }

        public Unit Unit { get; set; }


        // installments for company

        public ICollection<CompanyInstallment> CompanyInstallments { get; set; }=new List<CompanyInstallment>();
    }
}
