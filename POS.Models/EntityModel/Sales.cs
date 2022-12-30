using POS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EntityModel
{
    public class Sales : BaseEntity
    {
        public DateTime SalesDate { get; set; }

        public int CustomerId { get; set; }

        public double? Discount { get; set; }

        public string? Note { get; set; }

        public TransactionAccount? TransactionAccount { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }

        public double Paid { get; set; }
        public double? GrandTotal { get; set; }
        public double? Receivable { get; set; }
        public double? Due { get; set; }
        public double? PurchaseCost { get; set; }
        public double? Profit { get; set; }

    }
}
