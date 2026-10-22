using System.Security.Cryptography;

using JdrTracker.Domain.Enums;

namespace JdrTracker.Domain.Entities
{
    // Classe campagne JDR
    public class Campaign
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        //Pour partager la campagne et que d'autres utilisateurs puissent rejoindre
        public string ShareCode { get; private set; } = null!;
        public CampaignStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        //Liste des membres modifiables uniquement pour la classe -> Pour que personne puisse add en dehors
        private readonly List<CampaignMember> _members = new();
        //La liste des membres public qu'on peut accéder uniquement en lecture seule
        public IReadOnlyCollection<CampaignMember> Members => _members.AsReadOnly();

        //Pour EF Core
        private Campaign() { }

        //Constructeur privé
        private Campaign(string name, string shareCode)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            ShareCode = shareCode;
            Status = CampaignStatus.NotStarted;
            CreatedAt = DateTime.UtcNow;    
        }

        //Creation d'une nouvelle campagne en ajoutant le createur dans la liste de membre
        public static Campaign Create(string name, Guid userId, string nickname)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            var shareCode = GenerateShareCode();
            var newCampaign = new Campaign(name.Trim(), shareCode);
            newCampaign.AddMember(userId, nickname, CampaignRole.GameMaster);
            return newCampaign;
        }

        //Ajoute un membre dans la campagne
        //On vérifie si le membre est déjà dans la campagne ou non
        private void AddMember(Guid userId, string nickname, CampaignRole campaignRole)
        {
            if (_members.Any(m => m.UserId == userId)) {
                throw new InvalidOperationException("Membre déjà présent dans la campagne");
            }
            var newMember = CampaignMember.Create(userId, nickname, this.Id, campaignRole);
            _members.Add(newMember);
        }

        //Permet a un joueur de rejoindre la campagne
        public void Join(Guid userId, string nickname)
        {
            if (Status == CampaignStatus.Completed) { throw new InvalidOperationException("Campagne terminée, impossible d'ajouter un joueur"); }
            AddMember(userId, nickname, CampaignRole.Player);
        }

        //Génère le code de partage
        private static string GenerateShareCode()
        {
            //Ici on enlève I, O, L 0 ET 1 pour faciliter l'écriture du code de partage
            const string alphabetReduit = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
            return RandomNumberGenerator.GetString(alphabetReduit, 8);
        }
    }

}
