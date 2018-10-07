using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Areas.Admin.Models.DTO
{
    public class VMProduct
    {
        public Guid ModelID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductModel> ProductModels { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<HttpPostedFileBase> Picture { get; set; }
        public List<ProductTechnicProperty> ProductTechnicProperties { get; set; }

    }
}