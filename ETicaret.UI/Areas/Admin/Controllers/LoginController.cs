using ETicaret.DAL.Abstract;
using ETicaret.UI.Areas.Admin.Models.Attributes;
using ETicaret.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        public IUserDAL UserDAL { get; set; }

        public LoginController(IUserDAL userdal)
        {
            UserDAL = userdal;
        }
        //
        // GET: /Admin/Login/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminLogin model)
        {
            if(ModelState.IsValid)
            {
                Session["admin"] = UserDAL.Get(x => x.Email == model.EMail && x.Password == model.Password && x.IsActive == true && x.UserType.UserTypeName=="admin");
                if (Session["admin"] == null)
                {
                    ViewBag.Error = "Giriş başarısız.";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public ActionResult edit()
        {
            AdminLogin al = new AdminLogin()
            {
                EMail = "a@a.com",
                Password = "123"
            };
            return View(al);
        }

        [LoginControl]
        public ActionResult SignOut()
        {
            Session.Abandon();
            return View("Index");
        }
	}
}