using Chatting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatting.ViewModels
{
    public class VmMessage
    {
        public CustomUser user { get; set; }
        public List<Message> Messages { get; set; }
        public string senderId { get; set; }
    }
}
