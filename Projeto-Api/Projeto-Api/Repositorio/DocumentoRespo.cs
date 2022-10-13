using Amazon.Runtime.Internal;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Avaliacao_loja_interativa_c.Models;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Api.Controllers.Usuario;
using Projeto_Api.data.Interfaces;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.page;
using Projeto_Api.Domain.Tipos;
using Projeto_Api.Services;
using SendGrid;

namespace Projeto_Api.Repositorio
{
    public class DocumentoRespo : IDocumentoRepo
    {
        private ApplicationDbContext context;
        private ITipoRepositorio tipoRepositorio;
        private IConcessaoRepo concessaoRepositorio;
        private UsuarioAutenticado usuarioAutenticado;
        private IMapper mapper;
        private readonly IAws3Services _aws3Services;
        private readonly PaginarIQueryableService paginarService;
        private readonly SendGridService sendGrid;
        public DocumentoRespo(ApplicationDbContext context, IConcessaoRepo _concessaoRepo,
            ITipoRepositorio _tipoRepositorio, UsuarioAutenticado _usuarioAutenticado,
            IMapper mapper, IAws3Services aws3Services, PaginarIQueryableService paginarService, SendGridService sendGrid)
        {
            this.context = context;
            this.tipoRepositorio = _tipoRepositorio;
            this.concessaoRepositorio = _concessaoRepo;
            usuarioAutenticado = _usuarioAutenticado;
            this.mapper = mapper;
            _aws3Services = aws3Services;
            this.paginarService = paginarService;
            this.sendGrid = sendGrid;
        }

        public async Task<DocumentoResponse> Delete(int id)
        {
            User userAutenticado = usuarioAutenticado.BuscarUsuarioAutenticado();
            var documento = GetByIdEntity(id);

            if (userAutenticado != documento.Usuario) return null;


            DateTime localDate = DateTime.Now;
            documento.DeletedOn = localDate;
            context.SaveChanges();
            return mapper.Map<DocumentoResponse>(documento);
        }

        public List<DocumentoResponse> GetAll()
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            var responses = context.Documentos.Include(d => d.Concessao)
                .Include(d => d.Tipo)
                .Include(d => d.Usuario)
                .Where(d =>
                d.DeletedOn < diaParaVerificarDelecao
                ).ToList();


            return mapper.Map<List<DocumentoResponse>>(responses);
        }

        public Documento GetByIdEntity(int id)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            return context.Documentos.Include(d => d.Usuario).
                Include(d => d.Concessao).Include(d => d.Tipo).Where(d => d.Id == id &&
               d.DeletedOn < diaParaVerificarDelecao
                ).FirstOrDefault();
        }
        public Task<DocumentoResponse> GetById(int id)
        {
            var entity = GetByIdEntity(id);
            if (entity == null) return null;
            return Task.FromResult(mapper.Map<DocumentoResponse>(entity));
        }
        public DocumentoResponse Post(DocumentoRequest request)
        {
            var concessao = concessaoRepositorio.GetEntityById(request.ConcessaoId);
            var tipo = tipoRepositorio.GetEntityById(request.TipoId);
            User user = usuarioAutenticado.BuscarUsuarioAutenticado();
            string abreviacao = AdicionarAbreviacaoAoDocumento(tipo, concessao);

            Documento documento = new Documento(request.Descricao, abreviacao, tipo, concessao, user);
            documento.urlArquivoS3 = "";
            context.Documentos.Add(documento);
            context.SaveChanges();
            sendGrid.SendGrid(documento);
            return mapper.Map<DocumentoResponse>(documento);
        }

        public async Task<DocumentoResponse> Put(DocumentoPutRequest request, int id)
        {
            User usuarioAuth = usuarioAutenticado.BuscarUsuarioAutenticado();
            var documento = GetByIdEntity(id);
            if (usuarioAuth == documento.Usuario || usuarioAuth.Role == "admin")
            {
            var concessao = concessaoRepositorio.GetEntityById(request.ConcessaoId);
            var tipo = tipoRepositorio.GetEntityById(request.TipoId);

            documento.Concessao = concessao;
            documento.Tipo = tipo;
            documento.Descricao = request.Descricao;

            context.SaveChanges();
            return mapper.Map<DocumentoResponse>(documento);
            }else
            {
                return null;

            }
        }
        private string AdicionarAbreviacaoAoDocumento(Tipo tipo, Concessao concessao)
        {
            int numeroDeDocumentosComEssasSiglas = context.Documentos

                 .Where(d => d.Tipo.Sigla == tipo.Sigla && d.Concessao.Sigla == concessao.Sigla).ToList().Count();
            return $"{tipo.Sigla}.{concessao.Sigla}.{numeroDeDocumentosComEssasSiglas + 1}";

        }

        public async Task<string> AdicionarArquivodoNoDocumento(IFormFile file, int id)
        {
            User usuarioAuth = usuarioAutenticado.BuscarUsuarioAutenticado();

            var documento = this.GetByIdEntity(id);

            if (usuarioAuth != documento.Usuario) return null;

            var result = await _aws3Services.UploadFile
                (file, usuarioAutenticado.BuscarUsuarioAutenticado().Email);
            documento.urlArquivoS3 = result;
            context.SaveChanges();
            return result;
        }

        public async Task<PageList<DocumentoResponse>> GetAllAsync(PageParams pageParams)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);

            IQueryable<Documento> query = context
                .Documentos.Include(d => d.Concessao)
                .Include(d => d.Tipo)
                .Include(d => d.Usuario)
                .Where(d => d.DeletedOn < diaParaVerificarDelecao);

            query = query.AsNoTracking()
                    .Where(d => d.Descricao.ToLower().Contains(pageParams.Descricao.ToLower()));

            if (pageParams.TipoId > 0)
                query = query.AsNoTracking()
                    .Where(d => d.TipoId == pageParams.TipoId);

            if (pageParams.ConcessaoId > 0)
                query = query.AsNoTracking()
                    .Where(d => d.ConcessaoId == pageParams.ConcessaoId);
            
            return await paginarService.PaginarIQueryable(query, pageParams);
        }

        public async Task<PageList<DocumentoResponse>> GetAllDocDoUsuarioAsync(PageParams pageParams)
        {
            DateTime diaParaVerificarDelecao = new DateTime(1, 02, 02);
            int userId = usuarioAutenticado.BuscarUsuarioAutenticado().Id;

            IQueryable<Documento> query = context
                .Documentos.Include(d => d.Concessao)
                .Include(d => d.Tipo)
                .Include(d => d.Usuario)
                .Where(d => d.DeletedOn < diaParaVerificarDelecao && d.UserId == userId);


            if (pageParams.Descricao.Length > 0 && pageParams.Descricao != "")
                query = query.AsNoTracking()
                    .Where(d => d.Descricao.ToLower().Contains(pageParams.Descricao.ToLower()));


            if (pageParams.TipoId > 0)
                query = query.AsNoTracking()
                    .Where(d => d.TipoId == pageParams.TipoId);

            if (pageParams.ConcessaoId > 0)
                query = query.AsNoTracking()
                    .Where(d => d.ConcessaoId == pageParams.ConcessaoId);

            return await paginarService.PaginarIQueryable(query, pageParams);
        }
    }
}
