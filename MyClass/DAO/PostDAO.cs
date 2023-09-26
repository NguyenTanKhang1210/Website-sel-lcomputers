using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();


        public List<PostInfo> getListByTopicId(int? topid, string type = "Post", int? notid=null)
        {
            List<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts
                .Join(
                db.Topics,
                p => p.TopicId,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topid).ToList();
            }
            else
            {
                list = db.Posts
                .Join(
                db.Topics,
                p => p.TopicId,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topid && m.Id!=notid).ToList();
            }    
            return list;
        }


        public List<PostInfo> getList(string type = "Post")
        {
            List<PostInfo> list = db.Posts
                .Join(
                db.Topics,
                p => p.TopicId,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,
                }
                )
                .Where(m => m.Status == 1 && m.PostType == type).ToList();
            return list;
        }
        //
        public List<Post> getList(string status = "All", string type = "Post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.PostType == type).ToList();
                        break;

                    }
            }

            return list;
        }
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        public Post getRow(string slug)
        {
            return db.Posts.Where(m => m.PostType == "Post" && m.Slug == slug && m.Status == 1).FirstOrDefault();
        }
        public Post getRow(string slug, string posttype)
        {
            return db.Posts.Where(m => m.Slug == slug && m.PostType == posttype && m.Status == 1).FirstOrDefault();
        }
        //
        //Thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhậ mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
