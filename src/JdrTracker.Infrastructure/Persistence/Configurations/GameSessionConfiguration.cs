using JdrTracker.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JdrTracker.Infrastructure.Persistence.Configurations;

//Classe de la configuration pour la table GameSessions en bdd
public class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
{
    //Property -> contrainte sur colonne
    //HasIndex -> contrainte sur l'index
    public void Configure(EntityTypeBuilder<GameSession> builder)
    {
        //SessionNumber est optionnel, attribué au démarrage de la session
        builder.ToTable("GameSessions");
        builder.HasKey(g => g.Id);

        builder.Property(g => g.CampaignId).IsRequired();
        builder.Property(g => g.CreatedAt).IsRequired();
        builder.Property(g => g.ScheduledAt).IsRequired();
        builder.Property(g => g.Status).IsRequired();

        //Une campagne ne peut pas avoir deux sessions avec le même numéro
        builder.HasIndex(g => new { g.CampaignId, g.SessionNumber }).IsUnique();
    }
}