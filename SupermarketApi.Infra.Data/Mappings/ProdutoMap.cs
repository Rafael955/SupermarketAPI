using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarketAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketAPI.Infra.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTOS");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasColumnName("PRECO")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.Quantidade)
                .HasColumnName("QUANTIDADE")
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .HasColumnName("DATACADASTRO")
                .IsRequired();

            builder.Property(p => p.CategoriaId)
                .HasColumnName("CATEGORIA_ID")
                .IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
