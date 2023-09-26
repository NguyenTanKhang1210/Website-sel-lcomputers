using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MyClass.DAO;
using MyClass.Models;
using PagedList;

namespace BanPC.Controllers
{
    public class SiteController : Controller
    {
        private MyDBContext dBContext = new MyDBContext();
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        //URL mặc định hoặc URL bất kỳ

        // GET: Site
        public ActionResult Index(string slug = null, int? page=null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typelink = link.TypeLink;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug,  page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }
                    }
                }
                else
                {
                    //slug có trong bảng product
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }  
                    else
                    {

                        Post post = postDAO.getRow(slug);
                        if(post != null)
                        {
                            return this.PostDetail(post);
                        }   
                        else
                        {
                            return this.Error404(slug);
                        }    
                    }    
                    //slug có trong bảng Post Post-type=post
                    //Slug ko có trong bảng link 
                }
            }    
        }
        //Trang chủ
        public ActionResult Home()
        {
            List<ProductInfo> productNew = productDAO.getList(8);
            ViewBag.ProductNew = productNew;
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(id);// cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if(listcategory2.Count()!=0)
            {
                foreach(var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);// cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);// cấp 3
                        }
                    }
                }
            }    
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4);
            return View("HomeProduct", list);
        }
        //Nhóm Action Product
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1; //Trang hiện tại
            int pageSize = 4;// số mẫu tin hiển thị 1 trang
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        public ActionResult ProductCategory(string slug, int? page)
        {
            int pageNumber = page ?? 1; //Trang hiện tại
            int pageSize = 4;// số mẫu tin hiển thị 1 trang
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);// cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);// cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);// cấp 3
                        }
                    }
                }
            }

            //
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        public ActionResult ProductDetail(Product product)
        {
            return View("ProductDetail", product);
        }
        public ActionResult Product_sale()
        {
            var item=dBContext.Products.Where(x=>x.Sale==1).Take(8).ToList();
            return View("Product_sale",item);
        }
        //Nhóm Post
        public ActionResult Post(int? page)
        {
            List<PostInfo> list = postDAO.getList("Post");
            return View("Post", list);
        }
        public ActionResult PostTopic(string slug, int? page)
        {
            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic=topic;
            List<PostInfo> list = postDAO.getListByTopicId(topic.Id, "Post", null);
            return View("PostTopic", list);
        }
        public ActionResult PostPage(string slug)
        {
            Post post = postDAO.getRow(slug,"page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopicId(post.TopicId,"Post",post.Id);
            return View("PostDetail", post);
        }
        //hàm lỗi
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}