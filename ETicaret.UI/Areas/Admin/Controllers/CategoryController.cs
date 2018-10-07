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
    public class CategoryController : Controller
    {
        public ICategoryDAL CategoryDAL { get; set; }
        public IMakeDAL MakeDAL { get; set; }
        public CategoryController(ICategoryDAL categorydal, IMakeDAL makedal)
        {
            CategoryDAL = categorydal;
            MakeDAL = makedal;
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(CategoryDAL.GetList(x => x.IsActive == true));
        }

        public ActionResult Add()
        {
            VMCategory vmCat = new VMCategory();
            Guid temp = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
            vmCat.Categories = CategoryDAL.GetList(x => x.SubCategoryID == temp);
            return View(vmCat);
        }
        [HttpPost]
        public ActionResult Add(Category model)
        {
            if (model.SubCategoryID == null)
            {
                model.SubCategoryID = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
            }
            model.CategoryID = Guid.NewGuid();
            model.IsActive = true;
            if (ModelState.IsValid)
            {
                if (CategoryDAL.GetList(x => x.SubCategoryID == model.SubCategoryID && x.IsActive == true).Where(x => x.CategoryName == model.CategoryName).FirstOrDefault() == null)
                {
                    //bağlı tablolar taşınıyor...
                    Category tempCategory = CategoryDAL.Get(x => x.CategoryID == model.SubCategoryID && x.IsActive == true);
                    foreach (ProductModel item in tempCategory.ProductModels)
                    {
                        item.Category = model;
                    }
                    foreach (Property item in tempCategory.Properties)
                    {
                        model.Properties.Add(item);
                    }
                    tempCategory.Properties.Clear();
                    foreach (Make item in tempCategory.Makes)
                    {
                        model.Makes.Add(item);
                    }
                    tempCategory.Makes.Clear();
                    CategoryDAL.Add(model);
                }
                else
                {
                    VMCategory vmCat = new VMCategory();
                    Guid temp = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
                    vmCat.Categories = CategoryDAL.GetList(x => x.SubCategoryID == temp);
                    return View(vmCat);
                }
            }
            else
            {
                VMCategory vmCat = new VMCategory();
                Guid temp = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
                vmCat.Categories = CategoryDAL.GetList(x => x.SubCategoryID == temp);
                return View(vmCat);
            }

            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(Guid id)
        {
            VMCategoryEdit vmEdit = new VMCategoryEdit();
            vmEdit.Category = CategoryDAL.Get(x => x.IsActive && x.CategoryID == id);

            for (Category i = vmEdit.Category.SCategory; i != null; i = i.SCategory)
            {
                if (i.CategoryID == Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455"))
                {
                    vmEdit.ListCategories.Add(CategoryDAL.GetList(x => x.SubCategoryID == i.CategoryID));
                    break;
                }

                vmEdit.ListCategories.Add(CategoryDAL.GetList(x => x.SubCategoryID == i.SubCategoryID));
                if (i.SCategory.CategoryID == Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455"))
                    break;
            }
            Guid temp = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");

            return View(vmEdit);
        }
        [HttpPost]
        public ActionResult Edit(VMCategory model, Guid CategoryID, Guid id)
        {
            Category cat = CategoryDAL.Get(x => x.CategoryID == id);
            cat.IsActive = false;
            CategoryDAL.Update(cat);

            Category c = new Category();
            c.CategoryID = Guid.NewGuid();
            c.CategoryName = model.Category.CategoryName;
            c.Description = model.Category.Description;
            c.SubCategoryID = CategoryID;
            c.IsActive = true;

            foreach (var item in cat.Makes)
            {
                c.Makes.Add(item);
            }
            foreach (var item in cat.Properties)
            {
                c.Properties.Add(item);
            }
            foreach (var item in cat.ProductModels)
            {
                c.ProductModels.Add(item);
            }

            CategoryDAL.Add(c);
            return RedirectToAction("Index", "Category");
        }

        public ActionResult GetSubCategories(string id = null)
        {
            if (id == null)
            {
                id = "35391ff3-c30d-4a6a-b30b-14079eaaa455";
            }

            Guid temp = Guid.Parse(id);

            ICollection<Category> result = CategoryDAL.GetList(x => x.SubCategoryID == temp && x.IsActive == true);

            if (result.Count != 0)
                return PartialView(result);

            return null;
        }

        public JsonResult GetSubCategoriesJson(string id = null)
        {
            if (id == null)
            {
                id = "35391ff3-c30d-4a6a-b30b-14079eaaa455";
            }

            Guid temp = Guid.Parse(id);



            return Json(CategoryDAL.GetList(x => x.SubCategoryID == temp && x.IsActive == true).Select(x => new { x.CategoryID, x.CategoryName }), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCategoriesJson()
        {
            return Json(CategoryDAL.GetList(x => x.IsActive == true).Select(x => new { x.CategoryID, x.CategoryName }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoriesByMake(string id = null)
        {
            if (id == null)
            {
                return null;
            }

            Guid temp = Guid.Parse(id);

            ICollection<Category> result = MakeDAL.Get(x => x.MakeID == temp && x.IsActive == true).Categories;

            if (result.Count != 0)
                return Json(result.Select(x => new { x.CategoryID, x.CategoryName }), JsonRequestBehavior.AllowGet);

            return null;
        }

        public ActionResult AddModal()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult AddModal(Category model)
        {
            model.CategoryID = Guid.NewGuid();
            model.IsActive = true;
            CategoryDAL.Add(model);
            return Json(true);
        }

        public ActionResult EditModal(Guid id)
        {
            return PartialView(CategoryDAL.Get(x => x.CategoryID == id));
        }
        [HttpPost]
        public JsonResult EditModal(Category model)
        {
            Category temp = CategoryDAL.Get(x => x.CategoryID == model.CategoryID);
            temp.IsActive = false;
            CategoryDAL.Update(temp);

            Category temp2 = new Category()
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = model.CategoryName,
                Description = model.Description,
                IsActive = true,
                SubCategoryID = temp.SubCategoryID,
            };

            foreach (Make item in temp.Makes)
            {
                temp2.Makes.Add(item);
            }
            foreach (ProductModel item in temp.ProductModels)
            {
                temp2.ProductModels.Add(item);
            }
            foreach (Property item in temp.Properties)
            {
                temp2.Properties.Add(item);
            }


            CategoryDAL.Add(temp2);

            return Json(true);
        }

        public ActionResult DeleteModal(Guid id)
        {
            return PartialView(CategoryDAL.Get(x => x.CategoryID == id));
        }

        public JsonResult DeleteJson(Guid id)
        {
            Category temp = CategoryDAL.Get(x => x.CategoryID == id);
            temp.IsActive = false;
            CategoryDAL.Update(temp);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContextMenu()
        {
            return PartialView();
        }
    }
}