using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMModelAdd
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<Make> Makes { get; set; }
        public string ModelName { get; set; }
    }
}