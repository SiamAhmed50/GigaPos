using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EntityModel
{
    public class Product : BaseEntity
    {
       
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Code { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int UnitId { get; set; }
        public int SubUnit { get; set; }
        public string OpenningStock { get; set; }


        [Required]
        [Range(1, 10000)]
        public double SalePrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double PurchaseCost { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }




        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }


        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand Brand { get; set; }

        [ForeignKey("UnitId")]
        [ValidateNever]
        public Unit Unit { get; set; }
    }
}
