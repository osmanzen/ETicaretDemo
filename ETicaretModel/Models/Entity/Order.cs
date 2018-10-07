using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class Order : IEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid UserDetailID { get; set; }
        public Guid AddressID { get; set; }
        public Guid PaymentTypeID { get; set; }
        public Guid? UserCardID { get; set; }
        public Guid OrderStatusID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }

        //

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual User User { get; set; }
        public virtual UserAddress UserAddress { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual UserCard UserCard { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

    }
}