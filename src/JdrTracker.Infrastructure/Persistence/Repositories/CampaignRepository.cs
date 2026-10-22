using JdrTracker.Application.Common.Interfaces;
using JdrTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JdrTracker.Infrastructure.Persistence.Repositories;

//Implémentation EF Core de ICampaignRepository
//Seule cette couche connaît EF Core : l'Application ne manipule que l'interface
public class CampaignRepository : ICampaignRepository
{
    private readonly JdrTrackerDbContext _dbContext;

    public CampaignRepository(JdrTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Prépare l'insertion EFCore : mais rien n'est écrit tant que SaveChangesAsync n'est pas appelé
    public async Task AddAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        await _dbContext.Campaigns.AddAsync(campaign, cancellationToken);
    }

    //AnyAsync s'arrête dès la première correspondance, plus efficace que de charger l'entité
    public async Task<bool> ShareCodeExistsAsync(string shareCode, CancellationToken cancellationToken)
    {
        return await _dbContext.Campaigns.AnyAsync(c =>  c.ShareCode == shareCode, cancellationToken);
    }
}
