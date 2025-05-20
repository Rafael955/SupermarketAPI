using SupermarketAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Interfaces.Repositories
{
    public interface IProdutosRepository
    {
        void Add(Produto produto);

        void Update(Produto produto);

        void Delete(Produto produto);

        List<Produto>? GetAll();

        Produto? GetById(Guid id);

        Produto? GetByName(string productName);
    }
}
