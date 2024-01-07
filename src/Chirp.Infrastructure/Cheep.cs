namespace Chirp.Infrastructure;

/// <summary>
/// Represents a cheep in the application.
/// </summary>
public class Cheep
{
    /// <summary>
    /// Gets or sets the id of a cheep.
    /// </summary>
    public required string CheepId { get; set; }
    /// <summary>
    /// Gets or sets the author of a cheep.
    /// </summary>
    public required Author Author { get; set; }
    /// <summary>
    /// Gets or sets the text of a cheep.
    /// </summary>
    public required string Text { get; set; }
    /// <summary>
    /// Gets or sets the timestamp of a cheep.
    /// </summary>
    public DateTime TimeStamp { get; set; }
    /// <summary>
    /// Gets or sets the likes of a cheep.
    /// </summary>
    public List<Like> Likes { get; set; } = new();
}