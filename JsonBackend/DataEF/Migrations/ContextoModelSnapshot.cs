﻿// <auto-generated />
using System;
using DataEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataEF.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dominio.Modelos.Catalogos.MarcasAutos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FIngreso")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MarcasAutos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Toyota",
                            FIngreso = new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6287)
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Nissan",
                            FIngreso = new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6693)
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Hyundai",
                            FIngreso = new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6695)
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "Suzuki",
                            FIngreso = new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6696)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
