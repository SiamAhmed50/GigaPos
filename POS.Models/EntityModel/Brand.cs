using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EntityModel
{
    public class Brand : BaseEntity
    {
         

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }


        [ValidateNever]
        public string? LogoUrl { get; set; }
    }
}
