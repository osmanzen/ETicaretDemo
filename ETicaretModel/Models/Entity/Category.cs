using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class Category : IEntity
    {
        public Category()
        {
            Properties = new HashSet<Property>();
            ProductModels = new HashSet<ProductModel>();
            Makes = new HashSet<Make>();

        }
        public Guid CategoryID { get; set; }
        public Guid? SubCategoryID { get; set; }
        [Display(Name="Kategori Adı"),Required(ErrorMessage ="Boş Geçilemez.")]
        public string CategoryName { get; set; }
        [Display(Name = "Açıklama"), Required(ErrorMessage = "Boş Geçilemez.")]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        //Navigation
        public virtual Category SCategory { get; set; }
        public virtual ICollection<ProductModel> ProductModels { get; set; }
        public virtual ICollection<Property> Properties{ get; set; }
        public virtual ICollection<Make> Makes { get; set; }
    }
}