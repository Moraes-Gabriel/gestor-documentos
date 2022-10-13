using Avaliacao_loja_interativa_c.Models;
using Flunt.Validations;
using IWantApp.Domain;
using Projeto_Api.Controllers.Usuario;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Tipos;
using System.Text.Json.Serialization;

namespace Projeto_Api.Domain.Documento
{
    public class Documento : Entity
    {
        public string Descricao { get; set; }
        public string Abreviacao { get; set; }
        public string urlArquivoS3 { get; set; }
        public Concessao Concessao { get; set; }
        public int ConcessaoId { get; set; }
        public Tipo Tipo { get; set; }
        public int TipoId { get; set; }

        [JsonIgnore]
        public User Usuario{ get; set; }
        public int UserId { get; set; }

        public Documento( string descricao, string abreviacao)
        {
            this.Descricao = descricao;
            this.Abreviacao = abreviacao;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();
        }

        public Documento(string descricao, string abreviacao, Tipo tipo, Concessao concessao, User usuario)
        {
            this.Descricao = descricao;
            this.Abreviacao = abreviacao;
            this.Tipo = tipo;
            this.Concessao = concessao;
            this.Usuario = usuario;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();
        }

        private void Validate()
        {
            var contract = new Contract<Documento>()
                .IsNotNullOrEmpty(Descricao, "Descricao")
                .IsNotNullOrEmpty(Abreviacao, "Abreviacao")
                .IsNotNull(UserId, "UserId")
                .IsNotNull(ConcessaoId, "ConcessaoId")
                .IsNotNull(TipoId, "TipoId");

            AddNotifications(contract);
        }
        /* private void Validate()
         {
             var contract = new Contract<Category>()
                 .IsNotNullOrEmpty(Name, "Name")
                 .IsGreaterOrEqualsThan(Name, 3, "Name")
                 .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                 .IsNotNullOrEmpty(EditedBy, "EditedBy");
             AddNotifications(contract);
         }*/
    }
}
