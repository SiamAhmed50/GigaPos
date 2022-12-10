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
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private ApplicationDbContext _db;
        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Brand obj)
        {
            var objFromDb = _db.Brands.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
               
               
                if (obj.LogoUrl != null)
                {
                    objFromDb.LogoUrl = obj.LogoUrl;
                }
            }
        }
    }
}
