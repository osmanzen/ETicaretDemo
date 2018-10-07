using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        IOrderDAL OrderDAL;
        public OrderController(IOrderDAL orderdal)
        {
            OrderDAL = orderdal;
        }
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(OrderDAL.GetList(x => x.IsActive == true).Take(5));
        }

        public ActionResult Detail(Guid id)
        {
            return View(OrderDAL.Get(x => x.OrderID == id && x.IsActive));
        }
        [HttpPost]
        public ActionResult ChangeOrderStatus(Guid id, Guid statusid)
        {
            Order o = OrderDAL.Get(x => x.IsActive && x.OrderID == id);
            o.OrderStatusID = statusid;
            OrderDAL.Update(o);
            return RedirectToAction("Detail", new { id = id });
        }

        public JsonResult GetOrderCount()
        {
            return Json(OrderDAL.GetList(x => x.IsActive).Count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrdersForPage(int id)
        {
            return Json(OrderDAL.GetList(x => x.IsActive).Skip(id * 5).Take(5).Select(x => new { x.CreatedTime, x.PaymentType.PaymentName, x.OrderID, x.OrderStatusID, x.OrderStatus.Status }), JsonRequestBehavior.AllowGet);
        }
    }
}