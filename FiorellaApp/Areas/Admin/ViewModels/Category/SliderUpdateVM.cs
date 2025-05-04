using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Areas.Admin.ViewModels.Category
{
    public class SliderUpdateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        public string? ImageURL { get; set; }
    }
}
