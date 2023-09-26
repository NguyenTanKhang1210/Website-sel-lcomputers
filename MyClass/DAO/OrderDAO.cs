using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        public List<Order> getList(string status = "All")
        {
            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;

                    }
            }

            return list;
        }
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }
        public List<OrderInfo> getListJoin(string status = "All")
        {
            List<OrderInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                ReceiverAddress = o.ReceiverAddress,
                                ReceiverPhone = o.ReceiverPhone,
                                ReceiverEmail = o.ReceiverEmail,
                                ReceiverName = o.ReceiverName,
                                Note = o.Note,
                                CreatedAt = o.CreatedAt,
                                UpdateBy = o.UpdateBy,
                                UpdatedAt = o.UpdatedAt,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Qty = od.Qty,
                                Amount = od.Amount
                            }
                            )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                ReceiverAddress = o.ReceiverAddress,
                                ReceiverPhone = o.ReceiverPhone,
                                ReceiverEmail = o.ReceiverEmail,
                                ReceiverName = o.ReceiverName,
                                Note = o.Note,
                                CreatedAt = o.CreatedAt,
                                UpdateBy = o.UpdateBy,
                                UpdatedAt = o.UpdatedAt,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Qty = od.Qty,
                                Amount = od.Amount
                            }
                            )
                            .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                ReceiverAddress = o.ReceiverAddress,
                                ReceiverPhone = o.ReceiverPhone,
                                ReceiverEmail = o.ReceiverEmail,
                                ReceiverName = o.ReceiverName,
                                Note = o.Note,
                                CreatedAt = o.CreatedAt,
                                UpdateBy = o.UpdateBy,
                                UpdatedAt = o.UpdatedAt,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Qty = od.Qty,
                                Amount = od.Amount
                            }
                            )
                            .ToList();
                        break;

                    }
            }

            return list;
        }


        //
        //Thêm mẫu tin
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhậ mẫu tin
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
