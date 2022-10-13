namespace Projeto_Api.data.request
{
    public record DocumentoPutRequest(int id, string Descricao, int ConcessaoId, int TipoId);
}
