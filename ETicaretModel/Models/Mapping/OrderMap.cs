using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            ToTable("Order");
            HasKey(o => o.OrderID);

            Property(o => o.TotalAmount).IsRequired();
            Property(o => o.CreatedTime).IsRequired();
            Property(o => o.IsActive).IsRequired();

            //,
            HasOptional(o => o.UserCard).WithMany(c => c.Orders).HasForeignKey(o => o.UserCardID);
            HasRequired(o => o.PaymentType).WithMany(p => p.Orders).HasForeignKey(o => o.PaymentTypeID);
            HasRequired(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserID).WillCascadeOnDelete(false);
            HasRequired(o => o.UserAddress).WithMany(u => u.Orders).HasForeignKey(o => o.AddressID).WillCascadeOnDelete(false);
            HasRequired(o => o.UserDetail).WithMany(u => u.Orders).HasForeignKey(o => o.UserDetailID).WillCascadeOnDelete(false);
            HasRequired(x => x.OrderStatus).WithMany(x => x.Orders).HasForeignKey(x => x.OrderStatusID);
        }
    }
}
