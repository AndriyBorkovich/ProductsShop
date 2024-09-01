using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProductsShop.Services.AuthAPI.Persistence.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(150)]
        public string Name { get; set; }
    }
}
