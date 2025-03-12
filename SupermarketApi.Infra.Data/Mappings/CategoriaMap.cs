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
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIAS");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(c => c.DataCadastro)
              .HasColumnName("DATACADASTRO")
              .IsRequired();

            builder.HasIndex(c => c.Nome)
                .IsUnique();
        }
    }
}
