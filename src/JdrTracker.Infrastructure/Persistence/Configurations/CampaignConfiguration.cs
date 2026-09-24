using JdrTracker.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JdrTracker.Infrastructure.Persistence.Configurations;

//Classe de la configuration pour la table Campaigns en bdd
public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    //Property -> contrainte sur colonne
    //HasIndex -> contrainte sur l'index
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("Campaigns");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
        builder.Property(c => c.ShareCode).IsRequired().HasMaxLength(16);
        builder.HasIndex(c => c.ShareCode).IsUnique();
        builder.Property(c => c.Status).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();

        //Une campagne peux avoir plusieurs membres différent mais chaque membre une seule campagne unique donc relation One to many
        //Si on supprime une campagne, on supprime les membres associés a cette campagne
        builder.HasMany(c => c.Members)
            .WithOne()
            .HasForeignKey(m => m.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        //On précise a EF Core de ne pas passer par la propriété publique Members mais par celle privé _members
        builder.Metadata
            .FindNavigation(nameof(Campaign.Members))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}