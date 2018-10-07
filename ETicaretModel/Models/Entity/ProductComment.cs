using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class ProductComment : IEntity
    {
        public Guid ProductCommentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        //
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}