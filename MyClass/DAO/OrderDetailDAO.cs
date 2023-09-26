using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDetailDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        public List<OrderDetail> getList(int? orderid)
        {
            return db.OrderDetails.Where(m=>m.OrderId== orderid).ToList();
        }
        public List<OrderDetail> getList(string status = "All")
        {
            List<OrderDetail> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.OrderDetails.ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.OrderDetails.ToList();
                        break;
                    }
                default:
                    {
                        list = db.OrderDetails.ToList();
                        break;

                    }
            }

            return list;
        }
        public OrderDetail getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.OrderDetails.Find(id);
            }
        }


        //
        //Thêm mẫu tin
        public int Insert(OrderDetail row)
        {
            db.OrderDetails.Add(row);
            return db.SaveChanges();
        }
        //Cập nhậ mẫu tin
        public int Update(OrderDetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(OrderDetail row)
        {
            db.OrderDetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
