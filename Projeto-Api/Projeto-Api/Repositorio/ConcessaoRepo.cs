using AutoMapper;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Projeto_Api.data.Interfaces;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.page;
using Projeto_Api.Services;

namespace Projeto_Api.Repositorio
{
    public class ConcessaoRepo : IConcessaoRepo
    {

        private ApplicationDbContext context;
        private IMapper mapper;
        private PaginarIQueryableService paginarService;
        public ConcessaoRepo(ApplicationDbContext context, IMapper _mapper , PaginarIQueryableService paginarService)
        {
            this.context = context;
            this.mapper = _mapper;
            this.paginarService = paginarService;
        }

        [AllowAnonymous]
        public ConcessaoResponse Post(ConcessaoRequest request)
        {
            var concessao = new Concessao(request.Nome, request.Sigla);
            context.Concessoes.Add(concessao);
            context.SaveChanges();
            return mapper.Map<ConcessaoResponse>(concessao);
        }


        public async Task<List<ConcessaoResponse>> GetAllAsync()
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);
            
            List<Concessao> list = context.Concessoes.Where(c =>
            c.DeletedOn < diaParaVerificarDelecao
             ).ToList();

            return mapper.Map<List<ConcessaoResponse>>(list);
        }

        public Concessao GetEntityById(int concessaoId)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);
            var concessao =  context.Concessoes.Where(c => c.Id == concessaoId &&
            c.DeletedOn < diaParaVerificarDelecao
            ).FirstOrDefault();

            if (concessao == null) return null;

            return concessao;
        }

        public async Task<ConcessaoResponse> Put(ConcessaoRequest request, int id)
        {
            Concessao entity = GetEntityById(id);

            entity.Nome = request.Nome;
            entity.Sigla = request.Sigla;

            entity.EditedOn = DateTime.Now;

            context.Concessoes.Update(entity);

            await context.SaveChangesAsync();
            return new ConcessaoResponse(entity.Id, entity.Nome, entity.Sigla);
        }

        public async Task<ConcessaoResponse> GetById(int id)
        {
            return mapper.Map<ConcessaoResponse>(GetEntityById(id));
        }

        public async Task<PageList<ConcessaoResponse>> GetAllPaginadoAsync(PageParams pageParams)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            IQueryable<Concessao> query = context
                .Concessoes
                .Where(d => d.DeletedOn < diaParaVerificarDelecao);

            query = query.AsNoTracking()
                    .Where(d => d.Nome.ToLower().Contains(pageParams.Descricao.ToLower()));

            return await paginarService.PaginarIQueryable(query, pageParams);
        }
        public void Delete(int id)
        {
            var concessao = GetEntityById(id);

            DateTime localDate = DateTime.Now;
            concessao.DeletedOn = localDate;
            context.SaveChanges();
        }

    }
}
