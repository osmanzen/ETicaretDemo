using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class BasketProductMap : EntityTypeConfiguration<BasketProduct>
    {
        public BasketProductMap()
        {
            ToTable("BasketProduct");
            HasKey(b => new { b.UserID, b.ProductID });

            Property(b => b.Count).IsRequired();
            Property(x => x.IsActive).IsRequired();
            //
            HasRequired(b => b.Product).WithMany(x => x.BasketProducts).HasForeignKey(b => b.ProductID);
            HasRequired(b => b.User).WithMany(x => x.BasketProducts).HasForeignKey(b => b.UserID);
        }
    }
}
