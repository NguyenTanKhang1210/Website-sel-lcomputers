using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanPC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TatCaSanPham",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "TatCaBaiViet",
                url: "tat-ca-bai-viet",
                defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LienHe",
                url: "lien-he",
                defaults: new { controller = "Lienhe", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GioHang",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ThanhToan",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "ThanhCong",
                 url: "thanh-cong",
                 defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "TimKiem",
                url: "tim-kiem",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DangNhap",
                url: "dang-nhap",
                defaults: new { controller = "Customer", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DangXuat",
                url: "dang-xuat",
                defaults: new { controller = "Customer", action = "Logout", id = UrlParameter.Optional }
            );


            //Khai bao URL động - Nằm kế trên default
            routes.MapRoute(
                name: "SiteSlug",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
