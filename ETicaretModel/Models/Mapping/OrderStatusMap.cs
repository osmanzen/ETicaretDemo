using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class OrderStatusMap : EntityTypeConfiguration<OrderStatus>
    {
        public OrderStatusMap()
        {
            ToTable("OrderStatus");
            HasKey(x => x.OrderStatusID);
            Property(x => x.Status).HasMaxLength(50).IsRequired();

            //

        }
    }
}
