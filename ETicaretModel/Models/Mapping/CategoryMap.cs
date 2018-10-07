using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class CategoryMap:EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {

            ToTable("Category");
            HasKey(c => c.CategoryID);

            Property(c => c.CategoryName).HasMaxLength(50).IsRequired();
            Property(c => c.Description).HasMaxLength(250).IsRequired();
            Property(c => c.SubCategoryID).IsOptional();
            Property(c => c.IsActive).IsRequired();

            //
            HasOptional(c => c.SCategory).WithMany().HasForeignKey(c => c.SubCategoryID);
            HasMany(x => x.Properties).WithMany(x => x.Categories).Map(x => { x.ToTable("CategoryProperty"); x.MapLeftKey("CategoryID"); x.MapRightKey("PropertyID"); });
           // HasMany(x => x.Makes).WithMany(x => x.Categories).Map(x => { x.ToTable("CategoryMake"); x.MapLeftKey("CategoryID"); x.MapRightKey("MakeID"); });
        }
    }
}
