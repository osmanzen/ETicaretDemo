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
    [UserLoginControl, OutputCache(NoStore = true, Duration = 0)]
    public class UserDetailController : Controller
    {
        ETicaret.Model.Models.Entity.User currentUser;
        IUserDAL userDAL;
        IUserCardDAL userCardDAL;
        IUserAddressDAL userAddressDAL;
        ICityDAL cityDAL;
        IDistrictDAL districtDAL;
        IOrderDAL orderDAL;

        public UserDetailController(IUserDAL _userDAL, IUserCardDAL _userCardDAL, IUserAddressDAL _userAddressDAL, ICityDAL _cityDAL, IDistrictDAL _districtDAL, IOrderDAL _orderDAL)
        {
            orderDAL = _orderDAL;
            districtDAL = _districtDAL;
            cityDAL = _cityDAL;
            userDAL = _userDAL;
            userCardDAL = _userCardDAL;
            userAddressDAL = _userAddressDAL;
            currentUser = System.Web.HttpContext.Current.Session["user"] as ETicaret.Model.Models.Entity.User;
        }

        public ActionResult Index()
        {
            if (currentUser != null)
            {
                List<Order> orderList = orderDAL.GetList(x => x.UserID == currentUser.UserID && x.IsActive == true).OrderByDescending(x => x.CreatedTime).ToList();
                return View(orderList);
            }
            else
                return View("Index", "Login");
        }




        //USER ADDRESS
        public ActionResult UserAddress()
        {
            if (currentUser != null)
            {
                List<UserAddress> addresses = userAddressDAL.GetList(x => x.IsActive == true && x.UserID == currentUser.UserID).ToList();
                return View(addresses);
            }
            else
                return View("Index", "Login");
        }

        public ActionResult addressModal(string id)
        {
            ETicaret.Model.Models.Entity.UserAddress userAddress;
            if (id == "new")
            {
                userAddress = new Model.Models.Entity.UserAddress();
                ViewBag.District = districtDAL.GetList(x => x.CityID == 34).OrderBy(x => x.DistrictName).ToList();
            }
            else
            {
                userAddress = userAddressDAL.Get(x => x.UserAddressID == new Guid(id));
                ViewBag.District = districtDAL.GetList(x => x.CityID == userAddress.District.City.CityID).OrderBy(x => x.DistrictName).ToList();
                TempData["addressID"] = id;
            }

            ViewBag.City = cityDAL.GetList().OrderBy(x => x.CityName).ToList();

            return PartialView("addressModal", userAddress);
        }

        [HttpPost]
        public ActionResult addressModal(UserAddress postAddress)
        {
            if (TempData["addressID"] == null)
            {
                TempData["Success"] = "Ekleme İşlemi Başarılı";
            }
            else
            {
                Guid addressID = new Guid(TempData["addressID"].ToString());
                UserAddress updateAddress = userAddressDAL.Get(x => x.UserAddressID == addressID);
                updateAddress.IsActive = false;
                userAddressDAL.Update(updateAddress);
                TempData["Success"] = "Güncelleme İşlemi Başarılı";
            }

            UserAddress newAddress = new UserAddress()
            {
                UserAddressID = Guid.NewGuid(),
                IsActive = true,
                AddressFullName = postAddress.AddressFullName,
                DistrictID = postAddress.DistrictID,
                UserID = currentUser.UserID,
                Address = postAddress.Address
            };

            userAddressDAL.Add(newAddress);

            return RedirectToActionPermanent("UserAddress");
        }

        public ActionResult RemoveAddress(string id)
        {
            Guid addresID = new Guid(id);
            UserAddress removeAddress = userAddressDAL.Get(x => x.UserAddressID == addresID);
            removeAddress.IsActive = false;
            userAddressDAL.Update(removeAddress);

            return RedirectToActionPermanent("UserAddress");
        }


        //EDIT PROFILE
        public ActionResult EditProfile()
        {
            if (currentUser != null)
            {
                VMUserDetailEdit editUser = new VMUserDetailEdit();
                editUser.EMail = currentUser.Email;
                editUser.FullName = currentUser.FullName;

                if (currentUser.UserDetails.Count != 0)
                {
                    editUser.Telephone = currentUser.UserDetails.Where(x => x.IsActive == true).FirstOrDefault().Telephone;
                    editUser.Gender = currentUser.UserDetails.Where(x => x.IsActive == true).FirstOrDefault().Gender;
                }

                return View(editUser);
            }
            else
                return View("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditProfile(VMUserDetailEdit editUser)
        {
            if (ModelState.IsValid)
            {
                ETicaret.Model.Models.Entity.User lastUser = userDAL.Get(x => x.UserID == currentUser.UserID);

                if (editUser.LastPassword != null && editUser.Password != null && editUser.ConfirmPassword != null)
                {
                    if (editUser.LastPassword != lastUser.Password)
                    {
                        ViewBag.Error = "Şifre Hatalı";
                        return View();
                    }
                    else if (editUser.LastPassword == lastUser.Password)
                    {
                        if (editUser.Password == editUser.ConfirmPassword)
                        {
                            lastUser.Password = editUser.Password;
                        }
                        else
                        {
                            ViewBag.Error = "Girilen Şifreler Uyuşmuyor.";
                            return View();
                        }
                    }
                }

                lastUser.Email = editUser.EMail;
                lastUser.FullName = editUser.FullName;

                if (lastUser.UserDetails.Count > 0)
                {
                    lastUser.UserDetails.FirstOrDefault().Gender = editUser.Gender;
                    lastUser.UserDetails.FirstOrDefault().Telephone = editUser.Telephone;
                }
                else
                {
                    UserDetail newDetail = new UserDetail();
                    newDetail.IsActive = true;
                    newDetail.UserDetailID = new Guid();
                    newDetail.UserID = lastUser.UserID;
                    newDetail.Gender = editUser.Gender;
                    newDetail.Telephone = editUser.Telephone;

                    lastUser.UserDetails.Add(newDetail);
                }

                userDAL.Update(lastUser);
                Session["user"] = lastUser;
                if (lastUser.UserType.UserTypeName == "admin")
                    Session["admin"] = lastUser;

                ViewBag.Success = "Güncelleme Başarılı.";
                return View();
            }
            else
            {
                ViewBag.Error = "Güncelleme Başarısız. Lütfen Girilen Bilgileri Kontrol Ediniz.";
                return View();
            }
        }



        //USER CARDS
        public ActionResult UserCards()
        {
            if (currentUser != null)
            {
                return View();
            }
            else
                return View("Index", "Login");
        }

        [HttpPost]
        public JsonResult GetCardsList()
        {
            List<UserCard> userCards = userCardDAL.GetList(x => x.UserID == currentUser.UserID && x.IsActive == true).ToList();
            if (userCards.Count > 0)
            {
                List<VMUserCardsDetail> cardsList = new List<VMUserCardsDetail>();
                VMUserCardsDetail vmCard;
                foreach (var card in userCards)
                {
                    vmCard = new VMUserCardsDetail();
                    vmCard.ExpDate = card.ExpritionDate.ToString("MM") + "/" + card.ExpritionDate.ToString("yy");
                    vmCard.FullName = card.FullName;
                    vmCard.SecCode = card.SecurityCode;
                    vmCard.UserCardID = card.UserCardID.ToString();
                    if (card.CardNo[0] == '4')
                    {
                        vmCard.LogoUrl = "/Assets/img/visalogo.png";
                    }
                    else
                    {
                        vmCard.LogoUrl = "/Assets/img/masterlogo.png";
                    }
                    vmCard.UserCardNo = "**** **** **** " + card.CardNo.Substring(card.CardNo.Length - 4);
                    cardsList.Add(vmCard);
                }
                return Json(cardsList, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false);
        }

        [HttpPost]
        public JsonResult AddCard(VMUserCardsDetail newCard)
        {
            if (currentUser != null)
            {
                if (ModelState.IsValid)
                {
                    UserCard uCard = new UserCard();
                    uCard.UserCardID = Guid.NewGuid();
                    uCard.CardNo = newCard.UserCardNo;
                    uCard.ExpritionDate = Convert.ToDateTime(newCard.ExpDate.Replace(" ", "").Replace("\n", ""));
                    uCard.FullName = newCard.FullName;
                    uCard.IsActive = true;
                    uCard.SecurityCode = newCard.SecCode;
                    uCard.UserID = currentUser.UserID;
                    userCardDAL.Add(uCard);
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                return Json(false);
            }

        }

        [HttpPost]
        public JsonResult RemoveCard(string id)
        {
            if (currentUser != null)
            {
                UserCard removeCard = userCardDAL.Get(x => x.UserID == currentUser.UserID && x.UserCardID == new Guid(id));
                removeCard.IsActive = false;
                userCardDAL.Save();
                return Json(true);
            }
            else
                return Json(false);
        }
    }
}