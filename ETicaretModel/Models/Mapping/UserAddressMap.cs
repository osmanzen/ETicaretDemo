using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class UserAddressMap : EntityTypeConfiguration<UserAddress>
    {
        public UserAddressMap()
        {
            ToTable("UserAddress");
            HasKey(x => x.UserAddressID);

            Property(x => x.AddressFullName).HasMaxLength(50).IsRequired();
            Property(x => x.Address).HasMaxLength(1000).IsRequired();
            Property(x => x.IsActive).IsRequired();

            //Navigation Properties
            HasRequired(x => x.User).WithMany(x => x.UserAddresses).HasForeignKey(x => x.UserID);
            HasRequired(x => x.District).WithMany(x => x.UserAddresses).HasForeignKey(x => x.DistrictID);
        }
    }
}
