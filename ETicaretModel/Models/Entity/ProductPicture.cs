using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class ProductPicture : IEntity
    {
        public Guid ProductPictureID { get; set; }
        public Guid ProductID { get; set; }
        public string PicturePath { get; set; }
        public bool IsActive { get; set; }

        //
        public virtual Product Product { get; set; }
    }
}