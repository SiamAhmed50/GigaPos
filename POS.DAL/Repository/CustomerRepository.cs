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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Customer obj)
        {
            _db.Customers.Update(obj);
        }
    }
}
