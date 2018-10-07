using ETicaret.DAL.Abstract;
using ETicaret.Model.Models.Entity;
using ETicaret.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.UI.Areas.Admin.Controllers
{
    public class CampaignController : Controller
    {
        public ICampaignDAL CampaignDAL { get; set; }
        public ICategoryDAL CategoryDAL { get; set; }
        public IMakeDAL MakeDAL { get; set; }
        public IProductModelDAL ModelDAL { get; set; }
        public IProductDAL ProductDAL { get; set; }
        public IUserDAL UserDAL { get; set; }
        public CampaignController(ICampaignDAL campaigndal,ICategoryDAL categorydal,IMakeDAL makedal,IProductModelDAL modeldal,IProductDAL productdal,IUserDAL userdal)
        {
            CampaignDAL = campaigndal;
            CategoryDAL = categorydal;
            MakeDAL = makedal;
            ModelDAL = modeldal;
            ProductDAL = productdal;
            UserDAL = userdal;
        }
        // GET: Admin/Campaign
        public ActionResult Index()
        {
            

            return View(CampaignDAL.GetList());
        }
        public ActionResult Add()
        {
            VMCampaignAdd vmCampaign = new VMCampaignAdd();
            vmCampaign.MakeList = MakeDAL.GetList();

            return View(vmCampaign);
        }
        [HttpPost]
        public ActionResult Add(VMCampaignAdd model, HttpPostedFileBase file,string MakeID, string StartedDate, int EndingDate)
        {
            Campaign campaign = new Campaign();
            campaign.CampaignID = Guid.NewGuid();
            campaign.Title = model.Campaign.Title;
            campaign.StartedDate = DateTime.Parse(StartedDate);
            campaign.EndingDate = campaign.StartedDate.AddDays(EndingDate);
            campaign.Discount = model.Campaign.Discount;
            campaign.Status = model.Campaign.Status;
            campaign.IsActive = true;
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Assets/img/campaign"),Path.GetFileName(file.FileName));
                file.SaveAs(path);
                campaign.PictureUrl = Path.GetFileName(file.FileName);
            }
            Category cat = CategoryDAL.Get();
            Guid temp = Guid.Parse(MakeID);
            Make make = MakeDAL.Get(x=>x.MakeID==temp);

            foreach (var m in make.Models)
            {
                foreach (var p in m.Products)
                {
                    campaign.Products.Add(p);
                }
            }

            CampaignDAL.Add(campaign);

            return RedirectToAction("Index", "Campaign");
        }

        protected void SendMail(Campaign c)
        {
            ICollection<User> userList = UserDAL.GetList();

            foreach (User user in userList)
            {
                    if (user.UserType.UserTypeName == "standart")
                    {
                        SmtpClient smtp = new SmtpClient();

                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;

                        smtp.Credentials = new NetworkCredential("newspaperswebsiteba@gmail.com", "Ba123456");

                        MailMessage ePosta = new MailMessage();

                        ePosta.From = new MailAddress("newspaperswebsiteba@gmail.com", "ByPorto");
                        ePosta.To.Add(user.Email);
                        ePosta.Subject = c.Title;
                        ePosta.Body = c.Title + "\n" + c.StartedDate+"-"+c.EndingDate +"arasında geçerli Kampanyayı Sakın Kaçırmayın";

                        smtp.Send(ePosta);
                    }
                


            }
        }

    }
}