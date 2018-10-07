using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    public class ProductModelController : Controller
    {
        IProductModelDAL ProductModelDAL;
        public IMakeDAL MakeDAL { get; set; }
        public ProductModelController(IProductModelDAL productmodeldal, IMakeDAL makedal)
        {
            ProductModelDAL = productmodeldal;
            MakeDAL = makedal;
        }
        // GET: Admin/ProductModel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            VMModelAdd vmModel = new VMModelAdd();
            vmModel.Makes = MakeDAL.GetList(x => x.IsActive == true);
            return View(vmModel);
        }
        [HttpPost]
        public ActionResult Add(ProductModel model)
        {
            ProductModel pm = new ProductModel();
            pm.ModelID = Guid.NewGuid();
            pm.ModelName = model.ModelName;
            pm.MakeID = model.MakeID;
            pm.CategoryID = model.CategoryID;
            pm.IsActive = true;

            Make m = MakeDAL.Get(x => x.MakeID == model.MakeID);
            if (m.Categories.Where(x => x.CategoryID == model.CategoryID && x.IsActive == true) != null && ModelState.IsValid && ProductModelDAL.Get(x => x.ModelName == model.ModelName) == null)
            {
                ProductModelDAL.Add(pm);
                return RedirectToAction("Index", "ProductModel");
            }
            else
            {
                VMModelAdd vmModel = new VMModelAdd();
                vmModel.Makes = MakeDAL.GetList(x => x.IsActive == true);
                return View(vmModel);
            }
        }

        public JsonResult GetModelByName(string id)
        {
            return Json(ProductModelDAL.GetList(x => x.ModelName == id && x.IsActive == true).Select(x => new { x.ModelID, x.ModelName }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetModelsByMakeAndCategory(ProductModel model)
        {
            return Json(ProductModelDAL.GetList(x => x.MakeID == model.MakeID && x.CategoryID == model.CategoryID && x.IsActive == true).Select(x => new { x.ModelID, x.ModelName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContextMenu()
        {
            return PartialView();
        }
    }
}