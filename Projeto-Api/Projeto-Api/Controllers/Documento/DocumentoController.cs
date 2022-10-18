using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Projeto_Api.data.Interfaces;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.page;
using Projeto_Api.Services;
using System.Globalization;
using Projeto_Api.Domain.Documento;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Projeto_Api.Controllers.DocumentoController
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    [AllowAnonymous]
    public class DocumentoController : ControllerBase
    {
        private IDocumentoRepo repositorio;

        public DocumentoController(IDocumentoRepo _repositorio)
        {
            repositorio = _repositorio;
        }

        [HttpPost]
        [Authorize]
        public async Task<IResult> AdicionarDocumento(DocumentoRequest request)
        {
            var documento = repositorio.Post(request);
            return Results.Created($"/documento/{documento.Id}", documento.Id);
        }

        [HttpPost("file/{id}")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, int id)
        {
            var result = repositorio.AdicionarArquivodoNoDocumento(file, id);
            return Ok(new
            {
                path = result
            });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> editarDocumento(DocumentoPutRequest request, int id)
        {
            try
            {
                var response =  repositorio.Put(request, id);

                if (response == null) return NotFound(response);

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao editar documento. Erro: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDoc([FromQuery] PageParams pageParams)
        {
            try
            {
                var documentos = await repositorio.GetAllAsync(pageParams);

                documentos.ForEach(d =>
                {
                    d.data = $"{d.EditedOn.Year}/{d.EditedOn.Month}/{d.EditedOn.Day}";
                });

                if (documentos == null) return NoContent();

                
                Response.AddPagination(documentos.CurrentPage,
                    documentos.PageSize, documentos.TotalCount, documentos.TotalPages);

                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar documentos. Erro: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarDocById(int id)
        {
            try
            {
                var documento = await repositorio.GetById(id);

                documento.data = $"{documento.EditedOn.Year}/{documento.EditedOn.Month}/{documento.EditedOn.Day}";
             

                if (documento == null) return NotFound("usuario ou documento nao encontrado");

                return Ok(documento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }
        [HttpPost("sendGrid")]
        [AllowAnonymous]
        public async Task<ObjectResult> sendGrid()
        {
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress("gabipaiz@gmail.com", "Gabriel moraes");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("ilopaiz@gmail.com", "Gabriel Paiz");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        } [HttpPost("sendGrid2")]
        [AllowAnonymous]
        public async Task<ObjectResult> sendGrid2()
        {
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress("gabipaiz@gmail.com", "Gabriel M oraes");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("cfninja2013@gmail.com", "Gabriel paiz");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpGet("buscar/usuario")]
        public async Task<IActionResult> BuscarTodosDocumentosDoUsuarioAutenticado([FromQuery] PageParams pageParams)
        {
            try
            {
                var documentos = await repositorio.GetAllDocDoUsuarioAsync(pageParams);

                documentos.ForEach(d =>
                {
                    d.data = $"{d.EditedOn.Year}/{d.EditedOn.Month}/{d.EditedOn.Day}";
                });
                if (documentos == null) return NoContent();

                Response.AddPagination(documentos.CurrentPage,
                    documentos.PageSize, documentos.TotalCount, documentos.TotalPages);

                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }

        [HttpGet("file/{id}")]
        public async Task<IActionResult> getFile(int id)
        {
            var documento = repositorio.GetByIdEntity(id);

            if (documento == null) return NotFound("documento nao encontrado");
            var fileName = documento.urlArquivoS3;

            var bucketName = "lojainterativadocumentos";
            var client = new AmazonS3Client("AKIAW475BF2JBHIW5SUB", "U8IkUNnf8lJefS0P8UjiAZEn0mRzYnKupom6GgYf", Amazon.RegionEndpoint.SAEast1);
            var request = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = fileName
            };
            using GetObjectResponse response = await client.GetObjectAsync(request);
            using Stream responseStream = response.ResponseStream;
            var stream = new MemoryStream();
            await responseStream.CopyToAsync(stream);
            stream.Position = 0;

            return new FileStreamResult(stream, response.Headers["Content-Type"])
            {
                FileDownloadName = fileName
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDocById(int id)
        {
            try
            {
                var documento = repositorio.Delete(id);

                if (documento == null) return NotFound("usuario ou documento nao encontrado");

                return Ok(documento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }
    }
}
