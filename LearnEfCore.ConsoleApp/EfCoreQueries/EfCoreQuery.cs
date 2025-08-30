using LibraryEfConsole.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryEfConsole.EfCoreQueries;

public class EfCoreQuery
{
    private readonly LibraryContext _libraryContext;

    public EfCoreQuery(LibraryContext libraryContext)
    {
        _libraryContext = libraryContext;
    }

    public async Task RunQueries()
    {
        //await QueryUniqueBooksPermember();
        //await Top3MembersByDistinctBooksIn2024();
        //await AuthorsWhoseBooksHaveBeenBorrowedByAtleast3Members();
        await MembersWhoBorrowedTheSameBookMultipleTimes();
    }

    //<summary>
    //Find the top 3 members who have borrowed the highest number of distinct books in the year 2024.
    //Count unique books per member (if a member borrowed the same book multiple times, count it only once).
    //Only consider borrowings where BorrowedDate falls in 2024.
    //Return: Member’s name + number of distinct books borrowed.
    //Sort by the number of distinct books (descending).
    //</summary>
    async Task QueryUniqueBooksPermember()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2025, 1, 1);
        var uniqueBooks = await _libraryContext.Borrowings
            .Where(i => i.BorrowedDate >= start && i.BorrowedDate < end)
            .GroupBy(i => new { i.MemberId, i.Member!.FullName })
            .Select(i => new
            {
                MemberId = i.Key.MemberId,
                MemberName = i.Key.FullName,
                DistinctBookCount = i.Select(b => b.BookId).Distinct().Count()
            }).OrderByDescending(i => i.DistinctBookCount).ToListAsync();

        foreach (var member in uniqueBooks)
        {
            Console.WriteLine($"Member: {member.MemberName}, Distinct Books Borrowed: {member.DistinctBookCount}");
        }
    }

    /// <summary>
    /// Find the top 3 members who have borrowed the highest number of distinct books in the year 2024.
    /// </summary>
    /// <returns></returns>
    async Task Top3MembersByDistinctBooksIn2024()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2025, 1, 1);


        var topMembers = await _libraryContext.Borrowings
            .Where(b => b.BorrowedDate >= start && b.BorrowedDate < end)
            .GroupBy(b => new { b.MemberId, b.Member!.FullName })
            .Select(g => new
            {
                MemberId = g.Key.MemberId,
                MemberName = g.Key.FullName,
                DistinctBookCount = g.Select(b => b.BookId).Distinct().Count()
            }).OrderBy(i => i.DistinctBookCount).Take(5).ToListAsync();

        // OrderByDescending(i => i.MemberName) to sort by name descending

        foreach (var member in topMembers)
        {
            Console.WriteLine($"Member: {member.MemberName}, Distinct Books Borrowed: {member.DistinctBookCount}");
        }

    }

    /// <summary>
    /// Find the authors whose books have been borrowed by at least 3 different members in 2024.
    // Only consider borrowings where BorrowedDate falls in 2024.
    // A member counts once per author(if they borrowed multiple books by the same author, that’s still just one member).
    // Return: Author’s name + number of distinct members who borrowed their books in 2024.
    //Sort descending by that distinct member count.
    /// </summary>
    /// <returns></returns>
    async Task AuthorsWhoseBooksHaveBeenBorrowedByAtleast3Members()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2025, 1, 1);
        var authors = await _libraryContext.Borrowings
                    .Where(b => b.BorrowedDate.HasValue &&
                                b.BorrowedDate >= start && b.BorrowedDate < end)
                    .GroupBy(b => new { b.Book!.AuthorId, b.Book!.Author!.Name })
                    .Select(g => new
                    {
                        g.Key.AuthorId,
                        AuthorName = g.Key.Name,
                        DistinctMemberCount = g.Select(x => x.MemberId).Distinct().Count()
                    })
                    .Where(x => x.DistinctMemberCount >= 3)
                    .OrderByDescending(x => x.DistinctMemberCount)
                    .ThenBy(x => x.AuthorName)
                    .ToListAsync();

        foreach (var author in authors)
        {
            Console.WriteLine($"Author: {author.AuthorName}, Distinct Members Borrowed: {author.DistinctMemberCount}");
        }
    }

    /// <summary>
    /// List all members who borrowed the same book more than once (at any time).
    /// If a member borrowed the same book multiple times, they should appear.
    ///  Return: Member’s name, Book title, and the number of times that member borrowed that book.
    /// Only include rows where the borrow count > 1.
    /// Sort by the borrow count descending.
    /// </summary>
    /// <returns></returns>
        async Task MembersWhoBorrowedTheSameBookMultipleTimes()
    {
         await MyAnswer();
         await ChatGptsAnswer();


        async Task MyAnswer()
        {
            var result = await _libraryContext.Borrowings
                .GroupBy(b => new { b.MemberId, b.BookId, b.Book!.Title })
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    MemberId = g.Key.MemberId,
                    BookId = g.Key.BookId,
                    BookTitle = g.Key.Title,
                    BorrowCount = g.Count()
                }).OrderByDescending(i=> i.BorrowCount).ToListAsync();
           
        }

        async Task ChatGptsAnswer()
        {
            var members = await _libraryContext.Borrowings
                        .GroupBy(b => new
                        {
                            b.MemberId,
                            b.Member.FullName,
                            b.BookId,
                            b.Book.Title
                        })
                        .Where(g => g.Count() > 1)
                        .Select(g => new
                        {
                            g.Key.MemberId,
                            MemberName = g.Key.FullName,
                            g.Key.BookId,
                            BookTitle = g.Key.Title,
                            BorrowCount = g.Count()
                        })
                        .OrderByDescending(x => x.BorrowCount)
                        .ThenBy(x => x.MemberName)
                        .ThenBy(x => x.BookTitle)
                        .ToListAsync();
        }
    }

}