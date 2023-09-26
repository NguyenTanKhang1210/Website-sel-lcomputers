using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        public List<Product> SearchByKey(string key)
        {
            return db.Products.SqlQuery("Select * from Products where Name like '%"+key+"%'").ToList();
        }
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
                .Join(
                db.Categorys,
                p=>p.CatId,
                c=>c.Id,
                (p, c) => new ProductInfo
                { 
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    BrandId = p.BrandId,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m=>m.Status == 1 && listcatid.Contains(m.CatId)).OrderByDescending(m=>m.CreatedAt).Take(take).ToList();
            return list;
        }


        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    BrandId = p.BrandId,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId)).OrderByDescending(m => m.CreatedAt).ToPagedList(pageNumber, pageSize);
            return list;
        }

        public List<ProductInfo> getList(int take)
        {
            List<ProductInfo> list = db.Products
                .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    BrandId = p.BrandId,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt).Take(take).ToList();
            return list;
        }


        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    BrandId = p.BrandId,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m=>m.Status==1)
                .OrderByDescending(m=>m.CreatedAt).ToPagedList(pageNumber, pageSize);
            return list;
        }

        public List<ProductInfo> getList(string status = "All")
        {
            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products
                            .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    BrandId = p.BrandId,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products
                            .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    BrandId = p.BrandId,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                            .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products
                            .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    BrandId = p.BrandId,
                    Detail = p.Detail,
                    Image = p.Image,
                    Pricebuy = p.Pricebuy,
                    Pricesale = p.Pricesale,
                    Numbers = p.Numbers,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                            .ToList();
                        break;

                    }
            }

            return list;
        }
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m=>m.Slug==slug && m.Status==1).FirstOrDefault();   
        }

        //
        //Thêm mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhậ mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
