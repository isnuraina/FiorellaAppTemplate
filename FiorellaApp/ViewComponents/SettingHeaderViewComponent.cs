using FiorellaApp.Data;
using FiorellaApp.Models;
using FiorellaApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiorellaApp.ViewComponents
{
    public class SettingHeaderViewComponent : ViewComponent
    {
        private readonly FiorelloDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SettingHeaderViewComponent(FiorelloDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User); // DÜZGÜN yol
                if (user != null)
                {
                    ViewBag.FullName = user.FullName;
                }
            }

            var settings = _context.Settings.ToDictionary(key => key.Key, val => val.Value);
            return View(await Task.FromResult(settings));
        }

    }
}
