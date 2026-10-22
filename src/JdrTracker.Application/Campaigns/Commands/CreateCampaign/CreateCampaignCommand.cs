using JdrTracker.Application.Campaigns.Common;
using MediatR;

namespace JdrTracker.Application.Campaigns.Commands.CreateCampaign;

//Ici on utilise un record plutot qu'une classe car on utilise un objet portant des données plutot qu'un comportement
//Par défaut les prop sont en lecture seule
//Et si on compare 2 records qui ont la même valeur ils seront égaux alors qu'avec une classe non (objets différents)
//Le Irequest sert d'interface pour MediatR qui trouvera le bon handler
public record CreateCampaignCommand(string Name, Guid UserId, string Nickname) : IRequest<CampaignDto>;
