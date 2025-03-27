using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SupermarketAPI.Domain.DTOs.Categories;
using SupermarketAPI.Domain.DTOs.Products;

namespace SupermarketAPI.Tests.IntegrationTests
{
    public class ProductsTest
    {
        [Fact]
        public void CreateProduct_Successfully()
        {
            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //pegando uma categoria qualquer para teste
            var response = client.GetAsync("/api/categorias/listar-categorias").Result;

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            var categorias = JsonConvert.DeserializeObject<List<CategoriaResponseDto>>(content);

            //pegando uma categoria qualquer para o teste
            var categoriaEscolhida = categorias[new Random().Next(0, categorias.Count - 1)];

            //criando os dados do teste de produto
            var request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(100, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 300))
                .RuleFor(p => p.CategoriaId, categoriaEscolhida.Id)
                .Generate();

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/criar-produto", jsonRequest).Result;

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic objeto = JsonConvert.DeserializeObject<dynamic>(content);
            ProdutoResponseDto produto = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.productData.ToString());

            //verificando o conteudo dos dados do usuário
            produto?.Id.Should().NotBeNull();
            produto?.Nome.Should().Be(request.Nome);
            produto?.Quantidade.Should().NotBeNull();
            produto?.Quantidade.Should().Be(request.Quantidade);
            produto?.Preco.Should().NotBeNull();
            produto?.Preco.Should().BeGreaterThan(0);
            produto?.Preco.Should().Be(request.Preco);
            produto?.CategoriaId.Should().Be(request.CategoriaId);
            produto?.NomeCategoria.Should().Be(categoriaEscolhida.Nome);
        }

        [Fact]
        public void CreateProduct_PriceBelowZero()
        {
            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //pegando uma categoria qualquer para teste
            var response = client.GetAsync("/api/categorias/listar-categorias").Result;

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            var categorias = JsonConvert.DeserializeObject<List<CategoriaResponseDto>>(content);

            //pegando uma categoria qualquer para o teste
            var categoriaEscolhida = categorias[new Random().Next(0, categorias.Count - 1)];

            //criando os dados do teste de produto
            var request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Preco, -100)
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 300))
                .RuleFor(p => p.CategoriaId, categoriaEscolhida.Id)
                .Generate();

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/criar-produto", jsonRequest).Result;

            //verificando se a API retornou código 400 (HTTP BAD REQUEST)
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic objeto = JsonConvert.DeserializeObject<dynamic>(content);
            string errorMessage = objeto.message.ToString();

            errorMessage.Should().Be("Preço de um produto não poderá ter um valor negativo.");
        }

        [Fact]
        public void UpdateProduct_Successfully()
        {
            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //pegando uma categoria qualquer para teste
            var response = client.GetAsync("/api/categorias/listar-categorias").Result;

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            var categorias = JsonConvert.DeserializeObject<List<CategoriaResponseDto>>(content);

            //pegando uma categoria qualquer para o teste
            var categoriaEscolhida = categorias[new Random().Next(0, categorias.Count - 1)];

            //criando os dados do teste de produto
            var request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(100, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 300))
                .RuleFor(p => p.CategoriaId, categoriaEscolhida.Id)
                .Generate();

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/criar-produto", jsonRequest).Result;

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic objeto = JsonConvert.DeserializeObject<dynamic>(content);
            ProdutoResponseDto produto = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.productData.ToString());

            //atualizando o produto cadastrado
            request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, produto.Nome)
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(100, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 300))
                .RuleFor(p => p.CategoriaId, categoriaEscolhida.Id)
                .Generate();

            //serializar os dados da requisição em JSON
            jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de alteração de produto
            response = client.PutAsync($"/api/produtos/alterar-produto/{produto.Id}", jsonRequest).Result;

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);
            produto = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.productData.ToString());

            //verificando o conteudo dos dados do usuário
            produto?.Id.Should().NotBeNull();
            produto?.Nome.Should().Be(request.Nome);
            produto?.Quantidade.Should().NotBeNull();
            produto?.Quantidade.Should().Be(request.Quantidade);
            produto?.Preco.Should().NotBeNull();
            produto?.Preco.Should().BeGreaterThan(0);
            produto?.Preco.Should().Be(request.Preco);
            produto?.CategoriaId.Should().Be(request.CategoriaId);
            produto?.NomeCategoria.Should().Be(categoriaEscolhida.Nome);
        }
    }
}
