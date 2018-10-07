using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class ProductCommentMap : EntityTypeConfiguration<ProductComment>
    {
        public ProductCommentMap()
        {
            ToTable("ProductComment");
            HasKey(x => x.ProductCommentID);

            Property(x => x.Content).HasMaxLength(1000).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.IsActive).IsRequired();

            //Navigation Properties
            HasRequired(x => x.Product).WithMany(x => x.ProductComments).HasForeignKey(x => x.ProductID);
            HasRequired(x => x.User).WithMany(x => x.ProductComments).HasForeignKey(x => x.UserID);

        }
    }
}
