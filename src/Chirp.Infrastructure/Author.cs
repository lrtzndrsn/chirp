using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure;

/// <summary>
/// Represents a(n) author/user in the application.
/// </summary>
public class Author : IdentityUser
{
    /// <summary>
    /// Gets or sets a list of cheeps posted by the author.
    /// </summary>
    public List<Cheep> Cheeps { get; set; } = new();
    /// <summary>
    /// Gets or sets a list of followers of the author.
    /// </summary>
    public List<Author> Followers { get; set; } = new();
    /// <summary>
    /// Gets or sets a list of an authors following.
    /// </summary>
    public List<Author> Following { get; set; } = new();
    /// <summary>
    /// Gets or sets a list of cheeps an author has liked.
    /// </summary>
    public List<Like> Liked { get; set; } = new();

}