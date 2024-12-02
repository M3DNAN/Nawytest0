using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Models
{
   public class SharesTransaction
    {
        public string SharesTransactionId { get; set; }
        public string UnitId { get; set; }
        public string ApplicationUserId { get; set; }
        [DefaultValue(3)]
        public int? AvalibaleSharesNumber { get; set; }
        public int UserSharesNumber { get; set; }
        public decimal TotalPaidTillNow { get; set; }
        public string SignedContractImage { get; set; }
        public DateTime DownPaymentDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Unit Unit { get; set; }

        // realation of installment
        public ICollection<Installment> Installments { get; set; } = new List<Installment>();
    }
}
