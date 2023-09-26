using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        public List<User> getList()
        {
            return db.Users.ToList();
        }

        public List<User> getList(string page = "All", string roles = null)
        {
            List<User> list = null;
            switch (page)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0 && m.Roles == roles).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0 && m.Roles == roles).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Users.Where(m => m.Roles == roles).ToList();
                        break;
                    }

            }
            return list;
        }
        //Trả về 1 mẫu tin
        public User getRow()
        {
            return db.Users.Find();
        }
        public User getRow(int? id)
        {
            return db.Users.Find(id);
        }
        public User getRow(string username, string roles)
        {
            return db.Users.Where(m => m.Status == 1 && m.Roles == roles && (m.UserName == username || m.Email == username)).FirstOrDefault();

        }
        //
        //Thêm mẫu tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }
        //Cập nhậ mẫu tin
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }
    }
}
