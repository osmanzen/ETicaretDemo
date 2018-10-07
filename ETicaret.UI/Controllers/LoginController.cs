using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Models.Attributes;
using ETicaret.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Controllers
{
    public class LoginController : Controller
    {
        public IUserDAL userDAL { get; set; }
        public IBasketProductDAL basketDAL { get; set; }
        public LoginController(IUserDAL _userDAL, IBasketProductDAL _basketDAL)
        {
            basketDAL = _basketDAL;
            userDAL = _userDAL;
        }

        // GET: User/Login
        [UserLogoutControl]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, UserLogoutControl]
        public ActionResult Index(VMUserLogin user)
        {
            if (ModelState.IsValid)
            {
                var getUser = userDAL.Get(x => x.Email == user.EMail && x.Password == user.Password && x.IsActive == true);

                if (getUser == null)
                {
                    ViewBag.Error = "Email yada Şifre Hatalı";
                    return View();
                }
                else
                {
                    if (getUser.UserType.UserTypeName == "admin")
                    {
                        Session["admin"] = getUser;
                    }
                    Session["user"] = getUser;

                    CartToDatabase();

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        private void CartToDatabase()
        {
            ETicaret.Model.Models.Entity.User user = Session["user"] as ETicaret.Model.Models.Entity.User;

            if (Session["basket"] != null)
            {
                List<BasketProduct> basketList = Session["basket"] as List<BasketProduct>;

                foreach (BasketProduct item in basketList)
                {
                    var inBasket = user.BasketProducts.Where(x => x.ProductID == item.ProductID).FirstOrDefault();

                    if (inBasket == null)
                    {
                        item.UserID = user.UserID;
                        item.User = user;
                        basketDAL.Add(item);
                        basketDAL.Save();
                    }
                    else
                    {
                        if (inBasket.IsActive == true)
                        {
                            inBasket.Count += item.Count;
                        }
                        else
                        {
                            inBasket.Count = item.Count;
                            inBasket.IsActive = true;
                        }
                        basketDAL.Update(inBasket);
                        basketDAL.Save();
                    }
                }

            }
        }

        [UserLoginControl]
        public RedirectResult SignOut()
        {
            Session.Abandon();
            return Redirect("/Home/Index");
        }
    }
}