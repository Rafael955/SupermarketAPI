using SupermarketAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.DTOs.Categories
{
    public class CategoriaResponseDto
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
