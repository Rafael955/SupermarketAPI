using SupermarketAPI.Domain.DTOs.Categories;
using SupermarketAPI.Domain.DTOs.Products;
using SupermarketAPI.Domain.Interfaces.Repositories;
using SupermarketAPI.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Services
{
    public class CategoriasService : ICategoriasService
    {
        private readonly ICategoriasRepository _categoriaRepository;

        public CategoriasService(ICategoriasRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public CategoriaResponseDto ObterCategoriaPorId(Guid id)
        {
            #region Buscar o categorias pelo ID no Banco de Dados

            var category = _categoriaRepository.GetById(id);

            if (category == null)
                throw new ApplicationException("Categoria não foi encontrada, verifique o ID informado.");

            #endregion

            #region Retornar dados da categoria pesquisada

            return new CategoriaResponseDto
            {
                Id = category.Id,
                Nome = category.Nome,
                DataCadastro = category.DataCadastro
            };

            #endregion
        }

        public List<CategoriaResponseDto> ListarCategorias()
        {
            List<CategoriaResponseDto> listaCategoriasDto = new List<CategoriaResponseDto>();

            #region Obter lista de Categorias

            var listaCategorias = _categoriaRepository.GetAll();

            foreach (var categoria in listaCategorias)
            {
                listaCategoriasDto.Add(new CategoriaResponseDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    DataCadastro = categoria.DataCadastro
                });
            }

            #endregion

            #region Retornar lista com dados das categorias cadastradas

            return listaCategoriasDto;

            #endregion
        }
    }
}
