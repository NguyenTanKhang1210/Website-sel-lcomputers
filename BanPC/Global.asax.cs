﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanPC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            //Lưu thông tin đăng nhập quản lý
            Session["UserAdmin"] = "";
            //Lưu mã người đăng nhập quản lí
            Session["UserId"] = "1";
            Session["Fullname"] = "";
            Session["Img"] = "";

            //giỏ hàng
            Session["MyCart"] = "";
            //Lưu thông tin đăng nhập người dùng
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
        }

    }
}
