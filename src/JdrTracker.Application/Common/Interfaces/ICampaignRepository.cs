using JdrTracker.Domain.Entities;

namespace JdrTracker.Application.Common.Interfaces
{
    //Interface pour la manipulation d'une campagne
    public interface ICampaignRepository
    {
       //Ajout d'une campagne en asynchrone
       Task AddAsync(Campaign campaign, CancellationToken cancellationToken);
       //Vérifie l'unicité du code de partage de campagne
       Task<bool> ShareCodeExistsAsync(string shareCode, CancellationToken cancellationToken);
    }
}
