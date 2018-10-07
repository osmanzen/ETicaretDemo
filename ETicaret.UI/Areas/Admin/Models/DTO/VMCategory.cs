using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMCategory
    {
        public ICollection<Category> Categories { get; set; }

        public Category Category { get; set; }
    }
}