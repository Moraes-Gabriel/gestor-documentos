using Avaliacao_loja_interativa_c.Models;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.page;
using Projeto_Api.Domain.Tipos;
using SendGrid;

namespace Projeto_Api.data.Interfaces
{
    public interface IDocumentoRepo
    {

        DocumentoResponse Post(DocumentoRequest request);
        Task<PageList<DocumentoResponse>> GetAllAsync(PageParams pageParams);
        Task<DocumentoResponse> Put(DocumentoPutRequest request, int id);
        Task<DocumentoResponse> GetById(int id);
        Task<DocumentoResponse> Delete(int id);
        Task<PageList<DocumentoResponse>> GetAllDocDoUsuarioAsync(PageParams pageParams);
        Documento GetByIdEntity(int id);
        Task<string> AdicionarArquivodoNoDocumento(IFormFile file, int id);
    }
}