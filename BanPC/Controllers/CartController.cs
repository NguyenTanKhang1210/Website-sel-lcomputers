using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanPC.Controllers
{
    public class CartController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        XCart xcart = new  XCart();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();//ép kiểu
            return View("Index", listcart);
        }
        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id,product.Name,product.Image,product.Pricesale,1);
            //Add vào giỏ hàng
            List<CartItem> listcart = xcart.AddCart(cartitem);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            return RedirectToAction("Index", "Cart");
        }
        //CartUpdate
        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];//1,2,3,4,5,6
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult CartDelAll()
        {
            xcart.DelCart(null);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Payment()
        {
            
            List<CartItem> listcart = xcart.getCart();//ép kiểu
            //Kiểm tra đăng nhập trang người dùng==>khách hàng
            if (Session["UserCustomer"].Equals(""))
            {
                return Redirect("~/dang-nhap");// chuyển hướng đến URL
            }
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("Payment", listcart);
        }
        public ActionResult Purchase(FormCollection field)
        {
            //Lưu thông tin vào csdl order và orderdetail 
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            //Lấy thông tin
            String note = field["Note"];
            String ReceiverAddress = field["ReceiverAddress"];
            String ReceiverPhone = field["ReceiverPhone"];
            String ReceiverEmail = field["ReceiverEmail"];
            String ReceiverName = field["ReceiverName"];
            //Tạo đối tượng đơn hàng
            Order order = new Order();
            order.UserId = userid;
            order.Note = note;
            order.ReceiverAddress = ReceiverAddress;
            order.ReceiverPhone = ReceiverPhone;
            order.ReceiverEmail = ReceiverEmail;
            order.ReceiverName = ReceiverName;
            order.Status = 1;
            Random rd = new Random();
            order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            if(orderDAO.Insert(order)==1)
            {
                //thêm vào chi tiết đơn hàng
                List<CartItem> listcart = xcart.getCart();//ép kiểu
                foreach (CartItem cartItem in listcart) 
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.OrderId = order.Id;
                    orderdetail.ProductId = cartItem.ProductId;
                    orderdetail.Price = cartItem.Price;
                    orderdetail.Qty = cartItem.Qty;
                    orderdetail.Amount = cartItem.Amount;
                    orderDetailDAO.Insert(orderdetail);//lưu
                }
                //send email
                var strSanPham = "";
                var thanhtien = decimal.Zero;
                var TongTien = decimal.Zero;
                foreach(var sp in listcart)
                {
                    strSanPham += "<tr>";
                    strSanPham += "<td>" + sp.Name +"</td>";
                    strSanPham += "<td>" + sp.Qty +"</td>";
                    strSanPham += "<td>" + BanPC.Common.Common.FormatNumber(sp.Price,0) +"</td>";
                    strSanPham += "<tr>";
                    thanhtien += sp.Price * sp.Qty;
                }
                TongTien = thanhtien;
                string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.ReceiverName);
                contentCustomer = contentCustomer.Replace("{{Phone}}", order.ReceiverPhone);
                contentCustomer = contentCustomer.Replace("{{Email}}", order.ReceiverEmail);
                contentCustomer = contentCustomer.Replace("{{Diachigiaohang}}", order.ReceiverAddress);
                contentCustomer = contentCustomer.Replace("{{ThanhTien}}", BanPC.Common.Common.FormatNumber(thanhtien,0));
                contentCustomer = contentCustomer.Replace("{{TongTien}}", BanPC.Common.Common.FormatNumber(TongTien,0));
                BanPC.Common.Common.SendMail("BanPC","Don hang #"+ order.Code, contentCustomer.ToString(),order.ReceiverEmail);

                string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.ReceiverName);
                contentAdmin = contentAdmin.Replace("{{Phone}}", order.ReceiverPhone);
                contentAdmin = contentAdmin.Replace("{{Email}}", order.ReceiverEmail);
                contentAdmin = contentAdmin.Replace("{{Diachigiaohang}}", order.ReceiverAddress);
                contentAdmin = contentAdmin.Replace("{{ThanhTien}}", BanPC.Common.Common.FormatNumber(thanhtien, 0));
                contentAdmin = contentAdmin.Replace("{{TongTien}}", BanPC.Common.Common.FormatNumber(TongTien, 0));
                BanPC.Common.Common.SendMail("BanPC", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);

                xcart.DelCart();
            }
            return Redirect("~/thanh-cong");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}