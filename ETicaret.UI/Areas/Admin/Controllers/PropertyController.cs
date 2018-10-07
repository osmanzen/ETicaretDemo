using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    public class PropertyController : Controller
    {
        IPropertyDAL PropertyDAL;
        ICategoryDAL CategoryDAL;
        public PropertyController(IPropertyDAL propertydal, ICategoryDAL categorydal)
        {
            PropertyDAL = propertydal;
            CategoryDAL = categorydal;
        }
        // GET: Admin/Property
        public ActionResult Index()
        {
            return View(PropertyDAL.GetList());
        }

        public JsonResult GetPropertiesByCategory(Guid id)
        {
            Category c = CategoryDAL.Get(x => x.CategoryID == id);
            return Json(c.Properties.Select(x => new { x.PropertyID, x.PropertyName }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPropertiesByName(string id)
        {

            return Json(PropertyDAL.GetList(x => x.PropertyName.Contains(id)).Select(x => new { x.PropertyID, x.PropertyName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            Property property = new Property();
            Guid temp = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
            property.Categories = CategoryDAL.GetList(x => x.SubCategoryID == temp);
            return View();
        }

        [HttpPost]
        public ActionResult Add(Property model, Guid CategoryID)
        {
            model.PropertyID = Guid.NewGuid();
            model.IsActive = true;
            //
            Category c = CategoryDAL.Get(x => x.CategoryID == CategoryID);
            if (c.Properties.Where(x => x.PropertyName == model.PropertyName).FirstOrDefault() != null)
                return Json(false, JsonRequestBehavior.AllowGet);
            if (PropertyDAL.Get(x => x.PropertyName == model.PropertyName) != null)
                model = PropertyDAL.Get(x => x.PropertyName == model.PropertyName);

            c.Properties.Add(model);
            CategoryDAL.Save();

            Property temp = PropertyDAL.Get(x => x.PropertyName == model.PropertyName);
            return Json(new { temp.PropertyName, temp.PropertyID }, JsonRequestBehavior.AllowGet);
        }
    }
}