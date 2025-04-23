using System.ComponentModel.DataAnnotations.Schema;

namespace FiorellaApp.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BasketCount { get; set; }
        public string Image { get; set; }
    }
}
