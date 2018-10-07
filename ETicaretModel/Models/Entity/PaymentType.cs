using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class PaymentType : IEntity
    {
        public PaymentType()
        {
            Orders = new HashSet<Order>();

        }
        public Guid PaymentTypeID { get; set; }
        public string PaymentName { get; set; }
        public bool IsActive { get; set; }

        //
        public virtual ICollection<Order> Orders { get; set; }
    }
}