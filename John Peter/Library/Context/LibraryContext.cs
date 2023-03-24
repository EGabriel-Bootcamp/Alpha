using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Context;

public partial class LibraryContext : DbContext
{
    public LibraryContext() { }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options) { }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseMySql(
            "name=ConnectionStrings:LocalSQL",
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql")
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb4_0900_ai_ci").HasCharSet("utf8mb4");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Author");

            entity.HasIndex(e => e.PublisherId, "publisher_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");

            entity
                .HasOne(d => d.Publisher)
                .WithMany(p => p.Authors)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("Author_ibfk_1");

            entity
                .HasMany(d => d.Books)
                .WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorBook",
                    r =>
                        r.HasOne<Book>()
                            .WithMany()
                            .HasForeignKey("BookId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Author_Book_ibfk_2"),
                    l =>
                        l.HasOne<Author>()
                            .WithMany()
                            .HasForeignKey("AuthorId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Author_Book_ibfk_1"),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Author_Book");
                        j.HasIndex(new[] { "BookId" }, "book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                    }
                );
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Book");

            entity.HasIndex(e => e.AuthorId, "author_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Title).HasMaxLength(255).HasColumnName("title");

            entity
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorBook",
                    b =>
                        b.HasOne<Author>()
                            .WithMany()
                            .HasForeignKey("AuthorId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Author_Book_ibfk_1"),
                    a =>
                        a.HasOne<Book>()
                            .WithMany()
                            .HasForeignKey("BookId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Author_Book_ibfk_2"),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Author_Book");
                        j.HasIndex(new[] { "BookId" }, "book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                    }
                );
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Publisher");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
