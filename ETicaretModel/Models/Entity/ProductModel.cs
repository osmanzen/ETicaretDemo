using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Entity
{
    public class ProductModel:IEntity
    {
        public ProductModel()
        {
            Products = new HashSet<Product>();
        }
        public Guid ModelID { get; set; }
        public string ModelName { get; set; }
        public Guid MakeID { get; set; }
        public Guid CategoryID { get; set; }
        public bool IsActive { get; set; }

        //
        public virtual Make Make { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products{ get; set; }
    }
}
