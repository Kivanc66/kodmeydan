using Microsoft.AspNetCore.Identity;

namespace kodmeydan.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string? FullName { get; set; }
    }
}