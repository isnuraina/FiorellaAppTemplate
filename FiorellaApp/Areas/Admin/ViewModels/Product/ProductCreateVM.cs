using FiorellaApp.Models;

namespace FiorellaApp.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public IFormFile[] Photos { get; set; }
    }
}
