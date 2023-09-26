using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Product_options")]
    public class Product_option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        [Required(ErrorMessage = "Chọn size Không được để rỗng!")]
        public string Description { get; set; }
        public string Type { get; set; }
    }
}