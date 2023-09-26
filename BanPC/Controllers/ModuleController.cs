using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace BanPC.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Module
        public ActionResult ManiMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu",0);
            return View("ManiMenu",list);
        }
        public ActionResult ManiMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu",id);
            if(list.Count==0)
            {
                return View("ManiMenuSub1", menu); //ko có con
            }
            else
            {
                ViewBag.Menu = menu;
                return View("ManiMenuSub2", list);// có con

            }
        }
        //slider show
        public ActionResult Slidershow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slidershow");
            return View("Slidershow", list);
        }
        //ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory",list);
        }
        //PostLastNews
        public ActionResult PostLastNews()
        {
            return View("PostLastNews");
        }
        public ActionResult MenuFooter()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("MenuFooter", list);
        }
    }
}