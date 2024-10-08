﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaTecnica.Data;

#nullable disable

namespace PruebaTecnica.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240912000010_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaTecnica.Entities.ComprobanteFiscal", b =>
                {
                    b.Property<string>("RncCedula")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NCF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Itbis")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RncCedula", "NCF");

                    b.ToTable("ComprobantesFiscales");
                });

            modelBuilder.Entity("PruebaTecnica.Entities.Contribuyente", b =>
                {
                    b.Property<string>("RncCedula")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RncCedula");

                    b.ToTable("Contribuyentes");
                });
#pragma warning restore 612, 618
        }
    }
}
