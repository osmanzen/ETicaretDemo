using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMUserRegister
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez!"),
            EmailAddress(ErrorMessage = "Mail formatına uygun giriniz!"),
            Display(Name = "E-Posta"), DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez!"),
            MinLength(10, ErrorMessage = "Ad soyad minimum 10 karakter olmalıdır!"),
            DataType(DataType.Text), Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez!"),
            MinLength(3, ErrorMessage = "Şifre uzunluğu minimum 3 karakter olmalıdır!"),
            DataType(DataType.Password), Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez!"),
            MinLength(3, ErrorMessage = "Şifre uzunluğu minimum 3 karakter olmalıdır!"),
            DataType(DataType.Password), Display(Name = "Şifre"),Compare("Password", ErrorMessage = "Şifreler Aynı Değil")]
        public string ConfirmPassword { get; set; }        
    }
}