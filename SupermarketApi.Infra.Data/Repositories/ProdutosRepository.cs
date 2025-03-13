using Microsoft.EntityFrameworkCore;
using SupermarketAPI.Domain.Entities;
using SupermarketAPI.Domain.Interfaces.Repositories;
using SupermarketAPI.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Infra.Data.Repositories
{
    public class ProdutosRepository : IProdutosRepository
    {
        public void Add(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(produto);
                dataContext.SaveChanges();
            }
        }

        public void Update(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(produto);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Produto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Produto GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Produto>()
                    .Include(p => p.Categoria) //Similar a um JOIN no Banco de Dados
                    .SingleOrDefault(p => p.Id == id);
            }
        }

        public Produto GetByName(string productName)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Produto>()
                    .Include(p => p.Categoria) //Similar a um JOIN no Banco de Dados
                    .FirstOrDefault(p => p.Nome.Equals(productName));
            }
        }
    }
}
