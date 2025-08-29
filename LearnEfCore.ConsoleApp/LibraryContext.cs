using Microsoft.EntityFrameworkCore;
using LibraryEfConsole.Models;

namespace LibraryEfConsole;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Borrowing> Borrowings => Set<Borrowing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(e =>
        {
            e.HasKey(x => x.AuthorId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(e =>
        {
            e.HasKey(x => x.BookId);
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Price).HasColumnType("decimal(10,2)");
            e.HasOne(x => x.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Member>(e =>
        {
            e.HasKey(x => x.MemberId);
            e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Borrowing>(e =>
        {
            e.HasKey(x => x.BorrowingId);
            e.HasOne(x => x.Member)
                .WithMany(m => m.Borrowings)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Book)
                .WithMany(b => b.Borrowings)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}