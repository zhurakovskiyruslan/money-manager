using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using Npgsql;

namespace MoneyManager.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public EfUnitOfWork(AppDbContext context)=>
        _context = context;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            return await _context.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrent update detected." , ex);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg)
        {
            switch (pg.SqlState)
            {
                case PostgresErrorCodes.UniqueViolation:       
                    throw new ConflictException("Duplicate value violates a unique constraint.", ex);

                case PostgresErrorCodes.ForeignKeyViolation:    
                    throw ApplicationValidationException.Single("Invalid reference.", "General");

                case PostgresErrorCodes.NotNullViolation:      
                    throw ApplicationValidationException.Single("Required value is missing.", "General");

                case PostgresErrorCodes.CheckViolation:         
                    throw ApplicationValidationException.Single("Data failed a check constraint.", "General");

                default:
                    throw;
            }
        }
    }
}