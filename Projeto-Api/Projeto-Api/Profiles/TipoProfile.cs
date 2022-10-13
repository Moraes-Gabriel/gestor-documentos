using AutoMapper;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.Tipos;

namespace Avaliacao_loja_interativa_c.Profiles
{
    public class TipoProfile : Profile
    {

        public TipoProfile()
        {
            CreateMap<Tipo, TipoResponse>();
            CreateMap<Tipo, TipoRequest>();
            CreateMap<TipoRequest, Tipo>();
            CreateMap<TipoResponse, Tipo>();

        }
    }
}
