using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.DTOs.Products
{
    public class ProdutoRequestDto
    {
        public string? Nome { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public Guid? CategoriaId { get; set; }
    }
}
