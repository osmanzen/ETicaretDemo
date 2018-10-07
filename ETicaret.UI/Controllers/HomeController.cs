using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Models;
using ETicaret.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Controllers
{

    public class HomeController : Controller
    {
        IProductDAL productDAL;
        IUserDAL userDAL;
        ICategoryDAL categoryDAL;

        public HomeController(IProductDAL _productDAL, IUserDAL _userDAL, ICategoryDAL _categoryDAL)
        {
            productDAL = _productDAL;
            userDAL = _userDAL;
            categoryDAL = _categoryDAL;
        }


        // GET: Home
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSlider()
        {
            List<string> imgUrl = new List<string>();
            imgUrl.Add("../assets/img/demos/shop/slides/shop10/slide1.jpg");
            imgUrl.Add("../assets/img/demos/shop/slides/shop10/slide2.jpg");
            imgUrl.Add("../assets/img/demos/shop/slides/shop10/slide3.jpg");
            imgUrl.Add("../assets/img/demos/shop/slides/shop10/slide4.jpg");
            return PartialView("GetSlider", imgUrl);
        }

        public ActionResult TopProductContainer()
        {
            return PartialView(productDAL.GetList().Where(x => x.IsActive == true && x.UnitsInStock > 0).ToList());
        }

        [HttpPost]
        public ActionResult QuickView(string id)
        {
            var product = productDAL.Get(x => x.ProductID == new Guid(id));
            product.ViewCount++;
            productDAL.Update(product);
            return PartialView("QuickView", product);
        }

        public ActionResult Navbar()
        {
            return PartialView(categoryDAL.GetList().OrderBy(x => x.CategoryName).ToList());
        }

        public ActionResult ProductContainer()
        {
            List<Product> productList = productDAL.GetList(x => x.IsActive == true).Take(8).ToList();

            return PartialView(productList);
        }



        public JsonResult SearchList(string id)
        {
            List<VMSearchItem> searchList = new List<VMSearchItem>();
            searchList.AddRange(categoryDAL.GetList().Where(x => x.CategoryName.ToLower().Contains(id.ToLower())).Select(x => new VMSearchItem { itemID = x.CategoryID, itemName = x.CategoryName, url = "/Category/Index/" + x.CategoryID }).Take(5).ToList());
            if (searchList.Count <= 5)
                searchList.AddRange(productDAL.GetList().Select(x => new VMSearchItem { itemID = x.ProductID, itemName = x.ProductModel.Make.MakeName + " " + x.ProductModel.ModelName + " " + x.ProductName, url = "/Product/ProductDetail/" + x.ProductID }).Where(x => x.itemName.ToLower().Contains(id.ToLower())).Take(5).ToList());
            return Json(searchList, JsonRequestBehavior.AllowGet);
        }

    }
}