using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoDDD.Controllers
{
    public class HomeController : Controller
    { 
        [Authorize]
        public IActionResult SecretApi() => Ok("Secret API");


        [AllowAnonymous]
        public IActionResult Index() => Ok("Hello from index");


        [Authorize]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(x => new { Type = x.Type, Value = x.Value }));
        }

        public async Task<IActionResult> Authentication()
        {
            //Claim é uma informação do usuario
            ClaimsIdentity identity = new ClaimsIdentity("SeriesAuthCookie");

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,"1234"));
            identity.AddClaim(new Claim(ClaimTypes.Email, "wallaceinfofuturo@gmail.com"));
            identity.AddClaim(new Claim(ClaimTypes.Webpage, "https://github.com/WallaceMoreiraSilva"));

            //Responsavel por agregar varias identidades
            ClaimsPrincipal principal = new ClaimsPrincipal (new[] { identity });

            await HttpContext.SignInAsync(principal);

            return Redirect("/home/index");
        }




    }
}
