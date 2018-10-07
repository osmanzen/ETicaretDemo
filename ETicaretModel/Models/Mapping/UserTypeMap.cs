using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class UserTypeMap : EntityTypeConfiguration<UserType>
    {
        public UserTypeMap()
        {
            ToTable("UserType");
            HasKey(x => x.UserTypeID);

            Property(x => x.UserTypeName).HasMaxLength(50).IsRequired();
        }
    }
}
