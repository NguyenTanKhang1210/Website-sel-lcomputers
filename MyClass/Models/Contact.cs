using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Tiêu đề không để rỗng!")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }

    }
}
