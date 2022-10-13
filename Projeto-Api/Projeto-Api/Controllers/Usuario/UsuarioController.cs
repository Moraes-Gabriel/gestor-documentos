using Avaliacao_loja_interativa_c.Models;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Projeto_Api.data.Interfaces;
using Projeto_Api.Services;
using System.Security.Claims;

namespace Projeto_Api.Controllers.Usuario
{
    [ApiController]
    [Route("[controller]")]
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
            var user = new User { Nome = request.Nome,Email = request.Email, Senha = request.senha, Role = "funcionario" };
            context.Users.Add(user);
            context.SaveChanges();
            return Results.Created($"/usuario/{user.Id}", user.Id);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IResult> Action(int? page, int? rows)
        {
            var Users = context.Users.ToList();
            var usuarios = Users.Select(u => new UsuarioResponse(u.Id, u.Email, u.Nome, u.Role));
            return Results.Ok(usuarios);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [EnableCors]

        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioLoginRequest model)
        {
            var user = userRepo.Get(model.Email, model.senha);
            if (user == null)   
                return NotFound (new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
