using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class Product : IEntity
    {
        public Product()
        {
            ProductComments = new HashSet<ProductComment>();
            ProductPictures = new HashSet<ProductPicture>();
            ProductTechnicProperties = new HashSet<ProductTechnicProperty>();
            BasketProducts = new HashSet<BasketProduct>();
            OrderDetails = new HashSet<OrderDetail>();
            Campaigns = new HashSet<Campaign>();
        }
        public Guid ProductID { get; set; }
        public Guid ModelID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int ViewCount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        //navigation
        public virtual ICollection<BasketProduct> BasketProducts { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        public virtual ICollection<ProductTechnicProperty> ProductTechnicProperties { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}