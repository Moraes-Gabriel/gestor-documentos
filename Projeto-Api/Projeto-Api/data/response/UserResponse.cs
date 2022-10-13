using Microsoft.EntityFrameworkCore;
using Projeto_Api.Domain.Documento;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Projeto_Api.data.response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
