using FiorellaApp.Data;
using FiorellaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiorellaApp.ViewComponents
{
    public class SettingHeaderViewComponent : ViewComponent
    {
        private readonly FiorelloDbContext _context;

        public SettingHeaderViewComponent(FiorelloDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //string basket = Request.Cookies["Basket"];
            //List<BasketVM> list;
            //if (basket!=null)
            //{
            //    list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            //    ViewBag.BasketCount = list.Count;
            //}
            var settings = _context.Settings.ToDictionary(key=>key.Key,val=>val.Value);
            return View(await Task.FromResult(settings));
        }
    }
}
