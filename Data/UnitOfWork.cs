using FormulaApi.Core;
using FormulaApi.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FormulaApi.Data;

public class UnitOfWork:IUnitOfWork, IDisposable
{
    public IDriverRepository Drivers { get; private set; }
    private readonly ApiDbContext _context;

    public UnitOfWork(ApiDbContext context, ILoggerFactory loggerFactory )
    {
        _context = context;
        var _logger = loggerFactory.CreateLogger("logs");
        
        Drivers = new DriverRepository(_context, _logger);
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}