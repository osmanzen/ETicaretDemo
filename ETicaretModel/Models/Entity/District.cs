using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class District : IEntity
    {
        public District()
        {
            UserAddresses = new HashSet<UserAddress>();
        }
        public int  DistrictID { get; set; }
        public int CityID { get; set; }
        public string DistrictName { get; set; }
        //
        public virtual City City { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}