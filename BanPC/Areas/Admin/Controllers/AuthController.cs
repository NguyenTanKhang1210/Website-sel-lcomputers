using BanPC.Library;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanPC.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        MyDBContext db = new MyDBContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            if (!Session["UserAdmin"].Equals(""))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
            string strerror = "";
            string username = field["username"];
            string password = XString.ToMD5(field["password"]);//phãi mã hóa

            User rowuser = db.Users
                .Where(m => m.Status == 1 && (m.UserName == username || m.Email == username))
                .FirstOrDefault();
            if (rowuser == null)
            {
                strerror = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (rowuser.Password.Equals(password))
                {
                    Session["UserAdmin"] = rowuser.UserName;
                    Session["UserId"] = rowuser.Id;
                    Session["Fullname"] = rowuser.FullName;
                    Session["Img"] = rowuser.Img;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    strerror = "Mật khẩu không đúng";
                }
            }
            ViewBag.Error = "<span class='text-danger'>" + strerror + "</span>";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            //Lưu mã người đăng nhập quản lí
            Session["UserId"] = "1";
            Session["Fullname"] = "";
            Session["Img"] = "";
            return RedirectToAction("Login", "Auth");
        }
    }
}