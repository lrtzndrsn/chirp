namespace Chirp.Core;
/// <summary>
/// Defines the operations needed to work with author-related data
/// </summary>
public interface IAuthorRepository
{
    /// <summary>
    /// Gets the total amount of cheeps from an author.
    /// </summary>
    /// <param name="author"></param>
    /// <returns>A task where the result is the total cheep count from specified author.</returns>
    public Task<int> GetTotalCheepCountFromAuthor(string author);
    /// <summary>
    /// Finds a specific author based on a name.
    /// </summary>
    /// <param name="author"></param>
    /// <returns>A task where the result is the author corresponding to the specified name.</returns>
    public Task<AuthorDTO?> FindAuthorByName(string author);
    /// <summary>
    /// Finds a specific author based on an email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns>A task where the result is the author corresponding to the specified email.</returns>
    public Task<AuthorDTO?> FindAuthorByEmail(string email);
    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    public void CreateAuthor(string name, string email);
    /// <summary>
    /// Determines if a user is following an author.
    /// </summary>
    /// <param name="authorUsername"></param>
    /// <param name="username"></param>
    /// <returns>A task where the result is whether or not the specified user follows the specified author.</returns>
    public Task<bool> IsUserFollowingAuthor(string authorUsername, string username);
    /// <summary>
    /// Allows one author to follow another.
    /// </summary>
    /// <param name="authorWhoWantsToFollow"></param>
    /// <param name="authorToFollow"></param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public Task Follow(string authorWhoWantsToFollow, string authorToFollow);
    /// <summary>
    /// Allows one author to unfollow another
    /// </summary>
    /// <param name="authorWhoWantsToFollow"></param>
    /// <param name="authorToFollow"></param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public Task Unfollow(string authorWhoWantsToFollow, string authorToFollow);
    /// <summary>
    /// Gets the total amount of cheeps from the author and everyone they are following.
    /// </summary>
    /// <param name="authorUsername"></param>
    /// <returns>A task where the result is the total cheep count from specific author and everyone they follow</returns>
    public Task<int> GetTotalCheepCountFromFollowersAndAuthor(string authorUsername);
    /// <summary>
    /// Gets everyone that the author is following
    /// </summary>
    /// <param name="authorUsername"></param>
    /// <returns>A task where the result is a list of everyone the specified author is following</returns>
    public Task<List<AuthorDTO>> GetFollowing(string authorUsername);

}