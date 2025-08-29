using Microsoft.EntityFrameworkCore;

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
        await Top3MembersByDistinctBooksIn2024();
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
            }).OrderBy(i=> i.DistinctBookCount).Take(5).ToListAsync();

        // OrderByDescending(i => i.MemberName) to sort by name descending

        foreach (var member in topMembers)
        {
            Console.WriteLine($"Member: {member.MemberName}, Distinct Books Borrowed: {member.DistinctBookCount}");
        }

    }

}