using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMUserOrderUseCard
    {
        public Boolean Gender { get; set; }

        [Required(ErrorMessage = "Telefon boş geçilemez!"),
    DataType(DataType.PhoneNumber), Display(Name = "Telefon"), StringLength(10, ErrorMessage = "Telefon 10 karakter olmalıdır. Örn: 555 555 55 55")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Adres Boş geçilemez!")]
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Kart Seçimi Boş geçilemez!")]
        public Guid UserCardID { get; set; }
    }
}