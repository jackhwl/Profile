﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wenlin.IDP.DbContexts;

#nullable disable

namespace Wenlin.IDP.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    partial class IdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Wenlin.IDP.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SecurityCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("SecurityCodeExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Subject")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                            Active = true,
                            ConcurrencyStamp = "0294c20f-3150-4880-b44f-d114a3cceded",
                            Email = "david@hotmail.com",
                            Password = "password",
                            SecurityCodeExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Subject = "d860efca-22d9-47fd-8249-791ba61b07c7",
                            UserName = "David"
                        },
                        new
                        {
                            Id = new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                            Active = true,
                            ConcurrencyStamp = "b62afb0f-bddb-455c-8033-1d79d740d93d",
                            Email = "emma@hotmail.com",
                            Password = "password",
                            SecurityCodeExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Subject = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                            UserName = "Emma"
                        });
                });

            modelBuilder.Entity("Wenlin.IDP.Entities.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b0117b99-d761-45be-bce2-f805f487aab1"),
                            ConcurrencyStamp = "58ff3324-b0ac-4934-9ca4-f56443606710",
                            Type = "given_name",
                            UserId = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                            Value = "David"
                        },
                        new
                        {
                            Id = new Guid("21c454e1-c95c-4954-8604-e00d1bc2ccd1"),
                            ConcurrencyStamp = "63c103bb-013e-4711-b4a3-eefb987c5d76",
                            Type = "family_name",
                            UserId = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                            Value = "Flagg"
                        },
                        new
                        {
                            Id = new Guid("710a73de-a63e-4148-99e4-36f470d8d00f"),
                            ConcurrencyStamp = "1afb5101-0adb-4974-a31e-6f8d085f93b6",
                            Type = "country",
                            UserId = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                            Value = "nl"
                        },
                        new
                        {
                            Id = new Guid("9d0f6f45-6117-489c-889b-10e1a2e5eaba"),
                            ConcurrencyStamp = "4bc77b44-2b54-4411-a0dd-7a4f45469e98",
                            Type = "role",
                            UserId = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                            Value = "FreeUser"
                        },
                        new
                        {
                            Id = new Guid("7ff27784-43c4-4bb1-820d-7612505ac918"),
                            ConcurrencyStamp = "2fa17a87-a495-4088-991c-a3e536193bc0",
                            Type = "given_name",
                            UserId = new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                            Value = "Emma"
                        },
                        new
                        {
                            Id = new Guid("5c27d54c-37e5-4621-987a-0652efd006f9"),
                            ConcurrencyStamp = "4178f833-87d2-4f0a-9d54-eadab8b0cc5c",
                            Type = "family_name",
                            UserId = new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                            Value = "Flagg"
                        },
                        new
                        {
                            Id = new Guid("e6bfd28d-3a65-4b89-89a6-be0373db0dc9"),
                            ConcurrencyStamp = "4bdf3bb6-7b62-47d4-b52b-1c3d7a6e406a",
                            Type = "country",
                            UserId = new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                            Value = "be"
                        },
                        new
                        {
                            Id = new Guid("8c688928-3ca4-471a-afc7-b6677c320133"),
                            ConcurrencyStamp = "ff5bb99f-3658-44b7-9605-48827b809ac8",
                            Type = "role",
                            UserId = new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                            Value = "PayingUser"
                        });
                });

            modelBuilder.Entity("Wenlin.IDP.Entities.UserClaim", b =>
                {
                    b.HasOne("Wenlin.IDP.Entities.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Wenlin.IDP.Entities.User", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
