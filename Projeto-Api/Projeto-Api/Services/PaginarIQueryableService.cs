using AutoMapper;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.page;
using Projeto_Api.Domain.Tipos;

namespace Projeto_Api.Services
{
    public class PaginarIQueryableService
    {
        private readonly IMapper mapper;

        public PaginarIQueryableService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<PageList<DocumentoResponse>> 
            PaginarIQueryable(IQueryable<Documento> query, PageParams pageParams)
        {
            var responseEntity = await PageList<Documento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
            var response = mapper.Map<PageList<DocumentoResponse>>(responseEntity.ToList());

            response.CurrentPage = responseEntity.CurrentPage;
            response.PageSize = responseEntity.PageSize;
            response.TotalCount = responseEntity.TotalCount;
            response.TotalPages = responseEntity.TotalPages;

            return response;
        }
        
        public async Task<PageList<ConcessaoResponse>> 
            PaginarIQueryable(IQueryable<Concessao> query, PageParams pageParams)
        {
            var responseEntity = await PageList<Concessao>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
            var response = mapper.Map<PageList<ConcessaoResponse>>(responseEntity.ToList());

            response.CurrentPage = responseEntity.CurrentPage;
            response.PageSize = responseEntity.PageSize;
            response.TotalCount = responseEntity.TotalCount;
            response.TotalPages = responseEntity.TotalPages;

            return response;
        } 
        public async Task<PageList<TipoResponse>> 
            PaginarIQueryable(IQueryable<Tipo> query, PageParams pageParams)
        {
            var responseEntity = await PageList<Tipo>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
            var response = mapper.Map<PageList<TipoResponse>>(responseEntity.ToList());

            response.CurrentPage = responseEntity.CurrentPage;
            response.PageSize = responseEntity.PageSize;
            response.TotalCount = responseEntity.TotalCount;
            response.TotalPages = responseEntity.TotalPages;

            return response;
        }
    }
}
