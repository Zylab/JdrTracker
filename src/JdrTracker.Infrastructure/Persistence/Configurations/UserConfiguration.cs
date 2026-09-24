using JdrTracker.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JdrTracker.Infrastructure.Persistence.Configurations;


//Classe de la configuration pour la table Users en bdd
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    //Property -> contrainte sur colonne
    //HasIndex -> contrainte sur l'index
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
        builder.Property(u => u.CreatedAt).IsRequired();
    }
}
