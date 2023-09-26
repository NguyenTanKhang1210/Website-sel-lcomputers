using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Sliders")]

    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không để rỗng")]
        public string Name { get; set; }
        public string Link { get; set; }
        [Required(ErrorMessage = "Không để rỗng")]
        public int? Orders { get; set; }
        public string Position { get; set; }
        public string Img { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }
    }
}
