using FiorellaApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace FiorellaApp.ViewComponents
{
    public class SettingFooterViewComponent : ViewComponent
    {
        private readonly FiorelloDbContext _context;

        public SettingFooterViewComponent(FiorelloDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = _context.Settings.ToDictionary(key=>key.Key,val=>val.Value);
            return View(await Task.FromResult(settings));
        }
    }
}
