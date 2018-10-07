using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.Model.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class HomeController : Controller
    {
        public IProductDAL ProductDAL { get; set; }

        public HomeController(IProductDAL productDAL)
        {
            ProductDAL = productDAL;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetProduct(string id)
        {
            ICollection<VMProduct> productList = ProductDAL.GetList().Skip(3*Convert.ToInt32(id)).Take(3).Select(x => new VMProduct()
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                PicturePath = x.ProductPictures.Select(p => p.PicturePath).FirstOrDefault()
            }).ToList();
            return Json(productList,JsonRequestBehavior.AllowGet);
        }
    }
}