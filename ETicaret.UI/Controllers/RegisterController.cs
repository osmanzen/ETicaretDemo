using ETicaret.DAL.Abstract;
using ETicaret.UI.Models.Attributes;
using ETicaret.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Controllers
{
    [UserLogoutControl, ]
    public class RegisterController : Controller
    {
        public IUserDAL userDAL { get; set; }
        ETicaret.Model.Models.Entity.User newUser;

        public RegisterController(IUserDAL _userDAL)
        {
            userDAL = _userDAL;
        }

        // GET: User/Register
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(VMUserRegister register)
        {
            if (ModelState.IsValid)
            {
                newUser=userDAL.Get(x => x.Email==register.EMail && x.IsActive==true);

                if (newUser != null)
                {
                    ViewBag.Error = "E-Mail Adresi Kayıtlı Farklı Bir Adres Giriniz.";
                }
                else
                {
                    newUser = new Model.Models.Entity.User();
                    newUser.UserID = Guid.NewGuid();
                    newUser.UserTypeID = new Guid("69C7857A-3F4A-4B4A-8E5E-789A0B3A39AD");
                    newUser.FullName = register.FullName;
                    newUser.Email = register.EMail;
                    newUser.Password = register.Password;
                    newUser.CreatedDate = DateTime.Now;
                    newUser.IsActive = true;
                    userDAL.Add(newUser);

                    Session["user"] = newUser;
                    return RedirectToAction("Index", "UserDetail");
                }
            }
            return View();
        }
    }
}