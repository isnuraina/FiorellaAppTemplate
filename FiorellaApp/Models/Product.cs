using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Models
{
    public class Product:BaseEntity
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
