using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Product_values")]
    public class Product_value
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Product_id { get; set; }
        public string Product_name { get; set; }
        public string Product_description { get; set; }
    }
}