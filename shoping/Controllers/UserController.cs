using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using shoping.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using shoping.Models;

namespace shoping.Controllers
{
    public class UserController : Controller
    {
        private readonly SellerProjectContext _sql;
        public UserController(SellerProjectContext sql)
        {
            _sql = sql;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User u)
        {
            var user = _sql.Users.SingleOrDefault(x => x.UsersName == u.UsersName && x.UsersPassword == u.UsersPassword);
            if (user != null)
            {
                List<Claim> claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UsersName),
                    new Claim(ClaimTypes.Sid,user.UsersId.ToString()),
                };

                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var prins = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, prins, props).Wait();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User m)
        {
            _sql.Users.Add(m);
            _sql.SaveChanges();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Loglist()
        {
            ViewBag.say = _sql.Baskets.Count();
            ViewBag.Category = _sql.Categories.ToList();
            return View(_sql.Users.ToList());
        }

        public IActionResult LoglistRemove(int id)
        {
            var a = _sql.Products.Where(x => x.ProductUserId == id);
            var b = _sql.Users.SingleOrDefault(x => x.UsersId == id);
            _sql.Products.RemoveRange(a);
            _sql.Users.Remove(b);
            _sql.SaveChanges();
            return RedirectToAction("Loglist", "User");
        }

        public IActionResult Products(int id)
        {
            ViewBag.say = _sql.Baskets.Count();
            ViewBag.Category = _sql.Categories.ToList();
            return View(_sql.Products.Where(x => x.ProductUserId == id).ToList());
        }


    }
}
