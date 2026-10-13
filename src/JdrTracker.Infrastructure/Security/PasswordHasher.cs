using System.Security.Claims;

using JdrTracker.Application.Common.Interfaces;
using JdrTracker.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client.Extensions.Msal;

namespace JdrTracker.Infrastructure.Security;

// Implémentation du hachage de mot de passe basée sur PasswordHasher<T> d'ASP.NET
// Le type générique < User > est imposé par la signature mais l'instance d'utilisateur n'est pas utilisée donc on peut remplacer par !null
public class PasswordHasher : IPasswordHasher
{
    // Outil de hachage Microsoft
    private readonly PasswordHasher<User> _hasher = new();

    // Hache un mot de passe en clair avant stockage en base
    public string Hash(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    // Vérifie qu'un mot de passe en clair correspond au hash stocké
    public bool Verify(string password, string hash)
    {
        var result = _hasher.VerifyHashedPassword(null!, hash, password);
        // L'enum a 3 valeurs : Failed, Success, SuccessRehashNeeded
        // On considère valide tout ce qui n'est pas Failed
        return result != PasswordVerificationResult.Failed;
    }
}
