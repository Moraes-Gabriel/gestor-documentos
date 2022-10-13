using AutoMapper;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.page;

namespace Avaliacao_loja_interativa_c.Profiles
{
    public class DocumentoProfile : Profile
    {

        public DocumentoProfile()
        {
            CreateMap<Documento, DocumentoResponse>().ReverseMap();
            CreateMap<DocumentoResponse, Documento>().ReverseMap();
            CreateMap<DocumentoRequest, Documento>();
            CreateMap<Documento, DocumentoRequest>();

        }
    }
}
