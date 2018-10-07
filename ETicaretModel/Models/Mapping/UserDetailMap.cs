using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class UserDetailMap : EntityTypeConfiguration<UserDetail>
    {
        public UserDetailMap()
        {
            ToTable("UserDetail");
            HasKey(x => x.UserDetailID);

            Property(x => x.Gender).IsRequired();
            Property(x => x.Telephone).HasMaxLength(11).IsRequired();
            Property(x=>x.IsActive).IsRequired();

            //Navigatiron Properties
            HasRequired(x => x.User).WithMany(x => x.UserDetails).HasForeignKey(x => x.UserID);

        }
    }
}
