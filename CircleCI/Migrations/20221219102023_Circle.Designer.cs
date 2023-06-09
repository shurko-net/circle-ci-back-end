﻿// <auto-generated />
using System;
using CircleCI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CircleCI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221219102023_Circle")]
    partial class Circle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Ukrainian_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CircleCI.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategory"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("VARCHAR(45)");

                    b.HasKey("IdCategory");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CircleCI.Models.Comment", b =>
                {
                    b.Property<int>("IdComment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdComment"), 1L, 1);

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdPost")
                        .HasColumnType("int");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.HasKey("IdComment");

                    b.HasIndex("IdPost");

                    b.HasIndex("IdUser");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CircleCI.Models.Post", b =>
                {
                    b.Property<int>("IdPost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPost"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdCategory")
                        .HasColumnType("int");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdPost");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdUser");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CircleCI.Models.Subscription", b =>
                {
                    b.Property<int>("IdSubscription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSubscription"), 1L, 1);

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<int>("IdUserSub")
                        .HasColumnType("int");

                    b.HasKey("IdSubscription");

                    b.HasIndex("IdUser");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("CircleCI.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"), 1L, 1);

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("VARCHAR(45)");

                    b.Property<int>("Subscribed")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("TNumber")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("VARCHAR(45)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CircleCI.Models.Comment", b =>
                {
                    b.HasOne("CircleCI.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("IdPost");

                    b.HasOne("CircleCI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Models.Post", b =>
                {
                    b.HasOne("CircleCI.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("IdCategory");

                    b.HasOne("CircleCI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Models.Subscription", b =>
                {
                    b.HasOne("CircleCI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
