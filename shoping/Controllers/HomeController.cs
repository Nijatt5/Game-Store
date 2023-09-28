using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using shoping.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace shoping.Controllers
{
    public class HomeController : Controller
    {
        private readonly SellerProjectContext _sql;
        public HomeController(SellerProjectContext sql)
        {
            _sql = sql;
        }

        public IActionResult Index()
        {
            ViewBag.say = _sql.Baskets.Count();
            ViewBag.Category = _sql.Categories.ToList();
            return View(_sql.Products.Where(x => x.ProductConfirm == "Tesdiqlendi").ToList());
        }

        public IActionResult Add()
        {
            ViewBag.Category = new SelectList(_sql.Categories.ToList(),"CategoryId","CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product p, IFormFile Sekil)
        {
            if (Sekil != null)
            {
                string random = Path.GetRandomFileName() + Path.GetExtension(Sekil.FileName);
                p.ProductImg = random;
                using (FileStream s = new FileStream("wwwroot/img/" + random, FileMode.Create))
                {
                    Sekil.CopyTo(s);
                }
                p.ProductConfirm = "Tesdiqlenmedi";
                p.ProductUserId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                _sql.Products.Add(p);
                _sql.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Readmore(int id)
        {
            return View(_sql.Products.SingleOrDefault(x => x.ProductId == id));
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Category = new SelectList(_sql.Categories.ToList(), "CategoryId", "CategoryName");
            return View(_sql.Products.SingleOrDefault(x => x.ProductId == id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Product p, IFormFile Sekil)
        {
            var a = _sql.Products.SingleOrDefault(x => x.ProductId == id);
            if (Sekil != null)
            {
                using Stream s = new FileStream("wwwroot/img/" + a.ProductImg, FileMode.Create);
                Sekil.CopyTo(s);
            }
            a.ProductName = p.ProductName;
            a.ProductSalary = p.ProductSalary;
            a.ProductDescription = p.ProductDescription;
            _sql.SaveChanges();
            return View();
        }

        public IActionResult Remove(int id)
        {
            var a = _sql.Products.SingleOrDefault(x => x.ProductId == id);
            _sql.Remove(a);
            _sql.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Unconfirmed()
        {
            ViewBag.say = _sql.Baskets.Count();
            ViewBag.Category = _sql.Categories.ToList();
            return View(_sql.Products.Where(x => x.ProductConfirm == "Tesdiqlenmedi").ToList());
        }
        public IActionResult Confirm(int id)
        {
            _sql.Products.SingleOrDefault(x => x.ProductId == id).ProductConfirm = "Tesdiqlendi";
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Basket()
        {
            return View(_sql.Baskets.Include(x=>x.BasketProduct).ToList());
        }
        [HttpPost]

        public IActionResult Basket(int id)
        {
            Basket check = _sql.Baskets.SingleOrDefault(x => x.BasketProductId == id && x.BasketUserId == 1);
            if (check == null)
            {
                Basket b = new Basket();
                b.BasketProductId = id;
                b.BasketUserId = 1;
                _sql.Baskets.Add(b);
            }
            else
            {
                check.BasketCount++;
            }
            _sql.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult PlusBasket(int id)
        {
            Basket b = _sql.Baskets.SingleOrDefault(x => x.BasketId == id);
            b.BasketCount++;
            _sql.SaveChanges();
            return RedirectToAction("Basket", "Home");
        }
        

        public IActionResult MinusBasket(int id)
        {
            Basket b = _sql.Baskets.SingleOrDefault(x => x.BasketId == id);
            if (b.BasketCount == 1)
            {
                _sql.Baskets.Remove(b);
            }
            else
            {
                b.BasketCount--;
            }
            _sql.SaveChanges();
            return RedirectToAction("Basket", "Home");
        }

        public IActionResult Delete(int id)
        {
            var a = _sql.Baskets.SingleOrDefault(x => x.BasketId == id);
            _sql.Remove(a);
            _sql.SaveChanges();
            return RedirectToAction("Basket", "Home");
        }


        public IActionResult Filter(int minsalary, int maxsalary, string name)
        {
            ViewBag.say = _sql.Baskets.Count();
            var a = _sql.Products.Where(x => x.ProductConfirm == "Tesdiqlendi");
            if (minsalary != 0)
            {
                a = a.Where(x => x.ProductSalary >= minsalary);
            }

            if (maxsalary != 0)
            {
                a = a.Where(x => x.ProductSalary <= maxsalary);
            }

            if (name != null)
            {
                a = a.Where(x => x.ProductName.Contains(name));
            }

            return View(a.ToList());
        }





















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
