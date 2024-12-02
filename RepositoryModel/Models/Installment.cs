using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RepositoryModel.Models
{
    public class Installment
    {
        public string Id { get; set; }

        public DateTime InstallmentDate { get; set; }

        public decimal InstallmentPrice { get; set; }

        // relation of transaction
        public string TransactionId { get; set; }
        public SharesTransaction Transaction { get; set; }
        
    }
}
