using System.Data.Common;
using System.Runtime.CompilerServices;
using API.Controllers;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.UnitTests;

[Trait("Category", "UnitTests")]
public class UserRepositoryTests
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<ApplicationDbContext> _contextOptions;
    
    public UserRepositoryTests(DbConnection connection, DbContextOptions<ApplicationDbContext> contextOptions)
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        using var context = new ApplicationDbContext(_contextOptions);

        context.AddRange(
            new User { FirstName = "Austin", LastName = "Kaylor", Email = "austinjohnkaylor@outlook.com", Id = new Guid() },
            new User { FirstName = "John", LastName = "Doe", Email = "johndoe@outlook.com", Id = new Guid() },
            new User { FirstName = "Jane", LastName = "Doe", Email = "janedoe@outlook.com", Id = new Guid() });
        context.SaveChanges();
    }
    
    ApplicationDbContext CreateContext() => new(_contextOptions);

    [Fact(Skip = "Need to do")]
    public void UserRepository_Should_ReturnUser_ForUserId()
    {
        
    }
}