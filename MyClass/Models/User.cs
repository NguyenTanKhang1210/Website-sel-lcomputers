using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Users")]

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không để rỗng")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Img { get; set; }
        public int? CountError { get; set; }
        public string Roles { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? Status { get; set; }
    }
}
