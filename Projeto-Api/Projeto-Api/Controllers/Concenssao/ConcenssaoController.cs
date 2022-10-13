using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Projeto_Api.data.Interfaces;
using Projeto_Api.data.request;
using Projeto_Api.data.response;
using Projeto_Api.Domain.page;
using Projeto_Api.Repositorio;
using Projeto_Api.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Projeto_Api.Controllers.Concenssao
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    [AllowAnonymous]
    public class ConcenssaoController : ControllerBase
    {
        private readonly IConcessaoRepo repositorio;

        public ConcenssaoController(IConcessaoRepo Repositorio)
        {
            repositorio = Repositorio;
        }

        [HttpPost]
        public async Task<IResult> adicionarConcessao(ConcessaoRequest request)
        {
            var response =  repositorio.Post(request);
            return Results.Created($"/Concenssao/{response.Id}", response.Id);
        }

        [HttpGet]
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
        public async Task<IActionResult> GetAllPaginado([FromQuery] PageParams pageParams)
        {
            try
            {
                var result = await repositorio.GetAllPaginadoAsync(pageParams);
                if (result == null) return NoContent();

                Response.AddPagination(result.CurrentPage,
                    result.PageSize, result.TotalCount, result.TotalPages);

                return Ok(result);
            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> modificarTipo(ConcessaoRequest request, int id)
        {
            try
            {
                var response = await repositorio.Put(request,id);
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
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [EnableCors]
        [AllowAnonymous]
        public async Task<IActionResult> deleteConcessaoById(int id)
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
