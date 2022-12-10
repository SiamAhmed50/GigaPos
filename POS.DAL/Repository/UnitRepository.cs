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
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private ApplicationDbContext _db;
        public UnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Unit obj)
        {
            _db.Units.Update(obj);
        }
    }
}
