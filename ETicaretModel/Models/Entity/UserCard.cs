using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class UserCard : IEntity
    {
        public UserCard()
        {
            Orders = new HashSet<Order>();
        }
        public Guid UserCardID { get; set; }
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string CardNo { get; set; }
        public string SecurityCode { get; set; }
        public DateTime ExpritionDate { get; set; }
        public bool IsActive { get; set; }

        //
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}