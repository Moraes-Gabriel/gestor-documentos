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
            try
            {

                var client = new SendGridClient("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");
                var from = new EmailAddress("gabipaiz@gmail.com", "Gabriel De Moraes Paiz");
                var subject = "Documento criado com asdsuecsso";
                var to = new EmailAddress("cfninja2013@gmail.com", "Gabriel Paiz");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                return await client.SendEmailAsync(msg);

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
