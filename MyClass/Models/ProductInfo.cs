using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }

        public string Slug { get; set; }
        public int CatId { get; set; }
        public int? BrandId { get; set; }
        public string Name { get; set; }
        public string CatName { get; set; }
        public string Image { get; set; }
        public decimal Pricebuy { get; set; }
        public decimal Pricesale { get; set; }
        public String Detail { get; set; }
        public int Numbers { get; set; }

        public string MetaDesc { get; set; }

        public string MetaKey { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }
    }
}
