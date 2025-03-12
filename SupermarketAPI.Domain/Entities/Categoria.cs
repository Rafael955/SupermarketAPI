namespace SupermarketAPI.Domain.Entities
{
    public class Categoria 
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }

        #region Relacionamento

        // Relacionamento 1 para muitos: Uma categoria possui nenhum ou muitos produtos
        public List<Produto> Produtos { get; set; }

        #endregion
    }
}
