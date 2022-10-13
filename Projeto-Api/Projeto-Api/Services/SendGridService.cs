using SendGrid.Helpers.Mail;
using SendGrid;
using Projeto_Api.Domain.Documento;
using Avaliacao_loja_interativa_c.Models;

namespace Projeto_Api.Services
{
    public class SendGridService
    {
        public async Task<Response> SendGrid(Documento documento)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("gabipaiz@gmail.com", "Gabriel Moraes");
            var subject = "Documento criado com suecsso";
            var to = new EmailAddress(documento.Usuario.Email, documento.Usuario.Nome);
            var plainTextContent = "documento" + documento.Abreviacao;
            var htmlContent = "<strong>Documento criado</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);    
        }
    }
}
