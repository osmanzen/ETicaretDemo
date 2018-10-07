using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class UserDetail : IEntity
    {
        public UserDetail()
        {
            Orders = new HashSet<Order>();
        }

        public Guid  UserDetailID { get; set; }
        public Guid UserID { get; set; }
        public bool Gender { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        
        //
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}