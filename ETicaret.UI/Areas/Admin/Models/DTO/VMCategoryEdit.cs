using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMCategoryEdit
    {
        public VMCategoryEdit()
        {
            ListCategories = new HashSet<ICollection<Category>>();
        }
        public Category Category { get; set; }
        public ICollection<ICollection<Category>> ListCategories { get; set; }
    }
}