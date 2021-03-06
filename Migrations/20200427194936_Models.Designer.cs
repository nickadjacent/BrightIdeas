﻿// <auto-generated />
using System;
using BrightIdeas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BrightIdeas.Migrations
{
    [DbContext(typeof(BrightIdeasContext))]
    [Migration("20200427194936_Models")]
    partial class Models
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BrightIdeas.Models.BrightIdea", b =>
                {
                    b.Property<int>("BrightIdeaId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Idea")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("BrightIdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("BrightIdeas");
                });

            modelBuilder.Entity("BrightIdeas.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrightIdeaId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsUpLike");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("LikeId");

                    b.HasIndex("BrightIdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("BrightIdeas.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BrightIdeas.Models.BrightIdea", b =>
                {
                    b.HasOne("BrightIdeas.Models.User", "SubmittedBy")
                        .WithMany("Idea")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BrightIdeas.Models.Like", b =>
                {
                    b.HasOne("BrightIdeas.Models.BrightIdea", "Idea")
                        .WithMany("Likes")
                        .HasForeignKey("BrightIdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BrightIdeas.Models.User", "Liker")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
