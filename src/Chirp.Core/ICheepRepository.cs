namespace Chirp.Core;
/// <summary>
/// Defines the operations needed to work with cheep-related data
/// </summary>
public interface ICheepRepository
{
    /// <summary>
    /// Gets all the cheeps from the database.
    /// </summary>
    /// <param name="cheepPerPage"></param>
    /// <param name="pageNumber"></param>
    /// <returns>A task where the result is a list of all current cheeps in the database.</returns>
    public Task<List<CheepDTO>> GetCheeps(int cheepsPerPage, int pageNumber);
    /// <summary>
    /// Gets all the cheeps from an author.
    /// </summary>
    /// <param name="author"></param>
    /// <param name="cheepPerPage"></param>
    /// <param name="pageNumber"></param>
    /// <returns>A task where the result is a list of all the cheeps from specified author.</returns>
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int cheepsPerPage, int pageNumber);
    /// <summary>
    /// Gets all the cheeps from the database.
    /// </summary>
    /// <returns>A task where the result is a list of all current cheeps in the database</returns>
    public Task<List<CheepDTO>> GetAllCheeps();
    /// <summary>
    /// Gets the total amount of cheeps.
    /// </summary>
    /// <returns>A task where the result is the total amount of current cheeps in the database.</returns>
    public Task<int> GetTotalCheepCount();
    /// <summary>
    /// Gets all cheeps that should be shown on a private timeline.
    /// </summary>
    /// <param name="authorUsername"></param>
    /// <param name="limit"></param>
    /// <param name="pageNumber"></param>
    /// <returns>A task where the result is a list of cheeps that should be shown on specified authors timeline.</returns>
    public Task<List<CheepDTO>> GetPrivateTimelineCheeps(string authorUsername, int limit, int pageNumber);
    /// <summary>
    /// Creates a new cheep.
    /// </summary>
    /// <param name="cheep"></param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public Task CreateCheep(CheepDTO cheep);
    /// <summary>
    /// Likes a cheep.
    /// </summary>
    /// <param name="cheepId"></param>
    /// <param name="authorUsername"></param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task Like(string cheepId, string authorUsername);
    /// <summary>
    /// Dislikes a cheep.
    /// </summary>
    /// <param name="cheepId"></param>
    /// <param name="authorUsername"></param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task Dislike(string cheepId, string authorUsername);
    /// <summary>
    /// Gets the amount of likes of a cheep.
    /// </summary>
    /// <param name="cheepId"></param>
    /// <returns>A task where the result is the total number of likes on a cheep.</returns>
    public Task<int> GetLikesCount(string cheepId);
    /// <summary>
    /// Determines if a user has liked a cheep.
    /// </summary>
    /// <param name="cheepId"></param>
    /// <param name="authorUsername"></param>
    /// <returns>A task where the result says whether the specfied user has liked a specified cheep.</returns>
    public Task<bool> HasUserLikedCheep(string cheepId, string authorUsername);
}