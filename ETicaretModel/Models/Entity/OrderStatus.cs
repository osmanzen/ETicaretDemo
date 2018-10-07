using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Entity
{
    public class OrderStatus : IEntity
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }
        public Guid OrderStatusID { get; set; }
        public string Status { get; set; }
        //Navigation Properties
        public virtual ICollection<Order> Orders { get; set; }
    }
}
