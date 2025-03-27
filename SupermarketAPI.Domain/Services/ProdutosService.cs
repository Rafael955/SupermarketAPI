using FluentValidation;
using SupermarketAPI.Domain.DTOs.Products;
using SupermarketAPI.Domain.Entities;
using SupermarketAPI.Domain.Interfaces.Repositories;
using SupermarketAPI.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly IProdutosRepository _produtoRepository;

        public ProdutosService(IProdutosRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public ProdutoResponseDto CriarProduto(ProdutoRequestDto request)
        {
            #region Regra de Negócio - Não pode cadastrar produtos com o mesmo nome.

            var product = _produtoRepository.GetByName(request.Nome);

            if (product != null)
                throw new ApplicationException("Já existe um produto cadastrado com este nome!");

            #endregion

            #region Regra de Negócio - O preço do produto não pode ser negativo.

            if (request.Preco < 0)
                throw new ApplicationException("Preço de um produto não poderá ter um valor negativo.");

            #endregion

            #region Regra de Negócio - Ao cadastrar ou editar um produto, é obrigatório informar uma categoria

            if (request.CategoriaId == null)
                throw new ApplicationException("Categoria do Produto é obrigatória.");

            #endregion

            #region Capturar os dados recebidos

            product = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Preco = request.Preco,
                Quantidade = request.Quantidade,
                CategoriaId = request.CategoriaId.Value,
                DataCadastro = DateTime.Now
            };

            #endregion

            #region Validar dados do Produto

            var productValidator = new ProdutoValidator();
            var results = productValidator.Validate(product);

            if (!results.IsValid)
                throw new ValidationException(results.Errors);

            #endregion


            #region Cadastrar o Produto

            _produtoRepository.Add(product);
            
            product = _produtoRepository.GetById(product.Id);

            #endregion

            #region Retornar dados do Produto cadastrado

            return RetornarResponseProduto(product);

            #endregion
        }

        public ProdutoResponseDto AlterarProduto(Guid id, ProdutoRequestDto request)
        {
            #region Regra de Negócio - Não pode cadastrar produtos com o mesmo nome.

            var product = _produtoRepository.GetByName(request.Nome);

            if (product != null && product.Id != id)
                throw new ApplicationException("Já existe um produto cadastrado com este nome!");

            #endregion

            #region Regra de Negócio - O preço do produto não pode ser negativo.

            if (request.Preco < 0)
                throw new ApplicationException("Preço de um produto não poderá ter um valor negativo.");

            #endregion

            #region Regra de Negócio - Ao cadastrar ou editar um produto, é obrigatório informar uma categoria

            if (request.CategoriaId == null)
                throw new ApplicationException("Categoria do Produto é obrigatória.");

            #endregion

            #region Capturar os dados recebidos
            
            product = _produtoRepository.GetById(id);

            product.Nome = request.Nome;
            product.Preco = request.Preco;
            product.Quantidade = request.Quantidade;
            product.CategoriaId = (Guid)request.CategoriaId;

            #endregion

            #region Validar dados do Produto

            var productValidator = new ProdutoValidator();
            var results = productValidator.Validate(product);

            if (!results.IsValid)
                throw new ValidationException(results.Errors);

            #endregion

            #region Alterar o Produto

            _produtoRepository.Update(product);
            
            product = _produtoRepository.GetById(id);

            #endregion

            #region Retornar dados do produto alterado

            return RetornarResponseProduto(product);

            #endregion
        }

        public ProdutoResponseDto ExcluirProduto(Guid id)
        {
            #region Buscar o produto pelo ID no Banco de Dados

            var product = _produtoRepository.GetById(id);

            if (product == null)
                throw new ApplicationException("Produto não foi encontrado, verifique o ID informado.");

            #endregion

            #region Regra de Negócio - O produto não poderá ser excluído se houver quantidade em estoque

            if(product.Quantidade > 0)
                throw new ApplicationException("O produto não poderá ser excluído, pois ainda há quantidade em estoque.");

            #endregion

            #region Excluir o Produto

            _produtoRepository.Delete(product.Id);

            #endregion

            #region Retornar dados do produto excluido

            return RetornarResponseProduto(product);

            #endregion
        }

        public ProdutoResponseDto ObterProdutoPorId(Guid id)
        {
            #region Buscar o produto pelo ID no Banco de Dados

            var product = _produtoRepository.GetById(id);

            if (product == null)
                throw new ApplicationException("Produto não foi encontrado, verifique o ID informado.");

            #endregion

            #region Retornar dados do produto pesquisado

            return RetornarResponseProduto(product);

            #endregion
        }

        public List<ProdutoResponseDto> ObterProdutos()
        {
            List<ProdutoResponseDto> listaProdutosDto = new List<ProdutoResponseDto>();

            #region Obter lista de Produtos

            var listaProdutos = _produtoRepository.GetAll();

            foreach (var produto in listaProdutos)
            {
                listaProdutosDto.Add(new ProdutoResponseDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    CategoriaId = produto.CategoriaId,
                    NomeCategoria = produto.Categoria.Nome,
                    DataCadastro = produto.DataCadastro,
                    Preco = produto.Preco,
                    Quantidade = produto.Quantidade
                });
            }

            #endregion

            #region Retornar lista com dados dos produtos cadastrados

            return listaProdutosDto;

            #endregion
        }

        private ProdutoResponseDto RetornarResponseProduto(Produto product)
        {
            return new ProdutoResponseDto
            {
                Id = product.Id,
                Nome = product.Nome,
                CategoriaId = product.CategoriaId,
                Preco = product.Preco,
                Quantidade = product.Quantidade,
                DataCadastro = product.DataCadastro
            };
        }
    }
}
