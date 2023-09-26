using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Code { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string Note { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }


    }
}
