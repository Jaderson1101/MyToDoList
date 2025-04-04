using Microsoft.AspNetCore.Mvc;
using MyToDoList.Models;

namespace MyToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // POST: api/Auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // Aqui você faria a validação, hash da senha, salvar no banco...
            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            // Aqui você faria a verificação do usuário e senha
            // Geraria token JWT, se for usar
            return Ok(new { token = "token_exemplo", message = "Login realizado com sucesso!" });
        }
    }
}
