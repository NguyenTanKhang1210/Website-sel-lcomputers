using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class PostInfo
    {
        public int Id { get; set; }

        public string TopicName { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        public string Detail { get; set; }
        public string Img { get; set; }
        public string PostType { get; set; }

        public string MetaDesc { get; set; }

        public string MetaKey { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }
    }
}
