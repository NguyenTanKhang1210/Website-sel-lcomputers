using BanPC.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace BanPC.Controllers
{
    public class CustomerController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: Customer
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection filed)
        {
            String username = filed["username"];
            String password = XString.ToMD5( filed["password"]);
            User row_user = userDAO.getRow(username, "Customer");
            String strError = "";
            if (row_user == null)
            {
                strError = "Tên đăng nhập không tồn tài";
            }
            else
            {
                if (password.Equals(row_user.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["CustomerId"] = row_user.Id;
                    return Redirect("~/");
                }   
                else
                {
                    strError = password;
                }    
            }    
            ViewBag.Error = "<span class = 'text-danger'>" +strError+ "</div>";
            return View("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Customer");
        }
    }
}