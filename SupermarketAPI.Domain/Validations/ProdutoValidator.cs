using System.Data;
using FluentValidation;
using SupermarketAPI.Domain.Entities;

public class ProdutoValidator : AbstractValidator<Produto>
{
     /// <summary>
    /// Classe de validação do FluentValidator para a entidade Produto
    /// </summary>
    public ProdutoValidator()
    {
        RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("O ID do produto é obrigatório.");
        
        RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(120)
                    .WithMessage("O nome do produto deve ter no máximo 120 caracteres.")
                .MinimumLength(3)
                    .WithMessage("O nome do produto deve ter no mínimo 3 caracteres.");
        
        RuleFor(p => p.Preco)
            .NotEmpty()
                .WithMessage("O preço do produto é obrigatório.")
            .PrecisionScale(10, 2, true)
                .WithMessage("O preço do produto deve ter no máximo 10 digitos e 2 casas decimais.");

        RuleFor(p => p.Quantidade)
            .NotNull()
                .WithMessage("A quantidade do produto é obrigatória.");
        
        RuleFor(p => p.CategoriaId)
            .NotEmpty()
                .WithMessage("A categoria do produto é obrigatória.");
    }
}