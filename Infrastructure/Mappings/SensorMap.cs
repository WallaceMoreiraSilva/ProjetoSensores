using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Mappings
{
    public class SensorMap : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {           

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Numero)
                .HasColumnName("Numero")
                .IsRequired();

            builder.Property(c => c.DataCadastro)
                .HasColumnName("DataCadastro")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnName("DataAlteracao")
                .IsRequired();

            builder.Property(c => c.RegiaoId)
                .HasColumnName("RegiaoId")
                .IsRequired();

            builder.Property(c => c.PaisId)
               .HasColumnName("PaisId")
               .IsRequired();

            builder.Property(c => c.StatusSensorId)
               .HasColumnName("StatusSensorId")
               .IsRequired();
        }
    }

}

