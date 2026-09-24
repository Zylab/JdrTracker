using JdrTracker.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JdrTracker.Infrastructure.Persistence.Configurations;

//Classe de la configuration pour la table Notes en bdd
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    //Property -> contrainte sur colonne
    //HasIndex -> contrainte sur l'index
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        //UpdateMemberById et UpdatedAt sont optionnels, attribué lors de la modification d'une note
        builder.ToTable("Notes");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.GameSessionId).IsRequired();
        //On index GameSessionId car c'est la clé étrangère qui sera souvent utilisé pour récupérer les notes
        builder.HasIndex(n => n.GameSessionId);
        builder.Property(n => n.CreatedByMemberId).IsRequired();
        builder.Property(n => n.CreatedAt).IsRequired();
        builder.Property(n => n.Title).IsRequired().HasMaxLength(256);
        builder.Property(n => n.Content).IsRequired().HasMaxLength(10000);
        builder.Property(n => n.IsPrivate).IsRequired();
    }
}