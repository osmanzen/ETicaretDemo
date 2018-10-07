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
    public class ProductController : Controller
    {
        public IProductDAL ProductDAL { get; set; }
        public IProductCommentDAL CommentDAL { get; set; }
        public IProductPictureDAL PictureDAL { get; set; }
        public IProductTechnicPropertyDAL PropertyValueDAL { get; set; }
        public IPropertyDAL PropertyDAL { get; set; }
        public IProductModelDAL ModelDAL { get; set; }
        public IMakeDAL MakeDAL { get; set; }
        public ICategoryDAL CategoryDAL { get; set; }

        public ProductController(IProductDAL productdal, IProductCommentDAL commentdal, IProductPictureDAL picturedal, IProductTechnicPropertyDAL propertyvaluedal, IPropertyDAL propertdal, IProductModelDAL modeldal, IMakeDAL makedal,ICategoryDAL categorydal)
        {
            ProductDAL = productdal;
            CommentDAL = commentdal;
            PictureDAL = picturedal;
            PropertyValueDAL = propertyvaluedal;
            PropertyDAL = propertdal;
            ModelDAL = modeldal;
            MakeDAL = makedal;
            CategoryDAL = categorydal;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetail(Guid id)
        {
            VMProductDetail vmProduct = new VMProductDetail();
            Product product = ProductDAL.Get(x => x.ProductID == id);
            product.ViewCount++;
            ProductDAL.Update(product);

            ProductModel pmodel = ModelDAL.Get(x => x.ModelID == product.ModelID);
            vmProduct.ProductList = pmodel.Products;
            vmProduct.Product = product;

            vmProduct.CampaignList = product.Campaigns.Where(x => x.EndingDate > DateTime.Now && x.StartedDate < DateTime.Now).ToList();

            Guid cid = product.ProductModel.Category.SCategory.CategoryID;
            ICollection<Category> subCategoryList = new HashSet<Category>();
            GetSubCategory(cid, ref subCategoryList);

            ICollection<ProductModel> SubModelList = new HashSet<ProductModel>();
            foreach (var cat in subCategoryList)
            {
                foreach (var model in cat.ProductModels)
                {
                    SubModelList.Add(model);
                }
            }
            vmProduct.SubModelList = SubModelList;
            return View(vmProduct);
        }

        public JsonResult AddCommentJson(string comment, Guid id)
        {
            ProductComment _comment = new ProductComment();
            _comment.ProductCommentID = Guid.NewGuid();
            _comment.IsActive = true;
            _comment.Content = comment;
            _comment.CreatedDate = DateTime.Now;
            _comment.UserID = ((User)Session["user"]).UserID;
            _comment.ProductID = id;

            CommentDAL.Add(_comment);
            return Json(CommentDAL.GetList(x => x.ProductCommentID == _comment.ProductCommentID).Select(x => new { x.ProductCommentID, x.Content, x.User.FullName, x.CreatedDate }).FirstOrDefault());
        }

        public JsonResult DeleteComment(Guid id)
        {
            ProductComment pComment = CommentDAL.Get(x => x.ProductCommentID == id);
            //foreach (var item in pComment.Product.ProductComments)
            //{

            //}

            //ProductDAL.Save();
            CommentDAL.Delete(pComment);

            return Json(true);
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