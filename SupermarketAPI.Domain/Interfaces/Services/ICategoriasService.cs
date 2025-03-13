using SupermarketAPI.Domain.DTOs.Categories;
using SupermarketAPI.Domain.DTOs.Products;

namespace SupermarketAPI.Domain.Interfaces.Services
{
    public interface ICategoriasService
    {
        CategoriaResponseDto ObterCategoriaPorId(Guid id);

        List<CategoriaResponseDto> ListarCategorias();
    }
}

