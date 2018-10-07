using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMOrderAddressDetail
    {
        public List<UserAddress> UserAddresses { get; set; }

        public UserDetail UserDetail { get; set; }

        public int DistrictID { get; set; }

        public string AddressFullName { get; set; }

        public string Address { get; set; }

        public Boolean Gender { get; set; }

        public string Telephone { get; set; }
    }
}