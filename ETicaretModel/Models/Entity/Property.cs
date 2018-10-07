using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class Property : IEntity
    {
        public Property()
        {
            Categories = new HashSet<Category>();
            ProductTechnicProperties = new HashSet<ProductTechnicProperty>();
        }
        public Guid PropertyID { get; set; }
        public string PropertyName { get; set; }
        public bool IsActive { get; set; }

        //navigation
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductTechnicProperty> ProductTechnicProperties { get; set; }
    }
}