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

namespace Projeto_Api.Controllers.DocumentoController
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    [AllowAnonymous]
    public class DocumentoController : ControllerBase
    {
        private IDocumentoRepo repositorio;
        private readonly IConfiguration configuration;

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
        [AllowAnonymous]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, int id)
        {
            var result = repositorio.AdicionarArquivodoNoDocumento(file, id);
            return Ok(new
            {
                path = result
            });

        }

        [HttpPut("{id}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> BuscarDocById(int id)
        {
            try
            {
                var query = await repositorio.GetById(id);
                if (query == null) return NotFound("usuario ou documento nao encontrado");

                return Ok(query);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }

        [HttpGet("buscar/usuario")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarTodosDocumentosDoUsuarioAutenticado([FromQuery] PageParams pageParams)
        {
            try
            {
                var documentos = await repositorio.GetAllDocDoUsuarioAsync(pageParams);
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

        /*public async Task<IActionResult> get()
        {
            var result = repositorio.getArquiveS3();
            return Ok(result);
        }*/

        [HttpGet("file/{id}")]
        [AllowAnonymous]
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
        [EnableCors]
        [AllowAnonymous]
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
