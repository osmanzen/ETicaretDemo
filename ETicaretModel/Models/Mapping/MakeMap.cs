using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class MakeMap:EntityTypeConfiguration<Make>
    {
        public MakeMap()
        {
            ToTable("Make");
            HasKey(x => x.MakeID);

            Property(x => x.MakeName).HasMaxLength(50).IsRequired();
            Property(x => x.IsActive).IsRequired();
            //ilişkileri category ve model mapinde
        }
    }
}
