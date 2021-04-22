using Microsoft.AspNetCore.Identity;

namespace ProjetoDDD.Sensores.Infra.Data.Areas.Identity.Data
{    
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
