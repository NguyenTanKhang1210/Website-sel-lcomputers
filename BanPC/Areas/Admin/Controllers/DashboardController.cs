using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanPC.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}