using JdrTracker.Domain.Enums;

namespace JdrTracker.Domain.Entities
{
    public class CampaignMember
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CampaignId { get; private set; }
        public string Nickname { get; private set; } = null!;
        public CampaignRole Role { get; private set; }
        public DateTime JoinedAt { get; private set; }

        //Pour EF Core
        private CampaignMember() { }

        //Constructeur privé
        private CampaignMember(Guid userId, string nickname, Guid campaignId, CampaignRole role)
        {
            Id = Guid.CreateVersion7();
            UserId = userId;
            Nickname = nickname;
            CampaignId = campaignId;
            Role = role;
            JoinedAt = DateTime.UtcNow;
        }

        //Permet de créer un campaignMember
        //On vérifie que les paramètres sont bon
        internal static CampaignMember Create(Guid userId, string nickname, Guid campaignId, CampaignRole role) {
            if (userId == Guid.Empty) { throw new ArgumentException("Id user invalide", nameof(userId)); }
            if (campaignId == Guid.Empty) { throw new ArgumentException("Id campagne invalide", nameof(campaignId)); }
            ArgumentException.ThrowIfNullOrWhiteSpace(nickname);
            if (!Enum.IsDefined(role)) { throw new ArgumentOutOfRangeException(nameof(role), "Role de la campagne non defini"); }

            return new CampaignMember(userId, nickname.Trim(), campaignId, role);
        }
    }
}
