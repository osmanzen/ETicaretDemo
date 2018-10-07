using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(x => x.UserID);

            Property(x => x.FullName).HasMaxLength(50).IsRequired();
            Property(x => x.Email).HasMaxLength(50).IsRequired();
            Property(x => x.Password).HasMaxLength(50).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.IsActive).IsRequired();

            //Navigation Properties
            HasRequired(x => x.UserType).WithMany(x => x.Users).HasForeignKey(x => x.UserTypeID);
        }
    }
}
