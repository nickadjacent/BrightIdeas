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
    [Migration("20200427183902_LoginReg")]
    partial class LoginReg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
#pragma warning restore 612, 618
        }
    }
}
