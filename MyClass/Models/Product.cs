using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Slug { get; set; }
        [Required(ErrorMessage = "Chi tiết sản phẩm không được để rỗng")]
        public int CatId { get; set; }
        public int? BrandId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Pricebuy { get; set; }
        public decimal Pricesale { get; set; }
        public String Detail { get; set; }
        public int Numbers { get; set; }

        public string MetaDesc { get; set; }

        [Required(ErrorMessage = "Từ khóa SEO không được để rỗng")]
        public string MetaKey { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }
        public int Sale { get;set; }
    }
}
