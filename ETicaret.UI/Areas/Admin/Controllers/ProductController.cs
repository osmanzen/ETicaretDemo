using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Areas.Admin.Models.Attributes;
using ETicaret.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    //[LoginControl]
    public class ProductController : Controller
    {
        private IProductDAL ProductDAL;
        private IProductModelDAL ProductModelDAL;
        private ICategoryDAL CategoryDAL;
        public ProductController(IProductDAL productdal, IProductModelDAL productmodeldal, ICategoryDAL categorydal)
        {
            ProductDAL = productdal;
            ProductModelDAL = productmodeldal;
            CategoryDAL = categorydal;
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            VMProduct p = new VMProduct();
            //p.ProductModels = ProductModelDAL.GetList();
            Guid teknolojiID = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");
            p.Categories = CategoryDAL.GetList(x => x.SubCategoryID == teknolojiID);
            return View(p);
        }

        [HttpPost]
        public ActionResult Add(VMProduct model)
        {
            Product yeniUrun = new Product();

            ProductModel pm = ProductModelDAL.Get(x => x.ModelID == model.ModelID);
            yeniUrun.ModelID = pm.ModelID;
            yeniUrun.Description = model.Description;
            yeniUrun.IsActive = true;
            yeniUrun.ProductID = Guid.NewGuid();
            yeniUrun.ProductName = model.ProductName;
            yeniUrun.UnitPrice = model.UnitPrice;
            yeniUrun.UnitsInStock = model.UnitsInStock;
            yeniUrun.ViewCount = 0;

            Guid cID = pm.CategoryID;
            Category c = CategoryDAL.Get(x => x.CategoryID == cID);

            foreach (Property item in c.Properties)
            {
                string valProp = Request.Form[item.PropertyID.ToString()];
                if (valProp != null)
                {
                    ProductTechnicProperty temp = new ProductTechnicProperty();

                    temp.PropertyID = item.PropertyID;
                    temp.PropertyValue = valProp;
                    temp.ProductPropertyID = Guid.NewGuid();
                    yeniUrun.ProductTechnicProperties.Add(temp);
                }
            }

            foreach (HttpPostedFileBase item in model.Picture)
            {
                if (item != null)
                {
                    string pic = System.IO.Path.GetFileName(item.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Assets/img/products"), pic);
                    // file is uploaded
                    item.SaveAs(path);

                    ProductPicture temp = new ProductPicture();

                    temp.ProductPictureID = Guid.NewGuid();
                    temp.PicturePath = item.FileName;
                    temp.IsActive = true;
                    yeniUrun.ProductPictures.Add(temp);
                }
            }

            ProductDAL.Add(yeniUrun);


            return RedirectToAction("Add");
        }

        public ActionResult deneme()
        {

            return View();
        }
    }
}