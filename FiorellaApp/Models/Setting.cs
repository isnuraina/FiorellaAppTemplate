using System.ComponentModel.DataAnnotations;

namespace FiorellaApp.Models
{
    public class Setting:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
