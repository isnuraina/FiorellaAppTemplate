using FiorellaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Areas.Admin.ViewModels.Product
{
    public class ProductUpdateVM:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Count { get; set; }

        public List<IFormFile> Photos { get; set; } 

        [Required]
        public int CategoryId { get; set; }
        public List<string> ExistingImages { get; internal set; }
        public List<ProductImage> ProductImages { get; internal set; }
    }

}
