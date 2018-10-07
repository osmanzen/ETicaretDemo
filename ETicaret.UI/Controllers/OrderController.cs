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
    [OutputCache(NoStore = true, Duration = 0)]
    public class OrderController : Controller
    {
        ICityDAL cityDAL;
        IDistrictDAL districtDAL;
        IOrderDAL orderDAL;
        IUserDAL userDAL;
        IUserTypeDAL userTypeDAL;
        IProductDAL productDAL;
        IUserAddressDAL userAddressDAL;
        IUserCardDAL userCardDAL;
        IUserDetailDAL userDetailDAL;
        IBasketProductDAL basketProductDAL;
        User currentUser;

        public OrderController(ICityDAL _cityDAL, IDistrictDAL _districtDAL, IOrderDAL _orderDAL, IUserDAL _userDAL, IUserTypeDAL _userTypeDAL, IProductDAL _productDAL, IUserAddressDAL _userAddressDAL, IUserCardDAL _userCardDAL, IUserDetailDAL _userDetailDAL, IBasketProductDAL _basketProductDAL)
        {
            basketProductDAL = _basketProductDAL;
            userDetailDAL = _userDetailDAL;
            userCardDAL = _userCardDAL;
            userAddressDAL = _userAddressDAL;
            productDAL = _productDAL;
            userTypeDAL = _userTypeDAL;
            orderDAL = _orderDAL;
            cityDAL = _cityDAL;
            districtDAL = _districtDAL;
            userDAL = _userDAL;
            currentUser = System.Web.HttpContext.Current.Session["user"] as ETicaret.Model.Models.Entity.User;
        }

        // GET: QUICK ORDER
        [UserLogoutControl]
        public ActionResult QuickOrder()
        {
            if (Session["basket"] == null)
                return RedirectToAction("Index", "Cart");
            ViewBag.City = cityDAL.GetList().OrderBy(x => x.CityName).ToList();
            return View();
        }

        [HttpPost]
        [UserLogoutControl]
        public ActionResult QuickOrder(VMQuickOrder postOrder)
        {
            if (ModelState.IsValid)
            {
                User newGuest = new User()
                {
                    UserID = Guid.NewGuid(),
                    IsActive = false,
                    FullName = postOrder.FullName,
                    Email = postOrder.Email,
                    CreatedDate = DateTime.Now,
                    Password = "Guest",
                    UserTypeID = new Guid("b9b77014-f2a0-4d3c-9c0d-f64b3e36ec6f"),
                };

                UserType guestType = userTypeDAL.Get(x => x.UserTypeName == "guest");

                UserDetail guestDetail = new UserDetail()
                {
                    Gender = false,
                    IsActive = true,
                    Telephone = postOrder.Telephone,
                    UserDetailID = Guid.NewGuid(),
                    UserID = newGuest.UserID
                };

                UserAddress guestAddress = new UserAddress()
                {
                    UserAddressID = Guid.NewGuid(),
                    Address = postOrder.Address,
                    AddressFullName = "Guest",
                    DistrictID = postOrder.DistrictID,
                    IsActive = true,
                    UserID = newGuest.UserID
                };

                List<BasketProduct> guestBasket = Session["basket"] as List<BasketProduct>;
                decimal total = 0;


                foreach (var basket in guestBasket)
                {
                    decimal unitPrice = basket.Product.UnitPrice;
                    if (basket.Product.Campaigns.Count != 0)
                    {
                        foreach (var camp in basket.Product.Campaigns.Where(x => x.EndingDate > DateTime.Now && x.StartedDate < DateTime.Now && x.IsActive == true))
                        {
                            unitPrice = unitPrice * (1 - camp.Discount);
                        }
                    }

                    total += (unitPrice * basket.Count);
                }

                foreach (var basketProduct in guestBasket)
                {
                    var product = productDAL.Get(x => x.ProductID == basketProduct.ProductID);
                    product.UnitsInStock -= basketProduct.Count;
                    if (product.UnitsInStock <= 0)
                        product.IsActive = false;
                    productDAL.Update(product);
                }

                UserCard guestCard = new UserCard()
                {
                    CardNo = postOrder.CardNo,
                    ExpritionDate = Convert.ToDateTime("01-" + postOrder.Month + "-20" + postOrder.Year),
                    FullName = postOrder.CardFullName,
                    IsActive = true,
                    SecurityCode = postOrder.SecurityCode,
                    UserCardID = Guid.NewGuid(),
                    UserID = newGuest.UserID
                };

                Order guestOrder = new Order()
                {
                    OrderID = Guid.NewGuid(),
                    UserID = newGuest.UserID,
                    UserCardID = guestCard.UserCardID,
                    AddressID = guestAddress.UserAddressID,
                    UserDetailID = guestDetail.UserDetailID,
                    PaymentTypeID = new Guid("7e4289f3-ce9d-4c3b-847f-710664319e4b"),
                    CreatedTime = DateTime.Now,
                    IsActive = true,
                    OrderStatusID = new Guid("7A75CDFB-52A3-4455-A8C2-08D6DE333E45"),
                    TotalAmount = total
                };

                foreach (var basket in guestBasket)
                {
                    OrderDetail od = new OrderDetail()
                    {
                        OrderID = guestOrder.OrderID,
                        Count = basket.Count,
                        IsActive = true,
                        ProductID = basket.ProductID
                    };
                    guestOrder.OrderDetails.Add(od);
                }

                newGuest.Orders.Add(guestOrder);
                newGuest.UserAddresses.Add(guestAddress);
                newGuest.UserCards.Add(guestCard);
                newGuest.UserDetails.Add(guestDetail);
                newGuest.UserType = guestType;
                userDAL.Add(newGuest);

                Session["basket"] = null;

                ViewData["success"] = guestOrder.OrderID.ToString() + " numaralı siparişinizi bu kodu kullanarak takip edebilirsiniz. ";
                return View("QuickOrderDetail");
            }
            else
            {
                ViewBag.City = cityDAL.GetList().OrderBy(x => x.CityName).ToList();
                return View();
            }
        }

        [UserLogoutControl]
        public ActionResult QuickOrderDetail(Order od = null)
        {
            return View();
        }

        [HttpPost, UserLogoutControl]
        public ActionResult QuickOrderDetail(string id)
        {
            try
            {
                Order findOrder = orderDAL.Get(x => x.OrderID == new Guid(id));
                if (findOrder == null)
                    ViewBag.Error = "Sipariş Bulunamadı Yada Bir Kullanıcıya Ait. Lütfen Tekrar Deneyiniz.";
                return View(findOrder);
            }
            catch
            {
                ViewBag.Error = "Eksik yada Hatalı Giriş Yaptınız. Lütfen Tekrar Deneyiniz.";
                return View();
            }
        }

        // GET: USER ORDER
        [UserLoginControl]
        public ActionResult UserOrder()
        {
            if (currentUser != null)
            {
                VMOrderAddressDetail newUserOrder = new VMOrderAddressDetail();
                newUserOrder.UserDetail = userDetailDAL.Get(x => x.IsActive == true && x.UserID == currentUser.UserID);
                newUserOrder.UserAddresses = userAddressDAL.GetList(x => x.IsActive == true && x.UserID == currentUser.UserID).ToList();
                ViewBag.District = districtDAL.GetList(x => x.CityID == 34).OrderBy(x => x.DistrictName).ToList();
                ViewBag.City = cityDAL.GetList().OrderBy(x => x.CityName).ToList();

                return View(newUserOrder);
            }
            else
                return View("Index", "Login");
        }

        [HttpPost, UserLoginControl]
        public ActionResult OrderNewAddress(VMOrderAddressDetail postAddress, string udetail)
        {
            if (udetail != null)
            {
                UserDetail uDetail = new UserDetail()
                {
                    Gender = postAddress.Gender,
                    IsActive = true,
                    Telephone = postAddress.Telephone,
                    UserDetailID = Guid.NewGuid(),
                    UserID = currentUser.UserID
                };

                userDetailDAL.Add(uDetail);
            }

            UserAddress newAddress = new UserAddress()
            {
                UserID = currentUser.UserID,
                UserAddressID = Guid.NewGuid(),
                Address = postAddress.Address,
                AddressFullName = postAddress.AddressFullName,
                DistrictID = postAddress.DistrictID,
                IsActive = true,
            };

            userAddressDAL.Add(newAddress);
            return RedirectToAction("UserPayment/" + newAddress.UserAddressID);
        }

        [UserLoginControl]
        public ActionResult UserPayment(Guid id, string detail)
        {
            Session["address"] = id.ToString();

            VMUserNewCard newCard = new VMUserNewCard();
            ViewBag.UserCards = userCardDAL.GetList(x => x.IsActive == true && x.UserID == currentUser.UserID);

            return View(newCard);
        }

        [HttpPost, UserLoginControl]
        public ActionResult UserPayment(VMUserNewCard postCard)
        {
            UserCard newCard = new UserCard()
            {
                CardNo = postCard.CardNo,
                UserID = currentUser.UserID,
                ExpritionDate = Convert.ToDateTime("01-" + postCard.Month + "-20" + postCard.Year),
                FullName = postCard.CardFullName,
                SecurityCode = postCard.SecurityCode,
                UserCardID = Guid.NewGuid(),
                IsActive = postCard.isSave
            };

            userCardDAL.Add(newCard);

            return RedirectToAction("OrderSummary/" + newCard.UserCardID);
        }

        [UserLoginControl]
        public ActionResult OrderSummary(Guid id)
        {
            //List<Product> isValidStock = StokControl();

            //if (isValidStock.Count == 0)
            //{
            Guid selectedAddressID = new Guid(Session["address"].ToString());
            Guid selectedCardID = id;
            UserAddress address = userAddressDAL.Get(x => x.IsActive == true && x.UserAddressID == selectedAddressID);
            UserCard card = userCardDAL.Get(x => x.UserCardID == selectedCardID && x.IsActive == true && x.UserID == currentUser.UserID);
            UserDetail userDetail = userDetailDAL.Get(x => x.UserID == currentUser.UserID && x.IsActive == true);
            ICollection<BasketProduct> basketProducts = basketProductDAL.GetList(x => x.UserID == currentUser.UserID && x.IsActive == true);
            decimal totalAmount = 0;

            Order newOrder = new Order()
            {
                OrderID = Guid.NewGuid(),
                AddressID = selectedAddressID,
                CreatedTime = DateTime.Now,
                IsActive = true,
                OrderStatusID = new Guid("7a75cdfb-52a3-4455-a8c2-08d6de333e45"),
                UserDetailID = userDetail.UserDetailID,
                UserID = currentUser.UserID
            };

            newOrder.UserCardID = selectedCardID;

            foreach (var item in basketProducts)
            {


                decimal unitPrice = item.Product.UnitPrice;
                if (item.Product.Campaigns.Count != 0)
                {
                    foreach (var camp in item.Product.Campaigns.Where(x => x.EndingDate > DateTime.Now && x.StartedDate < DateTime.Now && x.IsActive == true))
                    {
                        unitPrice = unitPrice * (1 - camp.Discount);
                    }
                }

                totalAmount += (unitPrice * item.Count);


                OrderDetail newOD = new OrderDetail()
                {
                    OrderID = newOrder.OrderID,
                    Count = item.Count,
                    ProductID = item.ProductID,
                    IsActive = true
                };
                newOrder.OrderDetails.Add(newOD);

                Product product = productDAL.Get(x => x.ProductID == item.ProductID);
                product.UnitsInStock -= item.Count;
                productDAL.Update(product);

                item.IsActive = false;
                basketProductDAL.Update(item);
            }

            newOrder.TotalAmount = totalAmount;
            newOrder.PaymentTypeID = new Guid("7e4289f3-ce9d-4c3b-847f-710664319e4b");
            newOrder.UserCardID = selectedCardID;

            orderDAL.Add(newOrder);

            Session["address"] = null;
            Session["basket"] = null;

            TempData["Success"] = "Siparişiniz Alındı. İşlem Sürecini Bu Sayfadan Takip Edebilirsiniz.";

            return RedirectToAction("Index", "UserDetail");
            //}
            //else
            //{
            //    TempData["outOrder"] = isValidStock;
            //    return RedirectToAction("Index", "Cart");
            //}
        }

        [HttpPost]
        public JsonResult getDistrict(int cityID)
        {
            var district = districtDAL.GetList(x => x.CityID == cityID).Select(x => new { DistrictID = x.DistrictID, DistrictName = x.DistrictName }).ToList();
            return Json(district, JsonRequestBehavior.AllowGet);
        }
    }
}