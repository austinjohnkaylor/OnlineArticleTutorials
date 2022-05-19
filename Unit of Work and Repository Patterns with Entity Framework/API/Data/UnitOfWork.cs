using API.Interfaces;
using API.Repositories;

namespace API.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;

    public IUserRepository Users { get; private set; }

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        Users = new UserRepository(context, _logger);
    }

    public async Task Complete()
    {
        _logger.LogInformation("Saving changes to app.db");
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
