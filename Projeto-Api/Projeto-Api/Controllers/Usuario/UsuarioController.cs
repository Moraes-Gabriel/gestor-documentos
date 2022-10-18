using Avaliacao_loja_interativa_c.Models;
using Google.Apis.Auth;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Projeto_Api.data.Interfaces;
using Projeto_Api.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Projeto_Api.Controllers.Usuario
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class UsuarioController : ControllerBase
    {

        private ApplicationDbContext context;
        private IUserRepo userRepo;

        public UsuarioController(ApplicationDbContext _context,
            IUserRepo _userRepo)
        {
            context = _context;
            userRepo = _userRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IResult> Action([FromBody] UsuarioRequest request )
        {
            var user = new User { Nome = request.Nome,Email = request.Email, Role = "funcionario" };

                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.senha));
                }

            context.Users.Add(user);
            context.SaveChanges();
            return Results.Created($"/usuario/{user.Id}", user.Id);
        }

        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioLoginRequest model)
        {
            var user = userRepo.Get(model.Email);

            if (user == null)
            {
                return BadRequest("Username Or Password Was Invalid");
            }

            var match = CheckPassword(model.senha, user);

            if (!match)
            {
                return BadRequest("Username Or Password Was Invalid");
            }
            //JWTGenerator(user);

            var token = TokenService.GenerateToken(user);

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost("LoginWithGoogle")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "42523927962-qh268urhb8n1hv3amivo69a32ofd0hkj.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

            var user = userRepo.Get(payload.Email);


            var token = TokenService.GenerateToken(user);
            if (user != null)
            {
                return Ok(new
                {
                    user = user,
                    token = token
                });
            }else
            {
                return BadRequest();
            }
        }

        private bool CheckPassword(string password, User user)
        {
            bool result;

            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }

            return result;
        }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IResult> Action(int? page, int? rows)
    {
        var Users = context.Users.ToList();
        var usuarios = Users.Select(u => new UsuarioResponse(u.Id, u.Email, u.Nome, u.Role));
        return Results.Ok(usuarios);
    }

}
}
