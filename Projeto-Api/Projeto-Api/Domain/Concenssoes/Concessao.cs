using IWantApp.Domain;

namespace Projeto_Api.Domain.Concenssao
{
    public class Concessao : Entity
    {

        public string Nome { get; set; }
        public string Sigla { get; set; }

        public Concessao(string nome, string sigla)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

        }
      
    }
}
