using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Product_stores")]
    public class Product_store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Product_id { get; set; }
        public int Product_type { get; set; }
        public int Product_type_id { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public int Product_Price { get; set; }
        public int Product_Price_id { get; set; }
        public int Product_Price_type { get; set; }

    }
}