using Microsoft.EntityFrameworkCore;
using JdrTracker.Domain.Entities;


namespace JdrTracker.Infrastructure.Persistence;

public class JdrTrackerDbContext : DbContext
{
    public JdrTrackerDbContext(DbContextOptions<JdrTrackerDbContext> options)
        : base(options)
    {
    }

    //Ici on déclare les modèles qu'on va utiliser, les racines d'agrégats
    //CampaignMember n'y ait pas parce qu'on passe par Campaign pour y accéder
    public DbSet<User> Users => Set<User>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<GameSession> GameSessions => Set<GameSession>();
    public DbSet<Note> Notes => Set<Note>();

    //Charge les configurations des entités pour construire le modèle bdd
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JdrTrackerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
