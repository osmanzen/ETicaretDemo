﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.UI.Models.ViewModels
{
    public class VMUserNewCard
    {
        [Required(ErrorMessage = "Kart Adı Soyadı boş geçilemez!"), MinLength(5, ErrorMessage = "Kart Adı Soyadı Çok Kısa Girdiniz")]
        public string CardFullName { get; set; }

        [Required(ErrorMessage = "Kart Adı Soyadı boş geçilemez!"), StringLength(16, ErrorMessage = "Kart Numarası 16 karakter olmalıdır!")]
        public string CardNo { get; set; }

        [Required(ErrorMessage = "Güvenlik Kodu boş geçilemez!"), StringLength(3, ErrorMessage = "Güvenlik Kodu 3 karakter olmalıdır!")]
        public string SecurityCode { get; set; }

        [Required(ErrorMessage = "*"), MaxLength(2, ErrorMessage = "*"), MinLength(1, ErrorMessage = "*")]
        public string Month { get; set; }

        [Required(ErrorMessage = "*"), StringLength(2, ErrorMessage = "*")]
        public string Year { get; set; }

        public Boolean isSave { get; set; }
    }
}