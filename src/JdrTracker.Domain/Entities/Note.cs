namespace JdrTracker.Domain.Entities;

//Classe pour les notes que prendront les joueurs pendant une session
public class Note
{
    public Guid Id { get; private set; }
    public Guid GameSessionId { get; private set; }
    public Guid CreatedByMemberId { get; private set; }
    public Guid? UpdatedByMemberId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public bool IsPrivate { get; private set; }

    //Constructeur privé pour EF Core
    private Note() { }

    private Note(Guid gameSessionId, Guid createdByMemberId, string title, string content, bool isPrivate)
    {
        Id = Guid.CreateVersion7();
        GameSessionId = gameSessionId;
        CreatedByMemberId = createdByMemberId;
        CreatedAt = DateTime.UtcNow;
        Title = title;
        Content = content;
        IsPrivate = isPrivate;
    }

    //Permet de créer une nouvelle note
    public static Note Create(Guid gameSessionId, Guid createdByMemberId, string title, string content, bool isPrivate)
    {
        if (gameSessionId == Guid.Empty) { throw new ArgumentException("Id game session invalide", nameof(gameSessionId)); }
        if (createdByMemberId == Guid.Empty) { throw new ArgumentException("Id user du createur de la note invalide", nameof(createdByMemberId)); }
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        
        return new Note(gameSessionId, createdByMemberId, title.Trim(), content.Trim(), isPrivate);
    }

    //Permet d'update une note
    public void Update(string title, string content, bool isPrivate, Guid updatedByMemberId)
    {
        if (updatedByMemberId == Guid.Empty) { throw new ArgumentException("Id user du modificateur de la note invalide", nameof(updatedByMemberId)); }
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        if (isPrivate != IsPrivate && updatedByMemberId != CreatedByMemberId)
            throw new InvalidOperationException("Seul l'auteur peut changer la visibilité de la note.");

        if(IsPrivate && updatedByMemberId != CreatedByMemberId)
            throw new InvalidOperationException("La note est privée, seul l'auteur peut la modifier.");

        Title = title.Trim();
        Content = content.Trim();
        UpdatedByMemberId = updatedByMemberId;
        UpdatedAt = DateTime.UtcNow;
        IsPrivate = isPrivate;
    }
}
