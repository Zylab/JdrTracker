using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace JdrTracker.Domain.Entities
{
    //Classe utilisateur
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        //Pour EF Core
        private User() { }

        //Constructeur privé
        private User(string name, string email, string passwordHash)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        //Verification basique avant creation
        public static User Create(string name, string email, string passwordHash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);
            var normalizedEmail = email.Trim().ToLowerInvariant();
            if (!MailAddress.TryCreate(normalizedEmail, out _))
            {
                throw new ArgumentException("Email non valide", nameof(email));
            }
            return new User(name.Trim(), normalizedEmail, passwordHash);
        }
    }
}
