using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Model.Models.Entity
{
    public class Make:IEntity
    {
        public Make()
        {
            Models = new HashSet<ProductModel>();
            Categories = new HashSet<Category>();
        }
        public Guid MakeID { get; set; }
        public string MakeName { get; set; }
        public bool IsActive { get; set; }

        //
        public virtual ICollection<ProductModel> Models { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
