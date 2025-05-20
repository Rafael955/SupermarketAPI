using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public DateTime DataCadastro { get; set; }

        #region Relacionamento

        public Guid CategoriaId { get; set; }
        
        public virtual Categoria? Categoria { get; set; }
        
        #endregion
    }
}
