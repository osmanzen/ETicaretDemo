using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Controllers
{
    public class CategoryController : Controller
    {
        public ICategoryDAL CategoryDAL { get; set; }
        public IProductDAL ProductDAL { get; set; }
        public IMakeDAL MakeDAL { get; set; }

        public CategoryController(ICategoryDAL categorydal, IProductDAL productdal, IMakeDAL makedal)
        {
            CategoryDAL = categorydal;
            ProductDAL = productdal;
            MakeDAL = makedal;
        }

        // GET: Category



        public ActionResult Index(Guid id)
        {
            if (CategoryDAL.Get(x => x.CategoryID == id) != null)
            {

                VMCategoryListing vmList = new VMCategoryListing();
                Session["id"] = id;

                ICollection<Category> subCategoryList = new HashSet<Category>();
                GetSubCategory(id, ref subCategoryList);

                vmList.CategoryList = subCategoryList;

                ICollection<VMCategoryProduct> list = new HashSet<VMCategoryProduct>();
                GetMainProductList(ref list, subCategoryList);


                vmList.ProductList = list;

                ICollection<Make> mList = new HashSet<Make>();
                foreach (var m in MakeDAL.GetList())
                {
                    foreach (var cat in subCategoryList)
                    {
                        foreach (var make in cat.Makes)
                        {
                            if (m == make)
                            {
                                mList.Add(m);
                            }
                        }
                    }
                }
                vmList.MakeList = mList;
                return View(vmList);
            }
            else if (MakeDAL.Get(x => x.MakeID == id) != null)
            {
                Make make = new Make();
                make = MakeDAL.Get(x => x.MakeID == id);
                Guid catID = Guid.Parse("35391ff3-c30d-4a6a-b30b-14079eaaa455");

                VMCategoryListing vmList = new VMCategoryListing();
                Session["id"] = catID;

                ICollection<Category> subCategoryList = new HashSet<Category>();
                foreach (var cat in make.Categories)
                {
                    subCategoryList.Add(cat);
                }

                vmList.CategoryList = subCategoryList;

                ICollection<VMCategoryProduct> list = new HashSet<VMCategoryProduct>();
                #region makeProductsGet
                foreach (var m in make.Models)
                {
                    foreach (var p in m.Products)
                    {
                        VMCategoryProduct vmProduct = new VMCategoryProduct();
                        vmProduct.ProductID = p.ProductID;
                        vmProduct.ModelID = p.ModelID;
                        vmProduct.MakeID = p.ProductModel.MakeID;
                        vmProduct.ProductName = p.ProductName;
                        vmProduct.UnitPrice = p.UnitPrice;
                        vmProduct.UnitsInStock = p.UnitsInStock;
                        vmProduct.ViewCount = p.ViewCount;
                        vmProduct.Description = p.Description;
                        vmProduct.IsActive = p.IsActive;
                        vmProduct.PicturePath = p.ProductPictures.First().PicturePath;
                        vmProduct.ModelName = p.ProductModel.ModelName;

                        if (p.Campaigns.Count != 0)
                        {
                            vmProduct.hasCampaign = true;

                            string discount = "";
                            decimal price = p.UnitPrice;
                            int count = p.Campaigns.Count;
                            foreach (var campaign in p.Campaigns.Where(x => x.IsActive == true))
                            {
                                if (campaign.StartedDate < DateTime.Now && campaign.EndingDate > DateTime.Now)
                                {


                                    discount += "%" + (Convert.ToInt32(campaign.Discount * 100)).ToString();
                                    price *= (1 - campaign.Discount);

                                    if (count > 1)
                                    {
                                        discount += "+"; count--;
                                    }
                                }
                            }
                            vmProduct.TotalPrice = price;
                            vmProduct.TotalDiscount = discount;
                            vmProduct.OldPrice = p.UnitPrice;

                        }
                        else
                        {
                            vmProduct.TotalPrice = p.UnitPrice;
                            vmProduct.TotalDiscount = "";
                            vmProduct.hasCampaign = false;
                        }

                        list.Add(vmProduct);
                    }

                }
                #endregion

                vmList.ProductList = list;
                ICollection<Make> mList = new HashSet<Make>();
                mList.Add(make);
                vmList.MakeList = mList;
                return View(vmList);
            }
            else
                return Json(false);
        }

        [HttpPost]
        public JsonResult GetFilterProduct(string filterID = null, int maxVal = 0, int minVal = 0)
        {
            Guid id = Guid.Parse(Session["id"].ToString());

            ICollection<Category> subCategoryList = new HashSet<Category>();
            GetSubCategory(id, ref subCategoryList);

            ICollection<VMCategoryProduct> list = new HashSet<VMCategoryProduct>();
            GetMainProductList(ref list, subCategoryList);


            ICollection<VMCategoryProduct> FilterList = new HashSet<VMCategoryProduct>();
            ICollection<VMCategoryProduct> FilterList2 = new HashSet<VMCategoryProduct>();
            if (filterID != null && filterID != "")
            {
                Guid filterId = Guid.Parse(filterID);
                foreach (var item in list)
                {
                    if (item.MakeID == filterId)
                    {
                        FilterList.Add(item);
                    }
                }
            }
            else
                FilterList = list;

            if (maxVal != 0 || minVal != 0)
            {
                foreach (var item in FilterList)
                {
                    if (item.TotalPrice <= maxVal && item.TotalPrice >= minVal)
                    {
                        FilterList2.Add(item);

                    }
                }
                return Json(FilterList2);
            }
            else
                return Json(FilterList);
        }


        public void GetMainProductList(ref ICollection<VMCategoryProduct> list, ICollection<Category> subCategoryList)
        {
            foreach (var c in subCategoryList)
            {
                foreach (var m in c.ProductModels)
                {
                    foreach (var p in m.Products)
                    {
                        VMCategoryProduct vmProduct = new VMCategoryProduct();
                        vmProduct.ProductID = p.ProductID;
                        vmProduct.ModelID = p.ModelID;
                        vmProduct.MakeID = p.ProductModel.MakeID;
                        vmProduct.ProductName = p.ProductName;
                        vmProduct.UnitPrice = p.UnitPrice;
                        vmProduct.UnitsInStock = p.UnitsInStock;
                        vmProduct.ViewCount = p.ViewCount;
                        vmProduct.Description = p.Description;
                        vmProduct.IsActive = p.IsActive;
                        vmProduct.PicturePath = p.ProductPictures.First().PicturePath;
                        vmProduct.ModelName = p.ProductModel.ModelName;

                        if (p.Campaigns.Count != 0)
                        {
                            vmProduct.hasCampaign = true;

                            string discount = "";
                            decimal price = p.UnitPrice;
                            int count = p.Campaigns.Count;
                            foreach (var campaign in p.Campaigns.Where(x => x.IsActive == true))
                            {
                                if (campaign.StartedDate < DateTime.Now && campaign.EndingDate > DateTime.Now)
                                {


                                    discount += "%" + (Convert.ToInt32(campaign.Discount * 100)).ToString();
                                    price *= (1 - campaign.Discount);

                                    if (count > 1)
                                    {
                                        discount += "+"; count--;
                                    }
                                }
                            }
                            vmProduct.TotalPrice = price;
                            vmProduct.TotalDiscount = discount;
                            vmProduct.OldPrice = p.UnitPrice;

                        }
                        else
                        {
                            vmProduct.TotalPrice = p.UnitPrice;
                            vmProduct.TotalDiscount = "";
                            vmProduct.hasCampaign = false;
                        }

                        list.Add(vmProduct);
                    }

                }
            }
        }

        public void GetSubCategory(Guid id, ref ICollection<Category> categoryList)
        {
            ICollection<Category> cList = CategoryDAL.GetList(x => x.SubCategoryID == id);

            if (cList.Count != 0)
            {
                foreach (var category in cList)
                {
                    GetSubCategory(category.CategoryID, ref categoryList);
                }
            }
            else
            {
                Category cat = CategoryDAL.Get(x => x.CategoryID == id);
                categoryList.Add(cat);
            }

        }
    }
}