using Microsoft.EntityFrameworkCore;
using SupermarketAPI.Domain.Entities;
using SupermarketAPI.Domain.Interfaces.Repositories;
using SupermarketAPI.Infra.Data.Contexts;

namespace SupermarketAPI.Infra.Data.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        public List<Categoria> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Categoria>()
                    .Include(p => p.Produtos) //Similar a um JOIN no Banco de Dados
                    .ToList();
            }
        }

        public Categoria GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Categoria>()
                    .Include(p => p.Produtos) //Similar a um JOIN no Banco de Dados
                    .SingleOrDefault(p => p.Id == id);
            }
        }
    }
}
