using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
    public class CompanyInstallment
    {
        public string Id { get; set; }

        public DateTime InstallmentDate { get; set; }

        public decimal InstallmentPrice { get; set; }

        // realtion with main transactions
        public string CompanyTransactionId { get; set; }
        public CompanyTransaction CompanyTransaction { get; set; }


    }
}
