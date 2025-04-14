using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Models
{
    public class SliderContent:BaseEntity
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Desc { get; set; }
        public string SignImageUrl { get; set; }
    }
}
