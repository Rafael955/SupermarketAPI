using Azure.Core;
using FluentValidation;
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

                return StatusCode(StatusCodes.Status201Created, new
                {
                    message = $"O produto {response.Nome} foi cadastrado com sucesso!",
                    createdAt = DateTime.Now, //data e hora da criação
                    productData = response //dados do produto que foi incluido
                });
            }
            catch(ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(StatusCodes.Status400BadRequest, errors);
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
            try
            {
                var response = _produtoServices.AlterarProduto(id, request);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = $"Produto {response.Nome} foi atualizado com sucesso!",
                    modifiedAt = DateTime.Now, //data e hora da alteração
                    productData = response //dados do produto que foi alterado
                });
            }
            catch(ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(StatusCodes.Status400BadRequest, errors);
            }
            catch (ApplicationException ex)
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

        [HttpDelete("excluir-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var response = _produtoServices.ExcluirProduto(id);

                return StatusCode(StatusCodes.Status200OK, new 
                { 
                    message = $"O produto {response.Nome} foi excluido com sucesso!",
                    deletedAt = DateTime.Now, //data e hora da exclusão
                    productData = response //dados do produto que foi excluido
                });
            }
            catch (ApplicationException ex)
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

        [HttpGet("listar-produtos")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var response = _produtoServices.ObterProdutos();

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("obter-produto/{id}")]
        [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var response = _produtoServices.ObterProdutoPorId(id);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = ex.Message
                });
            }
        }
    }
}
