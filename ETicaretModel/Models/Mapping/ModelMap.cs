using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class ModelMap:EntityTypeConfiguration<ProductModel>
    {
        public ModelMap()
        {
            ToTable("ProductModel");
            HasKey(x => x.ModelID);

            Property(x => x.ModelName).HasMaxLength(250).IsRequired();
            Property(x => x.MakeID).IsRequired();
            Property(x => x.CategoryID).IsRequired();
            Property(x => x.IsActive).IsRequired();
            //
            HasRequired(x => x.Make).WithMany(x => x.Models).HasForeignKey(x => x.MakeID);
            HasRequired(x => x.Category).WithMany(x => x.ProductModels).HasForeignKey(x => x.CategoryID);
        }
    }
}
