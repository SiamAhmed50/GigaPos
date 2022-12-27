using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace POS.Models.EntityModel
{
    public class Product : BaseEntity
    {
       
        [Required]
        public string Name { get; set; }

        [AllowHtml]
        public string? Description { get; set; }

        public string? Code { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        [Display(Name = "Unit")]
        public int? UnitId { get; set; }
        [Display(Name = "Sub Unit")]
        public int? SubUnitId { get; set; }
   
        public string? OpenningStockUnit { get; set; }
        public string? OpenningStockSubUnit { get; set; }


        [Required]
        [Range(1, 10000)]
        [Display(Name = "Sale Price")]
        public double SalePrice { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Purchase Cost")]
        public double PurchaseCost { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        public string? BarcodeUrl { get; set; }
        [ValidateNever]
        public string? QrCodeUrl { get; set; }


        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }


        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand? Brand { get; set; }

        [ForeignKey("UnitId")]
        [ValidateNever]
        public Unit? Unit { get; set; }
    }
}
