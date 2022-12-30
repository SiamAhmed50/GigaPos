using POS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class SalesItem : BaseEntity
    {
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int? Quantity { get; set; }
        public double? SubTotal { get; set; }


        [ForeignKey("SalesId")]
        [ValidateNever]
        public Sales? Sales { get; set; }

    }
}
