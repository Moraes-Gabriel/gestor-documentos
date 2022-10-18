using IWantApp.Domain;

namespace Projeto_Api.Domain.Tipos
{
    public class Tipo : Entity
    {
     
        public string Nome { get; set; }
        public string Sigla {  get; set; }

        public Tipo(string nome, string sigla)
        {
            Nome = nome;
            Sigla = sigla;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;
        }
    }
}