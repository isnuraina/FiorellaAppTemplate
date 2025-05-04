using FiorellaApp.Areas.Admin.ViewModels.Category;
using FiorellaApp.Areas.Admin.ViewModels.Slider;
using FiorellaApp.Data;
using FiorellaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace FiorellaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly FiorelloDbContext _context;

        public SliderController(FiorelloDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            var sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            return View(sliders);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM sliderCreateVM)
        {
            var file = sliderCreateVM.Photo;
            if (file==null)
            {
                ModelState.AddModelError("Photo", "bos ola bilmez");
                return View(sliderCreateVM);
            }
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", fileName);
            using FileStream fileStream = new(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            Slider slider = new();
            slider.ImageUrl = fileName;
            slider.CreatedDate = DateTime.Now;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id ==null)
            {
                return BadRequest();
            }
            var slider = _context.Sliders.FirstOrDefault(m => m.Id == id);
            if (slider==null)
            {
                return NotFound();
            }
            return View(slider);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(new SliderUpdateVM { ImageURL=slider.ImageUrl});
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM sliderUpdateVM)
        {
            if (id == null) return BadRequest();
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();

            var file = sliderUpdateVM.Photo;
            if (file == null)
            {
                ModelState.AddModelError("Photo", "Şəkil boş ola bilməz");
                sliderUpdateVM.ImageURL = slider.ImageUrl;
                return View(sliderUpdateVM);
            }

            string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", slider.ImageUrl);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            slider.ImageUrl = fileName;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
