using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
