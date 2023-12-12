﻿// <auto-generated />
using System;
using CircleCI.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CircleCI.DataService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231003102358_initial migration")]
    partial class initialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Ukrainian_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CircleCI.Entities.DbSet.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryListId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryListId");

                    b.HasIndex("PostId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.CategoryList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoriesList");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Follow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FollowedUserId")
                        .HasColumnType("int");

                    b.Property<int>("FollowerUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FollowedUserId");

                    b.HasIndex("FollowerUserId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LikesAmount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ViewsAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Saved", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Saves");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("FollowersAmount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("TNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Category", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.CategoryList", "CategoryList")
                        .WithMany()
                        .HasForeignKey("CategoryListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CircleCI.Entities.DbSet.Post", "Post")
                        .WithMany("Category")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryList");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Comment", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.Post", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CircleCI.Entities.DbSet.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Follow", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.User", "FollowedUser")
                        .WithMany("Followers")
                        .HasForeignKey("FollowedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CircleCI.Entities.DbSet.User", "FollowerUser")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FollowedUser");

                    b.Navigation("FollowerUser");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Like", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.Post", "Post")
                        .WithMany("Like")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CircleCI.Entities.DbSet.User", "User")
                        .WithMany("Like")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Post", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Saved", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.Post", "Post")
                        .WithOne("Saves")
                        .HasForeignKey("CircleCI.Entities.DbSet.Saved", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CircleCI.Entities.DbSet.User", "User")
                        .WithMany("Saves")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Token", b =>
                {
                    b.HasOne("CircleCI.Entities.DbSet.User", "User")
                        .WithOne("Token")
                        .HasForeignKey("CircleCI.Entities.DbSet.Token", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.Post", b =>
                {
                    b.Navigation("Category");

                    b.Navigation("Comment");

                    b.Navigation("Like");

                    b.Navigation("Saves")
                        .IsRequired();
                });

            modelBuilder.Entity("CircleCI.Entities.DbSet.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Like");

                    b.Navigation("Posts");

                    b.Navigation("Saves");

                    b.Navigation("Token")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}