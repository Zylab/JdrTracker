using System;
using System.Collections.Generic;
using System.Text;

namespace JdrTracker.Application.Common.Interfaces;

// Hachage des mots de passe, défini côté Application pour rester indépendant de l'implémentation
// L'Infrastructure fournit la mise en œuvre concrète.
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}