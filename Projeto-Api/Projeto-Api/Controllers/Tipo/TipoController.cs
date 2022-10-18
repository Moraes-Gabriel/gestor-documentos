using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_Api.data.Interfaces;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.page;
using Projeto_Api.Services;

namespace Projeto_Api.Controllers.Tipo
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    [AllowAnonymous]    
    public class TipoController : ControllerBase
    {
        private ITipoRepositorio repositorio;

        public TipoController(ITipoRepositorio _repositorio)
        {
            this.repositorio = _repositorio;
        }

        [HttpPost]
        [EnableCors]
        [Authorize]
        public async Task<IResult> adicionarTipo(TipoRequest request)
        {
            var response = repositorio.Post(request);

            if (response == null) NoContent();


            return Results.Created($"/Tipo/{response.Id}", response.Id);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await repositorio.GetAllAsync();
                if (result == null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }

        }
        [HttpGet("paginado")]
        public async Task<IActionResult> GetAllFiltado([FromQuery] PageParams pageParams)
        {
            try
            {
                var result = await repositorio.GetAllPaginationAsync(pageParams);
                if (result == null) return NoContent();

                Response.AddPagination(result.CurrentPage,
                    result.PageSize, result.TotalCount, result.TotalPages);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> modificarTipo(TipoRequest request, int id)
        {
            try
            {
                var response = await repositorio.Put(request, id);
                if (response == null) return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var response = repositorio.GetById(id);
                if (response == null) NoContent();
                return Ok(response);
            }catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
           
        }

        [HttpDelete("{id}")]
        [EnableCors]
        [AllowAnonymous]
        public async Task<IActionResult> deleteTipoById(int id)
        {
            try
            {
                repositorio.Delete(id);
                return Ok("sucesso");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }

    }
}
