
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using kodmeydan.Models;
using System.Security.Claims;

namespace kodmeydan.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Report> Reports { get; set; }

        internal async Task GetUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}