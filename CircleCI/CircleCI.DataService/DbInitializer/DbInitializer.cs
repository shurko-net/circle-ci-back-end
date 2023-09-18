using CircleCI.DataService.Data;
using Microsoft.EntityFrameworkCore;

namespace CircleCI.DataService.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;

    public DbInitializer(AppDbContext context)
    {
        _context = context;
    }
    public void Initialize()
    {
        if (_context.Database.GetPendingMigrations().Any())
        {
            _context.Database.Migrate();
        }
    }
}