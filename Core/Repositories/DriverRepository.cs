using FormulaApi.Data;
using FormulaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulaApi.Core.Repositories;

public class DriverRepository: GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(ApiDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<Driver?> GetById(int Id)
    {
        try
        {
            return await _context.Drivers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override async Task<IEnumerable<Driver>> All()
    {
        try
        {
            return await _context.Drivers.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Driver?> GetByDriverNumber(int DriverNumber)
    {
        try
        {
            return await _context.Drivers.Where(s => s.DriverNumber == DriverNumber).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}