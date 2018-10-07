using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class ProductTechnicPropertyMap : EntityTypeConfiguration<ProductTechnicProperty>
    {
        public ProductTechnicPropertyMap()
        {
            ToTable("ProductTechnicProperty");
            HasKey(x => x.ProductPropertyID);

            Property(x => x.PropertyValue).HasMaxLength(50).IsRequired();

            //
            HasRequired(x => x.Product).WithMany(x => x.ProductTechnicProperties).HasForeignKey(x => x.ProductID);
            HasRequired(x => x.Property).WithMany(x => x.ProductTechnicProperties).HasForeignKey(x => x.PropertyID);
        }
    }
}
