﻿// <auto-generated />
using System;
using Lexify.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lexify.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("Lexify.Models.Definition", b =>
                {
                    b.Property<int>("DefinitionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DefinitionText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WordID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DefinitionID");

                    b.HasIndex("WordID");

                    b.ToTable("Definitions");
                });

            modelBuilder.Entity("Lexify.Models.Example", b =>
                {
                    b.Property<int>("ExampleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExampleText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WordID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExampleID");

                    b.HasIndex("WordID");

                    b.ToTable("Examples");
                });

            modelBuilder.Entity("Lexify.Models.Word", b =>
                {
                    b.Property<int>("WordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LearningStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("WordText")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("WordType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("WordID");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Lexify.Models.WordRelation", b =>
                {
                    b.Property<int>("RelationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelatedWordID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RelationType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("WordID")
                        .HasColumnType("INTEGER");

                    b.HasKey("RelationID");

                    b.HasIndex("RelatedWordID");

                    b.HasIndex("WordID");

                    b.ToTable("WordRelations");
                });

            modelBuilder.Entity("Lexify.Models.Definition", b =>
                {
                    b.HasOne("Lexify.Models.Word", "Word")
                        .WithMany("Definitions")
                        .HasForeignKey("WordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("Lexify.Models.Example", b =>
                {
                    b.HasOne("Lexify.Models.Word", "Word")
                        .WithMany("Examples")
                        .HasForeignKey("WordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("Lexify.Models.WordRelation", b =>
                {
                    b.HasOne("Lexify.Models.Word", "RelatedWord")
                        .WithMany("RelatedWordsTo")
                        .HasForeignKey("RelatedWordID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Lexify.Models.Word", "Word")
                        .WithMany("RelatedWordsFrom")
                        .HasForeignKey("WordID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RelatedWord");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("Lexify.Models.Word", b =>
                {
                    b.Navigation("Definitions");

                    b.Navigation("Examples");

                    b.Navigation("RelatedWordsFrom");

                    b.Navigation("RelatedWordsTo");
                });
#pragma warning restore 612, 618
        }
    }
}
