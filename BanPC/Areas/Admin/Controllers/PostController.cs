﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanPC.Library;
using MyClass.DAO;
using MyClass.Models;

namespace BanPC.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private PostDAO postDAO = new PostDAO();
        private TopicDAO topicDAO = new TopicDAO();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.getList("Index", "Post"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TopicId,Title,Slug,Detail,Img,PostType,MetaDesc,MetaKey,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Status")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                post.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.CreatedAt = DateTime.Now;
                postDAO.Insert(post);
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.UpdatedAt = DateTime.Now;
                postDAO.Update(post);
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            postDAO.Delete(post);
            return RedirectToAction("Index");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = 0;//trạng thái rác = 0
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            post.Status = 2;//trạng thái rác = 0
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Post");
        }
        public ActionResult Trash()
        {
            return View(postDAO.getList("Trash"));
        }
    }
}
