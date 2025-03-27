using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SupermarketAPI.Domain.Entities;

namespace SupermarketAPI.Domain.Validations
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                    .WithMessage("O ID da categoria é obrigatório.");
            
            RuleFor(c => c.Nome)
                .NotEmpty()
                    .WithMessage("O nome da categoria é obrigatório.")
                .MaximumLength(120)
                    .WithMessage("O nome da categoria deve ter no máximo 120 caracteres.")
                .MinimumLength(3)
                    .WithMessage("O nome da categoria deve ter no mínimo 3 caracteres.");
        }
    }
}