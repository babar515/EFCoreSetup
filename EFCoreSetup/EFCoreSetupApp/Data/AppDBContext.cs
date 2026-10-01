using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetupApp.Data;

public partial class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookPrice> BookPrices { get; set; }

    public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.HasIndex(e => e.Title, "IX_Book_Title");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Book_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Book_IsActive");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Book_Author");

            entity.HasOne(d => d.Language).WithMany(p => p.Books)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Language");
        });

        modelBuilder.Entity<BookPrice>(entity =>
        {
            entity.ToTable("BookPrice");

            entity.HasIndex(e => e.BookId, "IX_BookPrice_BookId");

            entity.HasIndex(e => e.CurrencyId, "IX_BookPrice_CurrencyId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Book).WithMany(p => p.BookPrices)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_BookPrice_Book");

            entity.HasOne(d => d.Currency).WithMany(p => p.BookPrices)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookPrice_CurrencyType");
        });

        modelBuilder.Entity<CurrencyType>(entity =>
        {
            entity.ToTable("CurrencyType");

            entity.HasIndex(e => e.Currency, "UQ_CurrencyType_Currency").IsUnique();

            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
