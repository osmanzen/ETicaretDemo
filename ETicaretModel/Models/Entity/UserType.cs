using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class UserType : IEntity
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }
        public Guid UserTypeID { get; set; }
        public string UserTypeName { get; set; }

        //
        public virtual ICollection<User> Users { get; set; }
    }
}