using Avaliacao_loja_interativa_c.Models;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Projeto_Api.Services
{
    public class UsuarioAutenticado 
    {

        private readonly IHttpContextAccessor _accessor;
        private readonly ApplicationDbContext context;

        public UsuarioAutenticado(IHttpContextAccessor accessor, ApplicationDbContext context)
        {
            _accessor = accessor;
            this.context = context;
        }

        public User BuscarUsuarioAutenticado()
        {
            var emailUsuarioAutenticado = _accessor.HttpContext.User.Identity.Name;

            User usuarioAutenticado = context.Users.Where(u => u.Email == emailUsuarioAutenticado)
                .FirstOrDefault();

            if (usuarioAutenticado == null)
                return null;
            return usuarioAutenticado;
        }
    }
}
