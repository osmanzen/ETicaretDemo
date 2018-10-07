using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class User : IEntity
    {
        public User()
        {
            Orders = new HashSet<Order>();
            UserCards = new HashSet<UserCard>();
            UserDetails = new HashSet<UserDetail>();
            UserAddresses = new HashSet<UserAddress>();
            BasketProducts = new HashSet<BasketProduct>();
            ProductComments = new HashSet<ProductComment>();

        }
        public Guid UserID { get; set; }
        public Guid UserTypeID { get; set; }

        [Required(ErrorMessage ="Bu alan boş bırakılamaz"),MinLength(10,ErrorMessage ="İsim 10 karakterden küçük olamaz")]
        public string FullName { get; set; }


        public string Email { get; set; }

        [Required(ErrorMessage ="Bu alan boş bırakılamaz"),MinLength(4,ErrorMessage ="Şifre 4 karakterden az olamaz")]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        //navigation
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserCard> UserCards { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<BasketProduct> BasketProducts { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }

    }
}