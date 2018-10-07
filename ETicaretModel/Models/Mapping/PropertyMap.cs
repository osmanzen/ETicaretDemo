using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class PropertyMap : EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            ToTable("Property");
            HasKey(x => x.PropertyID);

            Property(x => x.PropertyName).HasMaxLength(50).IsRequired();
            Property(x => x.IsActive).IsRequired();
        }
    }
}
