namespace LibraryEfConsole.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public int? PublishedYear { get; set; }
    public decimal? Price { get; set; }

    public int? AuthorId { get; set; }
    public Author? Author { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}

public class Member
{
    public int MemberId { get; set; }
    public string FullName { get; set; } = null!;
    public DateTime? JoinDate { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}

public class Borrowing
{
    public int BorrowingId { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public DateTime? BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}