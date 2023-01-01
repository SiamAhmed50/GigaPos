using POS.DAL.Data;
using POS.DAL.Repository.IRpository;
using POS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Repository
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private ApplicationDbContext _db;
        public PurchaseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Purchase obj)
        {
             
           
        }
    }
}
