﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SocialNetwork.Data;
using System;

namespace SocialNetwork.Migrations
{
    [DbContext(typeof(SocialNetworkDbContext))]
    [Migration("20171001135414_InitializeDatabase")]
    partial class InitializeDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialNetwork.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastTimeLoggedIn");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<byte[]>("ProfilePicture");

                    b.Property<DateTime>("RegisterOn");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserFriend", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("FriendId");

                    b.HasKey("UserId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("UserFriend");
                });

            modelBuilder.Entity("SocialNetwork.Models.UserFriend", b =>
                {
                    b.HasOne("SocialNetwork.Models.User", "User")
                        .WithMany("FriendshipsMade")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SocialNetwork.Models.User", "Friend")
                        .WithMany("FriendshipsAccepted")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}