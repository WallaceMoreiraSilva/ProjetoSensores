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
        [Authorize(Policy = "Admin")]
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
            ClaimsIdentity identity = new ClaimsIdentity("SeriesAuthCookie");

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,"1234"));
            identity.AddClaim(new Claim(ClaimTypes.Email, "wallaceinfofuturo@gmail.com"));
            identity.AddClaim(new Claim(ClaimTypes.Webpage, "https://github.com/WallaceMoreiraSilva"));

            //Temos a claimType.Role do tipo SecretRole que vai bater com a Policy e ae vai deixar passar na claim do SecretApi
            //Role (Perfil) é algo que define acesso a um ou um grupo de usuários em partes da aplicação. EX: um papel, uma função, um cargo ...
            //Claim (Afirmação) utilizamos pra realizar alguma interação antes de deixar o usuário prosseguir.
            identity.AddClaim(new Claim(ClaimTypes.Role, "SecretRole"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Student"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Teen"));

            //Responsavel por agregar varias identidades
            ClaimsPrincipal principal = new ClaimsPrincipal (new[] { identity });

            await HttpContext.SignInAsync(principal);

            return Redirect("/home/index");
        }

    }
}
