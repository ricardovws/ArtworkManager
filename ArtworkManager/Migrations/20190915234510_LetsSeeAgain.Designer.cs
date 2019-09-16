﻿// <auto-generated />
using System;
using ArtworkManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArtworkManager.Migrations
{
    [DbContext(typeof(ArtworkManagerContext))]
    [Migration("20190915234510_LetsSeeAgain")]
    partial class LetsSeeAgain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArtworkManager.Models.Artwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Code");

                    b.Property<int>("OwnerID");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Artwork");
                });

            modelBuilder.Entity("ArtworkManager.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<int?>("TeamId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("ArtworkManager.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ArtworkManager.Models.Artwork", b =>
                {
                    b.HasOne("ArtworkManager.Models.Author", "Owner")
                        .WithMany("Artworks")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ArtworkManager.Models.Author", b =>
                {
                    b.HasOne("ArtworkManager.Models.Team", "Team")
                        .WithMany("Authors")
                        .HasForeignKey("TeamId");
                });
#pragma warning restore 612, 618
        }
    }
}
