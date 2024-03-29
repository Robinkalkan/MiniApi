﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniApi.Data;

#nullable disable

namespace MiniApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240121110324_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MiniApi.Classes.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestId"), 1L, 1);

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("MiniApi.Classes.InterestLink", b =>
                {
                    b.Property<int>("InterestLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestLinkId"), 1L, 1);

                    b.Property<int>("InterestId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterestLinkId");

                    b.HasIndex("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("InterestLinks");
                });

            modelBuilder.Entity("MiniApi.Classes.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("MiniApi.Classes.Interest", b =>
                {
                    b.HasOne("MiniApi.Classes.Person", null)
                        .WithMany("Interests")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("MiniApi.Classes.InterestLink", b =>
                {
                    b.HasOne("MiniApi.Classes.Interest", "Interest")
                        .WithMany("InterestLinks")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniApi.Classes.Person", "Person")
                        .WithMany("InterestLinks")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interest");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MiniApi.Classes.Interest", b =>
                {
                    b.Navigation("InterestLinks");
                });

            modelBuilder.Entity("MiniApi.Classes.Person", b =>
                {
                    b.Navigation("InterestLinks");

                    b.Navigation("Interests");
                });
#pragma warning restore 612, 618
        }
    }
}
