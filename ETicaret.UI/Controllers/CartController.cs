using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Controllers
{
    public class CartController : Controller
    {
        IProductDAL productDAL;
        IBasketProductDAL basketDAL;

        User currentUser;

        public CartController(IProductDAL _productDAL, IBasketProductDAL _basketDAL)
        {
            productDAL = _productDAL;
            basketDAL = _basketDAL;
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderControl()
        {
            List<Product> outStock;
            List<BasketProduct> basketList;
            if (Session["user"] != null)
            {
                currentUser = Session["user"] as ETicaret.Model.Models.Entity.User;
                basketList = basketDAL.GetList(x => x.UserID == currentUser.UserID && x.IsActive == true).ToList();
                outStock = StockControl(basketList);
                if (outStock.Count > 0)
                {
                    TempData["OutStockError"] = outStock;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("UserOrder", "Order");
                }
            }
            else
            {
                basketList = Session["basket"] as List<BasketProduct>;
                outStock = StockControl(basketList);
                if (outStock.Count > 0)
                {
                    TempData["OutStockError"] = outStock;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("QuickOrder", "Order");
                }
            }
        }

        [HttpPost]
        public ActionResult CartIndex()
        {
            return PartialView(GetCartsList());
        }

        [HttpPost]
        public ActionResult CartDropDown()
        {
            return PartialView(GetCartsList());
        }

        private List<BasketProduct> GetCartsList()
        {
            if (Session["user"] != null)
                currentUser = Session["user"] as ETicaret.Model.Models.Entity.User;

            List<BasketProduct> carts;

            if (currentUser != null)
            {
                carts = currentUser.BasketProducts.Where(x => x.IsActive == true).ToList();
            }
            else
            {
                if (Session["basket"] == null)
                {
                    carts = new List<BasketProduct>();
                }
                else
                {
                    carts = Session["basket"] as List<BasketProduct>;
                }
            }

            return carts;
        }

        [HttpPost]
        public JsonResult CartRemove(string id)
        {
            try
            {
                if (Session["user"] != null)
                    currentUser = Session["user"] as ETicaret.Model.Models.Entity.User;

                List<BasketProduct> carts;
                if (currentUser != null)
                {
                    currentUser.BasketProducts.Where(x => x.ProductID == new Guid(id) & x.IsActive == true).FirstOrDefault().IsActive = false;
                    productDAL.Save();
                }
                else
                {
                    carts = Session["basket"] as List<BasketProduct>;
                    carts.Remove(carts.Where(x => x.ProductID == new Guid(id) && x.IsActive == true).FirstOrDefault());
                    Session["basket"] = carts;
                }
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult AddCart(string id, string count = "1")
        {
            int addCount = int.Parse(count);
            try
            {
                currentUser = Session["user"] as ETicaret.Model.Models.Entity.User;
                List<BasketProduct> basket;
                BasketProduct basketProduct;

                if (currentUser == null)
                {
                    basket = Session["basket"] as List<BasketProduct>;
                    if (basket == null)
                        basket = new List<BasketProduct>();
                    Boolean isAny = basket.Exists(x => x.ProductID == new Guid(id) & x.IsActive == true);
                    if (isAny)
                    {
                        basketProduct = basket.Where(x => x.ProductID == new Guid(id) & x.IsActive == true).FirstOrDefault();
                        basketProduct.Count += addCount;
                    }
                    else
                    {
                        basketProduct = new BasketProduct { ProductID = new Guid(id), IsActive = true, Count = addCount };
                        basketProduct.Product = productDAL.Get(x => x.ProductID == new Guid(id));
                        basket.Add(basketProduct);
                    }
                    if (basketProduct.Count == 0) { basket.Remove(basketProduct); }
                    Session["basket"] = basket;
                }
                else
                {
                    basket = basketDAL.GetList(x => x.UserID == currentUser.UserID).ToList();
                    Boolean isAny = basket.Exists(x => x.ProductID == new Guid(id));
                    if (isAny)
                    {
                        if (basket.Exists(x => x.IsActive == false && x.ProductID == new Guid(id)))
                        {
                            basketProduct = basket.Where(x => x.ProductID == new Guid(id)).FirstOrDefault();
                            basketProduct.Count = addCount;
                            basketProduct.IsActive = true;
                        }
                        else
                        {
                            basketProduct = basket.Where(x => x.ProductID == new Guid(id)).FirstOrDefault();
                            basketProduct.Count += addCount;
                            if (basketProduct.Count == 0) { basketProduct.IsActive = false; }
                        }
                    }
                    else
                    {
                        basketProduct = new BasketProduct { ProductID = new Guid(id), UserID = currentUser.UserID, User = currentUser, IsActive = true };
                        basketProduct.Count += addCount;
                        basketProduct.Product = productDAL.Get(x => x.ProductID == new Guid(id));
                        basketDAL.Add(basketProduct);
                    }
                    basketDAL.Save();
                }
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        public JsonResult ClearCart()
        {
            currentUser = Session["user"] as ETicaret.Model.Models.Entity.User;
            List<BasketProduct> basket;

            if (currentUser == null)
            {
                basket = new List<BasketProduct>();
                Session["basket"] = basket;
            }
            else
            {
                basket = basketDAL.GetList(x => x.UserID == currentUser.UserID && x.IsActive == true).ToList();
                foreach (var item in basket)
                {
                    item.IsActive = false;
                }
                basketDAL.Save();
            }

            return Json(true);
        }


        private List<Product> StockControl(List<BasketProduct> basket)
        {
            List<Product> outOrder = new List<Product>();
            List<BasketProduct> basketList = basket;

            foreach (var item in basketList)
            {
                int productStock = productDAL.Get(x => x.ProductID == item.ProductID).UnitsInStock;
                if (productStock - item.Count < 0)
                    outOrder.Add(productDAL.Get(x => x.ProductID == item.ProductID));
            }
            return outOrder;
        }
    }
}