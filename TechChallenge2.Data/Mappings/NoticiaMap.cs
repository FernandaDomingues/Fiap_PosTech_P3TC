using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;

namespace TechChallenge2.Data.Mappings
{
    public class NoticiaMap : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.ToTable("Noticia");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("INT");

            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Titulo")
                .HasColumnType("VARCHAR(255)");

            builder.Property(x => x.Conteudo)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Conteudo")
                .HasColumnType("VARCHAR(MAX)");

            builder.Property(x => x.DataPublicacao)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("DataPublicacao")
                .HasColumnType("DATETIME");

            builder.Property(x => x.Autor)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Autor")
                .HasColumnType("VARCHAR(255)");
        }
    }
}
