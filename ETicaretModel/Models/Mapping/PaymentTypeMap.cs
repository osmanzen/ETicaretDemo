using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class PaymentTypeMap : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeMap()
        {
            ToTable("PaymentType");
            HasKey(x => x.PaymentTypeID);

            Property(x => x.PaymentName).HasMaxLength(50).IsRequired();
            Property(x => x.IsActive).IsRequired();
        }
    }
}
