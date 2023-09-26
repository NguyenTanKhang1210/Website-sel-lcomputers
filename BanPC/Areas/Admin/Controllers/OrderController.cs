using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace BanPC.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private OrderDAO orderDAO = new OrderDAO();
        private OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        // GET: Admin/Order
        public ActionResult Index()
        {
            List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ReceiverAddress,ReceiverPhone,ReceiverEmail,ReceiverName,Note,CreatedAt,UpdateBy,UpdatedAt,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderDAO.Insert(order);
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ReceiverAddress,ReceiverPhone,ReceiverEmail,ReceiverName,Note,CreatedAt,UpdateBy,UpdatedAt,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderDAO.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            orderDAO.Delete(order);
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = (order.Status == 1) ? 2 : 1;
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;//trạng thái rác = 0
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            order.Status = 2;//trạng thái rác = 0
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult Destroy(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status==2)
            {
                order.Status = 0;
                order.UpdateBy = 1;
                order.UpdatedAt = DateTime.Now;
            }
            else
            {
                if (order.Status==3) 
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng này đã vận chuyển không thể hủy");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng này đã giao không thể hủy");
                }
                return RedirectToAction("Index", "Order");
            }    
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đã hủy đơn hàng thành công");
            return RedirectToAction("Index", "Order");
        }

    }
}
