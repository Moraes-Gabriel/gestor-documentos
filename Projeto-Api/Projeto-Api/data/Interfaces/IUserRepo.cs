using Avaliacao_loja_interativa_c.Models;
using Projeto_Api.Domain.Documento;

namespace Projeto_Api.data.Interfaces
{
    public interface IUserRepo
    {
        User Get(string email);
        User buscarUsuarioAutenticado();
    }
}
