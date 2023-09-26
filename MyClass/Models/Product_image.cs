using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Product_images")]
    public class Product_image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Product_id { get; set; }
        public string Image { get; set; }
    }
}