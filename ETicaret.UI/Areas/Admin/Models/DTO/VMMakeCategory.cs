using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMMakeCategory
    {
        public Make Make { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}