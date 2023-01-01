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
    public class DamageRepository: Repository<Damage>, IDamageRepository
    {
        private ApplicationDbContext _db;
        public DamageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Damage obj)
        {
            _db.Damages.Update(obj);
        }
    }
}
