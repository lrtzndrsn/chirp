namespace Chirp.Infrastructure
{
    /// <summary>
    /// Represents a like of a cheep in the database.
    /// </summary>
    public class Like
    {
        /// <summary>
        /// Gets or sets the id of a cheep.
        /// </summary>
        public required string LikeId { get; set; }
        /// <summary>
        /// Gets or sets the id of an author.
        /// </summary>
        public string? AuthorId { get; set; }
        /// <summary>
        /// Gets or sets the id of a cheep.
        /// </summary>
        public string? CheepId { get; set; }
    }
}