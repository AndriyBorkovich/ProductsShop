using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.AuthAPI.Persistence.Models;

namespace ProductsShop.Services.AuthAPI.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
