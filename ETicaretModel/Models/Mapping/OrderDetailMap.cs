using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap()
        {
            ToTable("OrderDetail");
            HasKey(x => new { x.OrderID, x.ProductID });

            Property(x => x.Count).IsRequired();
            Property(x => x.IsActive).IsRequired();

            //navigation Properties
            HasRequired(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderID);
            HasRequired(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductID);
        }
    }
}
