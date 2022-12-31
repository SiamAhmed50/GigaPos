using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EntityModel
{
    public class Supplier : BaseEntity
    {
        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
    
        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        [Required]
        public string Phone { get; set; }
        [Display(Name = "Opening Receivable")]
        public string? OpeningReceivable { get; set; }
        [Display(Name = "Opening Payable")]
        public string? OpeningPayable { get; set; }
    }
}
