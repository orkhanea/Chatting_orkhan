using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatting.ViewModels
{
    public class VmRegister
    {
        [MaxLength(15)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Surname { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(30), Required]
        public string Password { get; set; }
    }
}
