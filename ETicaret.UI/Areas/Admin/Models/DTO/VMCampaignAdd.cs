using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMCampaignAdd
    {
        public Campaign Campaign { get; set; }
        public ICollection<Category> CategoryList { get; set; }
        public ICollection<Make> MakeList { get; set; }
        public ICollection<ProductModel> ProductModelList { get; set; }
        public ICollection<Product> ProductList { get; set; }
        
    }
}