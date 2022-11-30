﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Repository.IRpository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get;}
        void Save();
    }
}
