using POS.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Repository.IRpository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        
        void Update(Category obj);
    }
}
