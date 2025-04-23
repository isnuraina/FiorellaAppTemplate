using FiorellaApp.Data;
using FiorellaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FiorelloDbContext _fiorelloDbContext;
        public HomeController(FiorelloDbContext fiorelloDbContext)
        {
            _fiorelloDbContext = fiorelloDbContext;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeVM()
            {
                Sliders = _fiorelloDbContext.Sliders
                .AsNoTracking()
                .ToList(),
                SliderContent = _fiorelloDbContext.SliderContents.SingleOrDefault(),
                Categories = _fiorelloDbContext.Categories
                .AsNoTracking()
                .ToList(),
                Products=_fiorelloDbContext.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .ToList(),   
            };
            return View(homeVM);
        }
    }
}
