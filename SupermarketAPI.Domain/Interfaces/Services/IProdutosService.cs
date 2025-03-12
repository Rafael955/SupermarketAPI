﻿using SupermarketAPI.Domain.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Interfaces.Services
{
    public interface IProdutosService
    {
        ProdutoResponseDto CriarProduto(ProdutoRequestDto produto);
        
        ProdutoResponseDto AlterarProduto(ProdutoRequestDto produto);

        ProdutoResponseDto ExcluirProduto(Guid id);

        ProdutoResponseDto ObterProdutoPorId(Guid id);

        List<ProdutoResponseDto> ObterProdutos();

        List<ProdutoResponseDto> ObterProdutosPorCategoria();
    }
}
