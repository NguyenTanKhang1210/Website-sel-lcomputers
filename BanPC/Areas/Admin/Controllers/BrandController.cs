using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanPC.Library;
using MyClass.DAO;
using MyClass.Models;

namespace BanPC.Areas.Admin.Controllers
{
    public class BrandController : BaseController
    {
        BrandDAO brandDAO = new BrandDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/brand
        public ActionResult Index()
        {
            return View(brandDAO.getList("Index"));
        }

        // GET: Admin/brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Admin/brand/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(brandDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                //Xử lí thêm thông tin
                brand.Slug = XString.Str_Slug(brand.Name);
                if (brand.Orders == null)
                {
                    brand.Orders = 1;
                }
                else
                {
                    brand.Orders += 1;
                }
                //Upload file
                var img = Request.Files["img"];// Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = brand.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        brand.Img = imgName;
                        string PathDir = "~/Public/images/brands/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file

                brand.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                brand.CreatedAt = DateTime.Now;
                if (brandDAO.Insert(brand) == 1)
                {
                    Link link = new Link();
                    link.Slug = brand.Slug;
                    link.TableId = brand.Id;
                    link.TypeLink = "brand";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(brandDAO.getList("Index"), "Orders", "Name", 0);
            return View(brand);
        }

        // GET: Admin/brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(brandDAO.getList("Index"), "Orders", "Name", 0);
            return View(brand);
        }

        // POST: Admin/brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = XString.Str_Slug(brand.Name);
                if (brand.Orders == null)
                {
                    brand.Orders = 1;
                }
                else
                {
                    brand.Orders += 1;
                }
                //Upload file
                var img = Request.Files["img"];// Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = brand.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        brand.Img = imgName;
                        string PathDir = "~/Public/images/brands/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //xóa file
                        if (brand.Img.Length > 0)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), brand.Img);
                            System.IO.File.Delete(DelPath);//xóa hình
                        }
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file


                brand.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                brand.UpdatedAt = DateTime.Now;
                if (brandDAO.Update(brand) == 1)
                {
                    Link link = linkDAO.getRow(brand.Id, "brand");
                    link.Slug = brand.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(brandDAO.getList("Index"), "Orders", "Name", 0);
            return View(brand);
        }

        // GET: Admin/brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = brandDAO.getRow(id);
            Link link = linkDAO.getRow(brand.Id, "brand");
            //xóa hình ảnh
            string PathDir = "~/Public/images/brands/";
            //xóa file
            if (brand.Img != null)
            {
                string DelPath = Path.Combine(Server.MapPath(PathDir), brand.Img);
                System.IO.File.Delete(DelPath);//xóa hình
            }

            if (brandDAO.Delete(brand) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Trash", "Brand");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            brand.Status = (brand.Status == 1) ? 2 : 1;
            brand.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            brand.UpdatedAt = DateTime.Now;
            brandDAO.Update(brand);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            brand.Status = 0;//trạng thái rác = 0
            brand.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            brand.UpdatedAt = DateTime.Now;
            brandDAO.Update(brand);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Brand");
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Brand");
            }
            brand.Status = 2;//trạng thái rác = 0
            brand.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            brand.UpdatedAt = DateTime.Now;
            brandDAO.Update(brand);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Brand");
        }
        public ActionResult Trash()
        {
            return View(brandDAO.getList("Trash"));
        }
    }
}
