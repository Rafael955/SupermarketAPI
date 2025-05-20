# 🛒 SupermarketAPI - Desafio Técnico

SupermarketAPI é uma API REST desenvolvida em **.NET 9.0** com **C#**, projetada para realizar operações de **CRUD de produtos** em um ambiente de supermercado. O projeto segue os princípios da arquitetura em camadas e possui integração com ferramentas modernas de documentação, validação e testes.

## 🚀 Tecnologias Utilizadas

- **.NET 9.0**
- **C#**
- **Entity Framework Core** v9.0.3
- **SQL Server** (banco de dados relacional)
- **FluentValidation** (validação de entidades)
- **Swagger** e **Scalar** (documentação da API)
- **xUnit** (testes de integração)

## 🧱 Arquitetura do Projeto

O projeto foi desenvolvido utilizando uma **arquitetura em camadas**, promovendo melhor organização, manutenção e escalabilidade do código.

### 🔹 Camadas

- **Aplicação:** Contém os casos de uso e orquestrações entre domínio e infraestrutura.
- **Domínio:** Contém as entidades, interfaces e regras de negócio.
- **Infraestrutura:** Responsável pela persistência de dados e implementação das interfaces do domínio.
- **Testes de Integração:** Projeto separado que utiliza **xUnit** para testar os cenários de CRUD da API de Produtos.

## 📚 Documentação

A API conta com documentação interativa acessível via:

- **Swagger UI**
- **Scalar**

Essas ferramentas facilitam a visualização e o teste dos endpoints diretamente no navegador.

## ✅ Funcionalidades

- [x] Criar um produto  
- [x] Listar todos os produtos  
- [x] Consultar produto por ID  
- [x] Atualizar produto  
- [x] Excluir produto  
- [x] Validação robusta com FluentValidation  
- [x] Testes de integração com xUnit  

## 🔧 Como Executar o Projeto

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/SupermarketAPI.git
   cd SupermarketAPI
   ```

2. **Configure a connection string no appsettings.json:**
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=SEU_SERVIDOR;Database=SupermarketDb;Trusted_Connection=True;"
   }
   ```

3. **Execute as migrations e atualize o banco:**
   ```bash
   dotnet ef database update
   ```

4. **Inicie a aplicação:**
   ```bash
   dotnet run
   ```

5. **Acesse o Swagger:**
   ```
   https://localhost:{porta}/swagger
   ```

## 🧪 Testes

Para executar os testes de integração:

```bash
cd SupermarketAPI.Tests
dotnet test
```

## 📁 Estrutura de Pastas (Simplificada)

```
SupermarketAPI/
├── Application/
├── Domain/
├── Infrastructure/
├── SupermarketAPI/ (API principal)
├── SupermarketAPI.Tests/ (Testes de Integração)
```

## 📝 Licença

Este projeto está licenciado sob a **MIT License**.

## 👨‍💻 Autor

Desenvolvido por **Rafael Ferreira Carvalho Caffonso**  
