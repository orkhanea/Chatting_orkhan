using Chatting.Data;
using Chatting.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatting.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            string currentUserId = _userManager.GetUserId(User);
            return View(_context.CustomUsers.Where(u=>u.Id!=currentUserId).ToList());
        }

        public IActionResult Message(string Rid)
        {
            ViewBag.Page = "Message";
            if (Rid!=null)
            {
                if (_context.CustomUsers.Find(Rid)!=null)
                {
                    string Sid =_userManager.GetUserId(User);
                    VmMessage model = new();
                    model.user = _context.CustomUsers.Find(Rid);
                    _httpContextAccessor.HttpContext.Session.SetString("RecId", model.user.Id);
                    model.senderId = Sid;
                    model.Messages = _context.Messages.Include(m=>m.Sender).Where(m => m.SenderId == Sid && m.RecieverId == Rid || m.SenderId == Rid && m.RecieverId == Sid).ToList();
                    return View(model);

                    
                    
                }
            }
            return RedirectToAction("Index");
        }
    }
}
