using POS.DAL.Data;
using POS.DAL.Repository.IRpository;
using POS.Models;
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

        public List<PurchaseItem> GetPurchaseItems(int PurchaseId)
        {
           return _db.PurchaseItems.AsQueryable().Where(w => w.PurchaseId == PurchaseId).ToList();
        }

        public void Update(Purchase obj)
        {
             
           
        }
    }
}
