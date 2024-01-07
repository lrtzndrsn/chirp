using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;
public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpContext _db;
    public AuthorRepository(ChirpContext db)
    {
        _db = db;
    }
    public async Task<int> GetTotalCheepCountFromAuthor(string author)
    {
        return await _db.Cheeps
        .Where(cheep => cheep.Author.UserName == author)
        .CountAsync();
    }

    public async Task<AuthorDTO?> FindAuthorByName(string author)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.UserName == author);
        if (authorModel == null)
        {
            throw new Exception("Could not find Author: " + author);
        }
        return new(authorModel.UserName!, authorModel.Email!);
    }

    public async Task<AuthorDTO?> FindAuthorByEmail(string email)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.Email == email);
        if (authorModel == null)
        {
            throw new Exception("Could not find Author from email: " + email);
        }
        return new(authorModel.UserName!, authorModel.Email!);
    }

    public void CreateAuthor(string name, string email)
    {
        var author = new Author()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = name,
            Email = email
        };
        _db.Authors.AddRange(author);
        _db.SaveChanges();
    }

    public async Task Follow(string authorWhoWantsToFollow, string authorToFollow)
    {
        Author authorWhoWantsToFollowModel = await FindAuthorModelByName(authorWhoWantsToFollow);
        Author authorToFollowModel = await FindAuthorModelByName(authorToFollow);

        authorWhoWantsToFollowModel.Following.Add(authorToFollowModel);
        authorToFollowModel.Followers.Add(authorWhoWantsToFollowModel);

        await _db.SaveChangesAsync();
    }

    public async Task Unfollow(string authorWhoWantsToUnfollow, string authorToUnfollow)
    {
        Author authorWhoWantsToFollowModel = await FindAuthorModelByName(authorWhoWantsToUnfollow);
        Author authorToFollowModel = await FindAuthorModelByName(authorToUnfollow);

        authorWhoWantsToFollowModel.Following.Remove(authorToFollowModel);
        authorToFollowModel.Followers.Remove(authorWhoWantsToFollowModel);

        await _db.SaveChangesAsync();
    }

    public async Task<Author> FindAuthorModelByName(string author)
    {
        Author? authorModel = await _db.Authors
                                    .Include(f => f.Following)
                                    .Include(f => f.Followers)
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(a => a.UserName == author);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist: " + author);
        }
        return authorModel;
    }

    public async Task<bool> IsUserFollowingAuthor(string authorUsername, string username)
    {
        var user = await FindAuthorModelByName(username);
        var author = await FindAuthorModelByName(authorUsername);
        return user.Following.Contains(author);
    }

    public async Task<List<AuthorDTO>> GetFollowing(string authorUsername)
    {
        var authorModel = await FindAuthorModelByName(authorUsername);
        return authorModel.Following
        .Select(author => new AuthorDTO(author.UserName!, author.Email!))
        .ToList();
    }

    public async Task<int> GetTotalCheepCountFromFollowersAndAuthor(string authorUserName)
    {
        var sum = await GetTotalCheepCountFromAuthor(authorUserName);
        var following = await GetFollowing(authorUserName);

        foreach (AuthorDTO author in following)
        {
            sum += await GetTotalCheepCountFromAuthor(author.name);
        }

        return sum;
    }
}