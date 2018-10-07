using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Model.Models.Entity
{
    public class City : IEntity
    {
        public City()
        {
            Districts = new HashSet<District>();
        }
        public int CityID { get; set; }
        public string CityName { get; set; }
        
        //
        public virtual ICollection<District> Districts { get; set; }

    }
}