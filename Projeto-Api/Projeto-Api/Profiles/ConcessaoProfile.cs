using AutoMapper;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Documento;

namespace Avaliacao_loja_interativa_c.Profiles
{
    public class ConcessaoProfile : Profile
    {

        public ConcessaoProfile()
        {
            CreateMap<Concessao, ConcessaoResponse>();
            CreateMap<ConcessaoResponse, Concessao>();
            CreateMap<Concessao, Concessao>();
            CreateMap<Concessao, ConcessaoRequest>();

        }
    }
}
