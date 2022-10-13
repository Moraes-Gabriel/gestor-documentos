using AutoMapper;
using Avaliacao_loja_interativa_c.Models;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Api.data.Interfaces;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.page;
using Projeto_Api.Domain.Tipos;
using Projeto_Api.Services;

namespace Projeto_Api.Repositorio
{
    public class TipoRepositorio : ITipoRepositorio
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UsuarioAutenticado usuarioAutenticado;
        private readonly PaginarIQueryableService paginarService;
        public TipoRepositorio(ApplicationDbContext context, IMapper _mapper, UsuarioAutenticado usuarioAutenticado, PaginarIQueryableService paginarService)
        {
            this.context = context;
            this.mapper = _mapper;
            this.usuarioAutenticado = usuarioAutenticado;
            this.paginarService = paginarService;
        }


        public void Delete(int id)
        {
            var tipo = GetEntityById(id);

            DateTime localDate = DateTime.Now;
            tipo.DeletedOn = localDate;
            context.SaveChanges();
        }

        public async Task<List<TipoResponse>> GetAllAsync()
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            List<Tipo> list = context.Tipos.Where(t =>
           t.DeletedOn < diaParaVerificarDelecao
            ).ToList();

            return mapper.Map<List<TipoResponse>>(list);
        }

        public async Task<TipoResponse> GetById(int id)
        {
            return mapper.Map<TipoResponse>(GetEntityById(id));
        }

        public Tipo GetEntityById(int tipoId)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            return context.Tipos.Where(t => t.Id == tipoId &&
            t.DeletedOn < diaParaVerificarDelecao
            ).FirstOrDefault();
        }

        public Tipo Post(TipoRequest request)
        {
            Tipo tipo = new Tipo(request.Nome, request.Sigla);
            context.Tipos.Add(tipo);
            context.SaveChanges();
            return tipo;
        }
        public async Task<TipoResponse> Put(TipoRequest request, int id)
        {
            var entity = GetEntityById(id);
            entity.Sigla = request.Sigla;
            entity.Nome = request.Nome;
            context.SaveChanges();
            return new TipoResponse(entity.Id, entity.Nome, entity.Sigla);
        }

        public async Task<PageList<TipoResponse>> GetAllPaginationAsync(PageParams pageParams)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            IQueryable<Tipo> query = context
                .Tipos
                .Where(d => d.DeletedOn < diaParaVerificarDelecao);

            query = query.AsNoTracking()
                    .Where(d => d.Nome.ToLower().Contains(pageParams.Descricao.ToLower()));

            return await paginarService.PaginarIQueryable(query, pageParams);
        }
    }
}