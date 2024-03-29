﻿// <auto-generated />
using System;
using ApiConsorcio.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiConsorcio.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiConsorcio.Models.Export", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateExport")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ExportedBy")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("LeadId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LeadId");

                    b.ToTable("Exports", (string)null);
                });

            modelBuilder.Entity("ApiConsorcio.Models.Lead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Campaign")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateLead")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("Exported")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<long>("Telephone")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("ApiConsorcio.Models.Export", b =>
                {
                    b.HasOne("ApiConsorcio.Models.Lead", "Lead")
                        .WithMany("Exports")
                        .HasForeignKey("LeadId");

                    b.Navigation("Lead");
                });

            modelBuilder.Entity("ApiConsorcio.Models.Lead", b =>
                {
                    b.Navigation("Exports");
                });
#pragma warning restore 612, 618
        }
    }
}
