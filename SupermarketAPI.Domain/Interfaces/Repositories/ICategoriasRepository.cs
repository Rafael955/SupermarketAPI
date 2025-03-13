using SupermarketAPI.Domain.Entities;

namespace SupermarketAPI.Domain.Interfaces.Repositories
{
    public interface ICategoriasRepository
    {
        List<Categoria> GetAll();

        Categoria GetById(Guid id);
    }
}
