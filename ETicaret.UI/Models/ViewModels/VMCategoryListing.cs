using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMCategoryListing
    {
        public ICollection<Category> CategoryList { get; set; }
        public ICollection<Make> MakeList { get; set; }
        public ICollection<VMCategoryProduct> ProductList { get; set; }
        
    }
}