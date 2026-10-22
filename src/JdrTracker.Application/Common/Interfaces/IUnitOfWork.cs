namespace JdrTracker.Application.Common.Interfaces;

//Uityofwork: regroupe plusieurs modifications dans une seule transaction, ici c'est pour modifier puis sauvegarder
//Découple l'Application d'EF Core, qui est l'implémentation concrète derrière
public interface IUnitOfWork
{
    //Valide en base toutes les modifications en attente, renvoie le nombre de lignes affectées
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}