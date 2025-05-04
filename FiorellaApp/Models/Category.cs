using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Models
{
    public class Category:BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<Product> Products { get; set; }
    }
}
