using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class BasketProduct : IEntity
    {
       
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }

        //navigation property
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}