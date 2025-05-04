using Microsoft.AspNetCore.Identity;

namespace FiorellaApp.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
