using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Mapping
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            ToTable("District");
            Property(x => x.DistrictID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            HasKey(d => d.DistrictID);
            Property(d => d.DistrictName).HasMaxLength(50).IsRequired();
            //
            HasRequired(d => d.City).WithMany(c => c.Districts).HasForeignKey(d => d.CityID);
        }
    }
}
