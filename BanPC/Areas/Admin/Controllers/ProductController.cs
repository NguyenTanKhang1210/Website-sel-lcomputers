using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using BanPC.Library;
using System.Web.Services.Description;

namespace BanPC.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        MyDBContext dBContext = new MyDBContext();
        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        BrandDAO brandDAO = new BrandDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListBrandId = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            var listcatid = dBContext.Categorys.Where(m => m.Status != 0).ToList();
            ViewBag.ListCatId = new SelectList(listcatid, "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreatedAt = DateTime.Now;
                productDAO.Insert(product);
                return RedirectToAction("Index");
            }
           
            ViewBag.ListBrandId = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            var listcatid = dBContext.Categorys.Where(m => m.Status != 0).ToList();
            ViewBag.ListCatId = new SelectList(listcatid, "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListBrandId = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            var listcatid = dBContext.Categorys.Where(m => m.Status != 0).ToList();
            ViewBag.ListCatId = new SelectList(listcatid, "Id", "Name", 0);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreatedAt = DateTime.Now;
                productDAO.Update(product);
                return RedirectToAction("Index");
            }
            var listcatid = dBContext.Categorys.Where(m=>m.Status!=0).ToList();
            ViewBag.ListCatId = new SelectList(listcatid, "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;//trạng thái rác = 0
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;//trạng thái rác = 0
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public String ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Image;
            }
        }
        public String ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}
