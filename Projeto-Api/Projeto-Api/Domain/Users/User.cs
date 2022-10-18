using Microsoft.EntityFrameworkCore;
using Projeto_Api.Domain.Documento;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Avaliacao_loja_interativa_c.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public ICollection<Documento> Documentos{ get; set; }

    }
}
