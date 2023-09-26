using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanPC.Controllers
{
    public class LienheController : Controller
    {
        // GET: Lienhe
        public ActionResult Index()
        {
            return View();
        }

        // Xử lý thông tin liên hệ
        [HttpPost]
        public ActionResult Index(Contact model)
        {
            if (ModelState.IsValid)
            {
               
                ViewBag.SuccessMessage = "Thông tin liên hệ đã được gửi thành công!";
                return View();
            }

            return View(model);
        }
    }
}