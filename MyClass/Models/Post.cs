using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public int TopicId { get; set; }
        [Required(ErrorMessage = "Nội dung bài viết không để rỗng!")]
        public String Title { get; set; }
        public string Slug { get; set; }

        [Required(ErrorMessage = "Nội dung bài viết không để rỗng!")]
        public string Detail { get; set; }
        public string Img { get; set; }
        public string PostType { get; set; }

        [Required(ErrorMessage = "Mô tả SEO không để rỗng!")]
        public string MetaDesc { get; set; }

        [Required(ErrorMessage = "Từ khóa SEO không để rỗng!")]
        public string MetaKey { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }

    }
}
