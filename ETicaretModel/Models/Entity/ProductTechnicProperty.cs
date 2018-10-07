using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class ProductTechnicProperty : IEntity
    {
       
        public Guid ProductPropertyID { get; set; }
        public Guid ProductID { get; set; }
        public Guid PropertyID { get; set; }
        public string PropertyValue { get; set; }

        //
        public virtual Product Product { get; set; }
        public virtual Property Property { get; set; }
    }
}