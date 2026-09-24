using JdrTracker.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JdrTracker.Infrastructure.Persistence.Configurations;

//Classe de la configuration pour la table CampaignMembers en bdd
public class CampaignMemberConfiguration : IEntityTypeConfiguration<CampaignMember>
{
    //Property -> contrainte sur colonne
    //HasIndex -> contrainte sur l'index
    public void Configure(EntityTypeBuilder<CampaignMember> builder)
    {
        builder.ToTable("CampaignMembers");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.CampaignId).IsRequired();
        builder.Property(c => c.Nickname).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Role).IsRequired();
        builder.Property(c => c.JoinedAt).IsRequired();

        //Un User ne peut rejoindre qu'une fois la même campagne
        builder.HasIndex(c => new { c.UserId, c.CampaignId  }).IsUnique();
    }
}