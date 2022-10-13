using Avaliacao_loja_interativa_c.Models;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Projeto_Api.data.Interfaces;
using Projeto_Api.Migrations;
using Projeto_Api.Services;

namespace Avaliacao_loja_interativa_c.Repositorio
{
    public class UserRepo : IUserRepo
    {
        private ApplicationDbContext context;
        private UsuarioAutenticado usuarioAutenticado;

        public UserRepo(ApplicationDbContext _context, UsuarioAutenticado usuarioAutenticado)
        {
            context = _context;
            this.usuarioAutenticado = usuarioAutenticado;
        }

        public User buscarUsuarioAutenticado()
        {
            throw new NotImplementedException();
        }

        public User Get(string email, string senha)
        {
            return context.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.Senha == senha).FirstOrDefault();
        }
     
    }
}
