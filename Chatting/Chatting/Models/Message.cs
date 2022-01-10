using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chatting.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1500)]
        public string Context { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public CustomUser Sender { get; set; }

        [ForeignKey("Reciever")]
        public string RecieverId { get; set; }
        public CustomUser Reciever { get; set; }
    }
}
