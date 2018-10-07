using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{

    public class CampaignMap : EntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            ToTable("Campaign");
            HasKey(x => x.CampaignID);
            Property(x => x.Title).HasMaxLength(50).IsRequired();
            Property(x => x.StartedDate).IsRequired();
            Property(x => x.EndingDate).IsRequired();
            Property(x => x.Discount).IsRequired();
            Property(x => x.PictureUrl).IsRequired();
            Property(x => x.IsActive).IsRequired();
            //
            HasMany(x => x.Products).WithMany(x => x.Campaigns).Map(m => { m.ToTable("ProductCampaign"); m.MapLeftKey("CampaignID"); m.MapRightKey("ProductID"); });
        }
    }
}
