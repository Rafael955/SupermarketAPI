using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.DTOs.Products
{
    public class ProdutoResponseDto
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public decimal? Preco { get; set; }

        public int? Quantidade { get; set; }

        public decimal? Total => Preco * Quantidade;

        public Guid? CategoriaId { get; set; }

        public string? NomeCategoria { get; set; }

        public DateTime? DataCadastro { get; set; }
    }
}
