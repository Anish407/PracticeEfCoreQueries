// See https://aka.ms/new-console-template for more information

using LibraryEfConsole.EfCoreQueries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services= new ServiceCollection()
    .AddDbContext<LibraryEfConsole.LibraryContext>(op=> 
        op.UseSqlServer("Server=.;Database=library;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"));
var dbContext = services.BuildServiceProvider();

var efCoreQuery = new EfCoreQuery(dbContext.GetRequiredService<LibraryEfConsole.LibraryContext>());

await efCoreQuery.RunQueries();
Console.ReadLine();