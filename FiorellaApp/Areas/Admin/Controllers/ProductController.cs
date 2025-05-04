using FiorellaApp.Areas.Admin.ViewModels.Product;
using FiorellaApp.Data;
using FiorellaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly FiorelloDbContext _context;

        public ProductController(FiorelloDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p=>p.ProductImages)
                .Include(p=>p.Category)
                .AsNoTracking()
                .ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
        {
            var files = productCreateVM.Photos;

            if (files == null || files.Length == 0)
            {
                ModelState.AddModelError("Photos", "Şəkil boş ola bilməz");
                return View(productCreateVM);
            }

            Product newProduct = new Product
            {
                CategoryId = productCreateVM.CategoryId,
                Name = productCreateVM.Name,
                Price = productCreateVM.Price,
                Count = productCreateVM.Count,
                CreatedDate = DateTime.Now,
                ProductImages = new List<ProductImage>()
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            bool isFirst = true;
            foreach (var file in files)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets","img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                ProductImage productImage = new ProductImage
                {
                    ImageUrl = fileName,
                    ProductId = newProduct.Id,
                    IsMain = isFirst 
                };

                isFirst = false; 

                await _context.ProductImages.AddAsync(productImage);
            }

            await _context.SaveChangesAsync(); 

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();

            foreach (var image in product.ProductImages)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", image.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            // Fetch product along with its category and images
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;

            var productUpdateVM = new ProductUpdateVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = (int)product.Price,
                Count = product.Count,
                CategoryId = product.CategoryId,
                ProductImages = product.ProductImages,
            };

            return View(productUpdateVM);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id, ProductUpdateVM productUpdateVM)
        {
            if (id != productUpdateVM.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products
                .Include(p => p.ProductImages) 
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                ViewBag.Categories = categories;
                return View(productUpdateVM); 
            }

            if (productUpdateVM.Photos != null && productUpdateVM.Photos.Count > 0)
            {
                foreach (var image in product.ProductImages)
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", image.ImageUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                foreach (var file in productUpdateVM.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var productImage = new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = fileName,
                        IsMain = true 
                    };

                    product.ProductImages.Add(productImage); 
                }
            }

            product.Name = productUpdateVM.Name;
            product.Price = productUpdateVM.Price;
            product.Count = productUpdateVM.Count;
            product.CategoryId = productUpdateVM.CategoryId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            foreach (var image in product.ProductImages)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", image.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath); 
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(); 

            return RedirectToAction(nameof(Index)); 
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)  
                .Include(p => p.ProductImages) 
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }
    }
}
