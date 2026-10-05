using JdrTracker.Application.Common.Interfaces;

namespace JdrTracker.Infrastructure.Persistence;

//Implémentation EF Core : SaveChangesAsync du DbContext est déjà une unité de travail
//Cette classe sert uniquement à ce que l'Application n'ait pas à connaître EF Core
public class UnitOfWork : IUnitOfWork
{
    private readonly JdrTrackerDbContext _dbContext;

    public UnitOfWork(JdrTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Valide en base toutes les modifications en attente, dans une seule transaction
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}