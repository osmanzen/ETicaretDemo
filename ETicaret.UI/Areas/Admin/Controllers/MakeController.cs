using ETicaret.DAL.Abstract;
using ETicaret.DAL.Concrete.EntityFramework.Context;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Areas.Admin.Models.DTO;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    public class MakeController : Controller
    {
        private IMakeDAL MakeDAL;
        private ICategoryDAL CategoryDAL;

        public MakeController(IMakeDAL makedal, ICategoryDAL cateforydal)
        {
            MakeDAL = makedal;
            CategoryDAL = cateforydal;
        }
        // GET: Admin/Make
        public ActionResult Index()
        {
            return View(MakeDAL.GetList());
        }

        public ActionResult Add()
        {
            VMMakeAdd vmMake = new VMMakeAdd();
            Guid teknolojiID = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
            vmMake.Categories = CategoryDAL.GetList(x => x.SubCategoryID == teknolojiID && x.IsActive == true);
            return View(vmMake);
        }
        [HttpPost]
        public ActionResult Add(Make model, List<Guid> CategoryID)
        {
            Make make = new Make();
            make.MakeID = Guid.NewGuid();
            make.MakeName = model.MakeName;
            make.IsActive = true;
            foreach (var item in CategoryID)
            {
                Guid id = Guid.Parse(item.ToString());
                Category cat = CategoryDAL.Get(x => x.CategoryID == id && x.IsActive == true);
                if (CategoryDAL.Get(x => x.SubCategoryID == cat.CategoryID) == null)
                    make.Categories.Add(cat);
            }

            if (MakeDAL.Get(x => x.MakeName == make.MakeName) != null || !ModelState.IsValid)
            {
                VMMakeAdd vmMake = new VMMakeAdd();
                Guid teknolojiID = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
                vmMake.Categories = CategoryDAL.GetList(x => x.SubCategoryID == teknolojiID && x.IsActive == true);
                return View(vmMake);
            }
            else
            {
                MakeDAL.Add(make);
                return RedirectToAction("Index", "Make");
            }
        }

        public ActionResult Edit(Guid id)
        {
            VMMakeCategory vmMC = new VMMakeCategory();
            vmMC.Make = MakeDAL.Get(x => x.MakeID == id);
            Guid cid = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
            vmMC.Categories = CategoryDAL.GetList(x => x.SubCategoryID == cid && x.IsActive == true);

            return View(vmMC);
        }
        [HttpPost]
        public ActionResult Edit(Make make, List<Guid> CategoryID)
        {
            Make oldMake = MakeDAL.Get(x => x.MakeID == make.MakeID);
            oldMake.IsActive = false;
            MakeDAL.Update(oldMake);

            Make newMake = new Make();
            newMake.MakeID = Guid.NewGuid();
            newMake.MakeName = make.MakeName;
            newMake.IsActive = true;

            foreach (var c in CategoryID)
            {
                Category category = CategoryDAL.Get(x => x.CategoryID == c);
                newMake.Categories.Add(category);
            }
            foreach (var model in oldMake.Models)
            {
                newMake.Models.Add(model);
            }

            MakeDAL.Add(newMake);

            return RedirectToAction("Index", "Make");
        }

        public JsonResult GetMakesByCategory(Guid id)
        {
            return Json(MakeDAL.GetList(x => x.Categories.Where(f => f.CategoryID == id && x.IsActive == true).FirstOrDefault() != null).Select(x => new { x.MakeID, x.MakeName }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMakeByName(string id)
        {
            return Json(MakeDAL.GetList(x => x.MakeName == id && x.IsActive == true).Select(x => new { x.MakeName, x.MakeID }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoryByMake(Guid id)
        {
            Make make = MakeDAL.Get(x => x.MakeID == id);
            return Json(CategoryDAL.GetList(x => x.IsActive == true).Where(x => x.Makes.Contains(make)).Select(x => new { x.CategoryID, x.CategoryName }).ToList(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddModal(Guid id)
        {
            VMMakeModal model = new VMMakeModal();
            model.Category = CategoryDAL.Get(x => x.CategoryID == id);
            model.Categories = CategoryDAL.GetList();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult AddModal(VMMakeModal model, List<Guid> CategoryList)
        {

            Make temp = MakeDAL.Get(x => x.MakeName == model.MakeName);
            if (temp != null)
            {
                foreach (Guid item in CategoryList)
                {
                    CategoryDAL.Get(x => x.CategoryID == item).Makes.Add(temp);
                }
                CategoryDAL.Save();
            }
            else
            {
                temp = new Make();
                temp.MakeID = Guid.NewGuid();
                temp.MakeName = model.MakeName;
                temp.IsActive = true;

                foreach (Guid item in CategoryList)
                {
                    temp.Categories.Add(CategoryDAL.Get(x => x.CategoryID == item));
                }

                MakeDAL.Add(temp);

            }
            return Json(true);
        }

        public ActionResult EditModal(Guid id)
        {
            return PartialView();
        }

        public ActionResult DeleteModal(Guid id)
        {
            return View(MakeDAL.Get(x => x.MakeID == id));
        }
        public JsonResult DeleteJson(Guid id)
        {
            Make modal = MakeDAL.Get(x => x.MakeID == id);

            MakeDAL.Delete(modal);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContextMenu()
        {
            return PartialView();
        }
    }
}