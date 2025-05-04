using System.ComponentModel.DataAnnotations;
namespace FiorellaApp.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }

    }
}