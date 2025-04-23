using FiorellaApp.Data;
using FiorellaApp.Models;
using FiorellaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiorellaApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly FiorelloDbContext _fiorelloDbContext;
        public BasketController(FiorelloDbContext fiorelloDbContext)
        {
            _fiorelloDbContext = fiorelloDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddBasket(int?id)
        {
            if (id == null) return BadRequest();
            var existProduct = _fiorelloDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return BadRequest();
            string basket = Request.Cookies["basket"];
            List<BasketVM> list;
            if(basket is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            var existProductBasket = list.FirstOrDefault(p => p.Id == existProduct.Id);
            if (existProductBasket == null)
            {
                list.Add(new BasketVM() { Id = existProduct.Id, BasketCount = 1 });
            }
            else
            {
                existProductBasket.BasketCount++;
            }

               
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(list));
            TempData["BasketCount"] = list.Sum(x => x.BasketCount);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> list;
            if (basket is null)
            {
                list = new(); 
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var basketProduct in list)
                {
                    var existProduct=_fiorelloDbContext.Products
                        .Include(p=>p.ProductImages)
                        .FirstOrDefault(p=>p.Id==basketProduct.Id);
                    basketProduct.Name = existProduct.Name;
                    basketProduct.Image = existProduct.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl;
                }

            }
            return View(list);
        }
        public IActionResult Remove()
        {
            return View();
        }
     
    }
}
