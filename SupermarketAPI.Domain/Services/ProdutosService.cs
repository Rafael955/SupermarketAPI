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

            #region Cadastrar o Produto

            _produtoRepository.Add(product);

            #endregion

            #region Retornar dados do Produto cadastrado
            
            product = _produtoRepository.GetById(product.Id);

            return new ProdutoResponseDto 
            { 
                Id = product.Id,
                Nome = product.Nome,
                CategoriaId  =product.CategoriaId,
                Preco = product.Preco,
                Quantidade = product.Quantidade,
                DataCadastro = product.DataCadastro,
                NomeCategoria = product.Categoria.Nome
            };

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
            product.CategoriaId = request.CategoriaId.Value;

            #endregion

            #region Alterar o Produto

            _produtoRepository.Update(product);

            #endregion

            #region Retornar dados do produto alterado

            return new ProdutoResponseDto
            {
                Id = product.Id,
                Nome = product.Nome,
                CategoriaId = product.CategoriaId,
                Preco = product.Preco,
                Quantidade = product.Quantidade,
                DataCadastro = product.DataCadastro,
                NomeCategoria = product.Categoria.Nome
            };

            #endregion
        }

        public ProdutoResponseDto ExcluirProduto(Guid id)
        {
            #region Buscar o produto pelo ID no Banco de Dados

            var product = _produtoRepository.GetById(id);

            if (product == null)
                throw new ApplicationException("Produto não foi encontrado, verifique o ID informado.");

            #endregion

            #region Excluir o Produto

            _produtoRepository.Delete(product.Id);

            #endregion

            #region Retornar dados do produto excluido

            return new ProdutoResponseDto
            {
                Id = product.Id,
                Nome = product.Nome,
                CategoriaId = product.CategoriaId,
                Preco = product.Preco,
                Quantidade = product.Quantidade,
                DataCadastro = product.DataCadastro,
                NomeCategoria = product.Categoria.Nome
            };

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

            return new ProdutoResponseDto
            {
                Id = product.Id,
                Nome = product.Nome,
                CategoriaId = product.CategoriaId,
                Preco = product.Preco,
                Quantidade = product.Quantidade,
                DataCadastro = product.DataCadastro,
                NomeCategoria = product.Categoria.Nome
            };

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
                    DataCadastro = produto.DataCadastro,
                    Preco = produto.Preco,
                    NomeCategoria = produto.Categoria.Nome,
                    Quantidade = produto.Quantidade
                });
            }

            #endregion
            
            #region Retornar lista com dados dos produtos cadastrados

            return listaProdutosDto;
            
            #endregion
        }
    }
}
