using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMProductDetail
    {
        public Product  Product { get; set; }
        public ICollection<Product> ProductList { get; set; }
        public ICollection<Campaign> CampaignList { get; set; }
        public ICollection<ProductModel> SubModelList { get; set; }
    }
}