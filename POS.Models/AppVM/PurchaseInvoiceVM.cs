using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.AppVM
{
    public class PurchaseInvoiceVM
    {
        public string? InvoiceId { get; set; }
        public string? Date { get; set; }
        public string? Note { get; set; }

        public string? Due { get; set; }
        public string? Paid { get; set; }
        public string? GrandTotal { get; set; }

        public string? TransactionAccount { get; set; } 
        public string? SupplierName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public List<PurchaseItemsInvoiceVM>? PurchaseItems { get; set; }

    }

    public class PurchaseItemsInvoiceVM
    { 
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? UnitName { get; set; }
        public string? SubUnitName { get; set; }
        public string? Price { get; set; }
        public string? MainUnitQuantity { get; set; }
        public string? SubUnitQuantity { get; set; }
        public string? SubTotal { get; set; }
    }
}
