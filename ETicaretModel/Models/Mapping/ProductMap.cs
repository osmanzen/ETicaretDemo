using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");
            HasKey(x => x.ProductID);

            Property(x => x.ProductName).HasMaxLength(50).IsRequired();
            Property(x => x.UnitPrice).IsRequired();
            Property(x => x.UnitsInStock).IsRequired();
            Property(x => x.ViewCount).IsRequired();
            Property(x => x.Description).HasMaxLength(250).IsRequired();
            Property(x => x.ModelID).IsOptional();
            Property(x => x.IsActive).IsRequired();

            //navigation Properties
            HasOptional(x => x.ProductModel).WithMany(x => x.Products).HasForeignKey(x => x.ModelID);
        }
    }
}
