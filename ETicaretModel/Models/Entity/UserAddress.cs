using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class UserAddress : IEntity
    {
        public UserAddress()
        {
            Orders = new HashSet<Order>();
        }
        public Guid UserAddressID { get; set; }
        public Guid UserID { get; set; }
        public int DistrictID { get; set; }
        public string AddressFullName { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        
        //
        public virtual User  User { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}