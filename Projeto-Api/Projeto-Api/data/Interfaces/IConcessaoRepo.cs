using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.page;

namespace Projeto_Api.data.Interfaces
{
    public interface IConcessaoRepo
    {
        ConcessaoResponse Post(ConcessaoRequest request);
        Task<PageList<ConcessaoResponse>> GetAllPaginadoAsync(PageParams pageParams);
        Task<List<ConcessaoResponse>> GetAllAsync();
        Task<ConcessaoResponse> Put(ConcessaoRequest request, int id);
        void Delete(int id);
        Concessao GetEntityById(int concessaoId);
        Task<ConcessaoResponse> GetById(int id);
    }
}