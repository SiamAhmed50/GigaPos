using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EntityModel
{
    public class Unit : BaseEntity
    {
       
        [Required]
        public string Name { get; set; }

        public int RelatedUnitId { get; set; }

        public List<string> RelatedSign { get; set; }

        public int RelatedBy { get; set; }
    }
}
