using POS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EntityModel
{
    public class Purchase : BaseEntity
    {
        
        public DateTime PurchaseDate { get; set; } 
        public string? Note { get; set; }

        public double? Due { get; set; }
        public double Paid { get; set; }
        public double? GrandTotal { get; set; }

        public TransactionAccount? TransactionAccount { get; set; }
         
        public int? SupplierId { get; set; }

     






        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier? Supplier { get; set; }

 


        [ValidateNever]
        public List<PurchaseItem>? PurchaseItems { get; set; }
    }
}
