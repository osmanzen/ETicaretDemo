using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class UserCardMap : EntityTypeConfiguration<UserCard>
    {
        public UserCardMap()
        {
            ToTable("UserCard");
            HasKey(x => x.UserCardID);

            Property(x => x.FullName).HasMaxLength(50).IsRequired();
            Property(x => x.CardNo).HasMaxLength(16).IsRequired();
            Property(x => x.SecurityCode).HasMaxLength(3).IsRequired();
            Property(x => x.ExpritionDate).IsRequired();
            Property(x => x.IsActive).IsRequired();

            //Navigation Properties
            HasRequired(x => x.User).WithMany(x => x.UserCards).HasForeignKey(x => x.UserID);

        }
    }
}
