﻿using Core.DataAccess.EntityFramework;
using ETicaret.DAL.Abstract;
using ETicaret.DAL.Concrete.EntityFramework.Context;
using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DAL.Concrete.EntityFramework.Entities
{
    public class EFUserTypeDAL : EFEntityRepositoryBase<UserType, ETicaretDBContext>, IUserTypeDAL
    {
        public EFUserTypeDAL(DbContext context)
            : base(context)
        {

        }
    }
}
