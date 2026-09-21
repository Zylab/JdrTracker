using JdrTracker.Domain.Enums;

namespace JdrTracker.Domain.Entities
{
    //Classe pour les différentes sessions JDR
    public class GameSession
    {
        public Guid Id { get; private set; }
        public Guid CampaignId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ScheduledAt { get; private set; }
        public GameSessionStatus Status { get; private set; }
        public int? SessionNumber { get; private set; }

        //Constructeur privé pour EF Core
        private GameSession() { }

        private GameSession(Guid campaignId, DateTime scheduledAt)
        {
            Id = Guid.CreateVersion7();
            CampaignId = campaignId;
            CreatedAt = DateTime.UtcNow;
            ScheduledAt = scheduledAt;
            Status = GameSessionStatus.Planned;
        }

        //On créer et planifie une session
        public static GameSession Create(Guid campaignId, DateTime scheduledAt)
        {
            if (campaignId == Guid.Empty) { throw new ArgumentException("Id campagne invalide", nameof(campaignId)); }
            if (scheduledAt == default) { throw new ArgumentException("La date est vide.", nameof(scheduledAt)); }

            return new GameSession(campaignId, scheduledAt);
        }

        //On démarre la session uniquement si son status est Planned
        public void Start(int newSessionNumber) {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newSessionNumber);
            if(Status != GameSessionStatus.Planned) { throw new InvalidOperationException("Impossible de démarrer une session qui n'est pas planifiée");  }

            SessionNumber = newSessionNumber;
            Status = GameSessionStatus.InProgress;
        }

        //On annule la session uniquement si son status n'est pas Completed
        //On passe le numéro de session a null parce que la session est annulée
        public void Cancel()
        {
            if (Status == GameSessionStatus.Completed) { throw new InvalidOperationException("Impossible d'annuler une session terminée"); }

            SessionNumber = null;
            Status = GameSessionStatus.Cancelled;
        }

        //On termine la session uniquement si son status était InProgress
        public void Complete()
        {
            if (Status != GameSessionStatus.InProgress) { throw new InvalidOperationException("Impossible de terminer une session qui n'est pas en cours"); }
            Status = GameSessionStatus.Completed;
        }
    }
}
