using IWantApp.Infra.Data;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.page;
using Projeto_Api.Domain.Tipos;

namespace Projeto_Api.data.Interfaces
{
    public interface ITipoRepositorio
    {

        Tipo Post(TipoRequest request);
        Task<PageList<TipoResponse>> GetAllPaginationAsync(PageParams pageParams);
        Task<List<TipoResponse>> GetAllAsync();
        Task<TipoResponse> Put(TipoRequest request, int id);
        void Delete(int id);
        Tipo GetEntityById(int tipoId);
        Task<TipoResponse> GetById(int id);
    }
}