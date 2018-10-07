using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMMakeAdd
    {
        public string MakeName { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}