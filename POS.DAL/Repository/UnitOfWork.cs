using POS.DAL.Data;
using POS.DAL.Repository.IRpository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Brand = new BrandRepository(_db);
            Unit = new UnitRepository(_db);
            Customer = new CustomerRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IBrandRepository Brand { get; private set; }
        public IUnitRepository Unit { get; private set; }
        public ICustomerRepository Customer { get; private set; }
      

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
