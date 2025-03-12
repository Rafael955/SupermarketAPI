using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketAPI.Domain.DTOs.Products;
using SupermarketAPI.Domain.Interfaces.Services;
using SupermarketAPI.Domain.Services;
using System.Threading.Tasks;

namespace SupermarketAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosService _produtoServices;

        public ProdutosController(IProdutosService produtoServices)
        {
            _produtoServices = produtoServices;
        }

        [HttpPost("criar-produto")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] ProdutoRequestDto request)
        {
            try
            {
                var response = _produtoServices.CriarProduto(request);

                return Created(string.Empty, response);
            }
            catch(ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("alterar-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Put(Guid id, [FromBody] ProdutoRequestDto request)
        {
            return Ok();
        }

        [HttpDelete("excluir-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }

        [HttpGet("listar-produtos")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("obter-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }
    }
}
