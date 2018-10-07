using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Entity
{
    public class Campaign : IEntity
    {
        public Campaign()
        {
            Products = new HashSet<Product>();
        }
        public Guid CampaignID { get; set; }
        public string Title { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndingDate { get; set; }
        public decimal Discount { get; set; }
        public string PictureUrl { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        //
        public virtual ICollection<Product> Products { get; set; }
    }
}
