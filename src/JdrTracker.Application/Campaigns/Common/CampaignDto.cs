using JdrTracker.Domain.Enums;

namespace JdrTracker.Application.Campaigns.Common;

//Ici on utilise un record plutot qu'une classe car readonly et uniquement transfert de données (et pas modification)
public record CampaignDto(Guid Id, string Name, string ShareCode, CampaignStatus Status, DateTime CreatedAt, IReadOnlyList<CampaignMemberDto> Members);
