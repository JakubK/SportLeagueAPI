﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportLeagueAPI.Context;

namespace SportLeagueAPI.Migrations
{
    [DbContext(typeof(LeagueDbContext))]
    partial class LeagueDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("SportLeagueAPI.DTO.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Season");

                    b.Property<int?>("SettlementId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SettlementId");

                    b.ToTable("Events");

                    b.HasData(
                        new { Id = 1, Date = "2019-02-01", Name = "Test Event", Season = 1, SettlementId = 1 },
                        new { Id = 2, Date = "2018-01-02", Name = "Cool Event", Season = 2, SettlementId = 2 }
                    );
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EventId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Medias");

                    b.HasData(
                        new { Id = 1, Url = "https://google.com" },
                        new { Id = 2, Url = "https://wp.pl" },
                        new { Id = 3, Url = "https://wp.pl" },
                        new { Id = 4, Url = "https://wp.pl" },
                        new { Id = 5, Url = "https://wp.pl" },
                        new { Id = 6, EventId = 1, Url = "https://wp.pl" },
                        new { Id = 7, EventId = 1, Url = "https://wp.pl" }
                    );
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("MediaId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.ToTable("Newses");

                    b.HasData(
                        new { Id = 1, Date = "2012-02-03", Description = "Sample description of news", MediaId = 5, Name = "Test News" }
                    );
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MediaId");

                    b.Property<string>("Name");

                    b.Property<int?>("SettlementId");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.HasIndex("SettlementId");

                    b.ToTable("Players");

                    b.HasData(
                        new { Id = 1, MediaId = 3, Name = "Player 1", SettlementId = 1 },
                        new { Id = 2, MediaId = 4, Name = "Player 2", SettlementId = 2 }
                    );
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EventId");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Points");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Scores");

                    b.HasData(
                        new { Id = 1, EventId = 1, PlayerId = 1, Points = 10 },
                        new { Id = 2, EventId = 1, PlayerId = 2, Points = 10 },
                        new { Id = 3, EventId = 2, PlayerId = 1, Points = 40 }
                    );
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Settlement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("MediaId");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.ToTable("Settlements");

                    b.HasData(
                        new { Id = 1, MediaId = 1, Name = "Settlement 1" },
                        new { Id = 2, MediaId = 2, Name = "Settlement 2" }
                    );
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Event", b =>
                {
                    b.HasOne("SportLeagueAPI.DTO.Player")
                        .WithMany("Events")
                        .HasForeignKey("PlayerId");

                    b.HasOne("SportLeagueAPI.DTO.Settlement", "Settlement")
                        .WithMany("Events")
                        .HasForeignKey("SettlementId");
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Media", b =>
                {
                    b.HasOne("SportLeagueAPI.DTO.Event", "Event")
                        .WithMany("Medias")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.News", b =>
                {
                    b.HasOne("SportLeagueAPI.DTO.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId");
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Player", b =>
                {
                    b.HasOne("SportLeagueAPI.DTO.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId");

                    b.HasOne("SportLeagueAPI.DTO.Settlement", "Settlement")
                        .WithMany("Players")
                        .HasForeignKey("SettlementId");
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Score", b =>
                {
                    b.HasOne("SportLeagueAPI.DTO.Event", "Event")
                        .WithMany("Scores")
                        .HasForeignKey("EventId");

                    b.HasOne("SportLeagueAPI.DTO.Player", "Player")
                        .WithMany("Scores")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("SportLeagueAPI.DTO.Settlement", b =>
                {
                    b.HasOne("SportLeagueAPI.DTO.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId");
                });
#pragma warning restore 612, 618
        }
    }
}
