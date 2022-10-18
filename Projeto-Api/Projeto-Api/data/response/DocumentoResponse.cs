using Avaliacao_loja_interativa_c.Models;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Tipos;

namespace Projeto_Api.data.response
{
    public class DocumentoResponse
    {
        public int Id{ get; set; }
        public string Descricao { get; set; }
        public string Abreviacao { get; set; }
        public string UrlArquivoS3 { get; set; }
        public Concessao Concessao { get; set; }
        public Tipo Tipo { get; set; }
        public User Usuario { get; set; }
        public DateTime EditedOn { get; set; }
        public string data { get; set; }
        public DocumentoResponse()
        {
        }

    }
    
}
