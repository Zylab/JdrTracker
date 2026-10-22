using AutoMapper;
using JdrTracker.Domain.Entities;

namespace JdrTracker.Application.Campaigns.Common;

//Déclare les correspondances entre les entités du Domain et les DTOs renvoyés par l'API
public class CampaignMappingProfile : Profile
{
    //Les propriétés de même nom sont mappées automatiquement
    public CampaignMappingProfile()
    {
        CreateMap<Campaign, CampaignDto>();
        CreateMap<CampaignMember, CampaignMemberDto>();
    }
}
