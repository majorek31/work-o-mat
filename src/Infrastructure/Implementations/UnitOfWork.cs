using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Infrastructure.Persistance;

namespace WorkOMat.Infrastructure.Implementations;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}