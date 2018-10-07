using ETicaret.Model.Models.Entity;
using ETicaret.Model.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DAL.Concrete.EntityFramework.Context
{
    public class ETicaretDBContext : DbContext
    {
        public ETicaretDBContext()
            //: base(nameOrConnectionString: "Server=tcp:levent.database.windows.net,1433;Initial Catalog=eticaret1;Persist Security Info=False;User ID=leventsumak;Password=Levo395.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            : base(nameOrConnectionString: @"Server=.\ZenSoftDB;Database=Eticaret;Trusted_Connection=True;")
        {

        }

        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductTechnicProperty> ProductTechnicProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserCard> UserCards { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
        public DbSet<Make> Make { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new BasketProductMap());

            modelBuilder.Configurations.Add(new CategoryMap());

            modelBuilder.Configurations.Add(new CampaignMap());

            modelBuilder.Configurations.Add(new CityMap());

            modelBuilder.Configurations.Add(new DistrictMap());

            modelBuilder.Configurations.Add(new OrderMap());

            modelBuilder.Configurations.Add(new OrderStatusMap());

            modelBuilder.Configurations.Add(new OrderDetailMap());

            modelBuilder.Configurations.Add(new PaymentTypeMap());

            modelBuilder.Configurations.Add(new ProductMap());

            modelBuilder.Configurations.Add(new ProductCommentMap());

            modelBuilder.Configurations.Add(new ProductPictureMap());

            modelBuilder.Configurations.Add(new ProductTechnicPropertyMap());

            modelBuilder.Configurations.Add(new PropertyMap());

            modelBuilder.Configurations.Add(new UserMap());

            modelBuilder.Configurations.Add(new UserAddressMap());

            modelBuilder.Configurations.Add(new UserCardMap());

            modelBuilder.Configurations.Add(new UserDetailMap());

            modelBuilder.Configurations.Add(new UserTypeMap());

            modelBuilder.Configurations.Add(new ModelMap());

            modelBuilder.Configurations.Add(new MakeMap());
        }
    }
}
