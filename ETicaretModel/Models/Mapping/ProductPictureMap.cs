using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class ProductPictureMap : EntityTypeConfiguration<ProductPicture>
    {
        public ProductPictureMap()
        {
            ToTable("ProductPicture");
            HasKey(x => x.ProductPictureID);

            Property(x => x.PicturePath).HasMaxLength(250).IsRequired();
            Property(x => x.IsActive).IsRequired();

            //Navigation Properties
            HasRequired(x => x.Product).WithMany(x => x.ProductPictures).HasForeignKey(x => x.ProductID);
        }
    }
}
