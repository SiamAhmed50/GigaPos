using POS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Repository.IRpository
{
    public interface IBrandRepository : IRepository<Brand>
    {

        void Update(Brand obj);
    }
}
