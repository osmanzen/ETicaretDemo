using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class LoginController : Controller
    {
        public IUserDAL UserDAL { get; set; }

        public LoginController(IUserDAL userDAL)
        {
            UserDAL = userDAL;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid)//User da tanımladığımız şartlara uygun mu?
            {
                User u = UserDAL.Get(x => x.Email == user.Email && x.Password==user.Password);
                if (u!=null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı adı veya Parola yanlış";
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Singup(User user)
        {
            user.UserID = Guid.NewGuid();
            user.IsActive = true;
            user.CreatedDate = DateTime.Now;
            user.UserTypeID = new Guid("eda9e927-3225-4c77-a9fc-03c1b8cfe8c2");

            UserDAL.Add(user);

            return RedirectToAction("");
        }
    }
}