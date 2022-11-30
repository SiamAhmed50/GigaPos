using Microsoft.AspNetCore.Identity;
using POS.DAL.Data;
using POS.DAL.Repository.IRpository;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Repository
{
    public class Dbinitializer : IDbinitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public Dbinitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void seed()
        {
            // Check Role

            // Add Role

            //Check User

            // Add User

            // Check User Role

            // Add User Role

        }
    }
}
