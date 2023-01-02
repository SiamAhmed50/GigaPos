
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace POS.Models.AppVM
{
    public class PosVM
    {
        public Sales Pos { get; set; }
        
        [ValidateNever]

        public IEnumerable<SelectListItem>? ProductList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CustomerList { get; set; }

    }
}
