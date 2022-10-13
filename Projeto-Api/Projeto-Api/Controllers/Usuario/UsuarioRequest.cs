namespace Projeto_Api.Controllers.Usuario
{
    public record UsuarioRequest(string Email, string senha, string Nome);
    public record UsuarioLoginRequest(string Email, string senha);

}