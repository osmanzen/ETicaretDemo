using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class OrderDetail : IEntity
    {
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }
        //

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}