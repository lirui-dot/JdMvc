using JdMvc.Models;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JdMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(User user)
        {


            if (!ModelState.IsValid)
            {
                var dbuser = _context.Users.FirstOrDefault(m => m.LoginName.Equals(user.UserName) && m.PassWord.Equals(user.PassWord));

                var ps = _context.Personals.SingleOrDefault(m => m.UserId == dbuser.Id);
                Personal personal = new Personal();
                personal.UserId = dbuser.Id;
                personal.UserName = dbuser.LoginName;
                personal.LoginName = dbuser.LoginName;
                personal.Name = dbuser.Name;
                
                if (ps != null)
                {
                    _context.Update(ps);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Personals.Add(personal);
                    await _context.SaveChangesAsync();
                }
                if (dbuser != null)
                {
                    return RedirectToAction("Edit", "Personal", new { id = dbuser.Id });
                }
                else
                    return View();
            }


            return View(user);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {


            User user = new User();
            user.UserName = register.LoginName;
            user.LoginName = register.LoginName;
            user.Name = register.Name;
            user.PassWord = register.PassWord;
            user.CpassWord = register.CpassWord;

            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult NameIsExists(string LoginName)
        {

            var yhm = _context.Users.SingleOrDefault(m => m.LoginName == LoginName);
            if (yhm != null)
            {
                return Json("用户名重复");
            }

            return Json(true);
        }

    }
}
