using Chatting.Data;
using Chatting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatting.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task SendPrivateMessage(string recieverId, string message)
        {
         
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var cUser = _context.CustomUsers.Find(currentUser.Id);
            await Clients.User(recieverId).SendAsync("ReceiveMessage", cUser.Name, cUser.Id, message);

            

            Message m = new();
            m.Context = message;
            m.CreatedDate = DateTime.Now;
            m.RecieverId = recieverId;
            m.SenderId = currentUser.Id;

            _context.Messages.Add(m);
            _context.SaveChanges();

        }

        public async Task isTyping(string recieverId)
        {

            await Clients.User(recieverId).SendAsync("IsTyping");

        }

        public async Task isNotTyping(string recieverId)
        {

            await Clients.User(recieverId).SendAsync("IsNotTyping");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var RecId = _httpContextAccessor.HttpContext.Session.GetString("RecId");
            if (RecId!=null)
            {
                await Clients.User(RecId).SendAsync("IsNotTyping");
            }
           

        }

    }
}
