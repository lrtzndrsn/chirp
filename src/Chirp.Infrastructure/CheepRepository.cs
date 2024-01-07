using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;
public class CheepRepository : ICheepRepository
{
    private readonly ChirpContext _db;
    private readonly CheepValidator _validator;
    public CheepRepository(ChirpContext db, CheepValidator validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<List<CheepDTO>> GetCheeps(int cheepsPerPage, int pageNumber)
    {
        int amountOfCheepsToSkip = (pageNumber - 1) * cheepsPerPage;

        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Skip(amountOfCheepsToSkip)
        .Take(cheepsPerPage)
        .Select(cheep => new CheepDTO(cheep.CheepId, cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetAllCheeps()
    {
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Select(cheep => new CheepDTO(cheep.CheepId, cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int cheepsPerPage, int pageNumber)
    {
        int amountOfCheepsToSkip = (pageNumber - 1) * cheepsPerPage;

        Author authorModel = await FindAuthorModelByName(author);

        List<CheepDTO> cheeps = await _db.Cheeps
            .Where(cheep => cheep.Author.Id == authorModel.Id)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip(amountOfCheepsToSkip)
            .Take(cheepsPerPage)
            .Select(cheep => new CheepDTO(cheep.CheepId, cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
            .ToListAsync();

        return cheeps;
    }

    public async Task<int> GetTotalCheepCount()
    {
        return await _db.Cheeps.CountAsync();
    }

    public async Task CreateCheep(CheepDTO cheep)
    {
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(cheep);

        if (!validationResult.IsValid)
        {
            throw new Exception("The cheep can be no more than 160 characters long!");
        }
        Author author = await FindAuthorModelByName(cheep.author);
        var newCheep = new Cheep { CheepId = cheep.id, Author = author, Text = cheep.message, TimeStamp = DateTime.Parse(cheep.timestamp) };
        author.Cheeps.Add(newCheep);
        _db.Cheeps.AddRange(newCheep);
        _db.SaveChanges();
    }

    public async Task<List<CheepDTO>> GetPrivateTimelineCheeps(string authorUsername, int limit, int pageNumber)
    {
        int cheepsToSkip = (pageNumber - 1) * limit;
        Author author = await FindAuthorModelByName(authorUsername);
        List<Author> following = author.Following;
        List<Cheep> cheeps = author.Cheeps;
        foreach (Author user in following)
        {
            Author followerAuthor = await FindAuthorModelByName(user.UserName!);
            cheeps.AddRange(followerAuthor.Cheeps);
        }

        return cheeps.OrderByDescending(cheep => cheep.TimeStamp)
            .Skip(cheepsToSkip)
            .Take(limit)
            .Select(cheep => new CheepDTO(cheep.CheepId, cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
            .ToList();
    }

    public async Task Like(string cheepId, string authorUsername)
    {
        Author author = await FindAuthorModelByName(authorUsername);
        Cheep cheepModel = await _db.Cheeps
                        .Include(c => c.Likes)
                        .FirstOrDefaultAsync(c => c.CheepId == cheepId) ?? throw new Exception("Cheep does not exist");
        var like = new Like { LikeId = Guid.NewGuid().ToString(), AuthorId = author.Id, CheepId = cheepId };
        cheepModel.Likes.Add(like);
        author.Liked.Add(like);
        _db.SaveChanges();
    }

    public async Task Dislike(string cheepId, string authorUsername)
    {
        Author author = await FindAuthorModelByName(authorUsername);
        Cheep cheepModel = await _db.Cheeps
                        .Include(c => c.Likes)
                        .FirstOrDefaultAsync(c => c.CheepId == cheepId) ?? throw new Exception("Cheep does not exist");
        Like like = await _db.Likes.FirstOrDefaultAsync(like => like.CheepId == cheepId && like.AuthorId == author.Id) ?? throw new Exception("Like does not exist");

        cheepModel.Likes.Remove(like);
        author.Liked.Remove(like);
        _db.SaveChanges();
    }

    public async Task<int> GetLikesCount(string cheepId)
    {
        Cheep cheepModel = await _db.Cheeps
                        .Include(c => c.Likes)
                        .FirstOrDefaultAsync(c => c.CheepId == cheepId) ?? throw new Exception("Cheep does not exist");
        return cheepModel.Likes.Count;
    }

    public async Task<bool> HasUserLikedCheep(string cheepId, string authorUsername)
    {
        Author authorModel = await _db.Authors
                        .FirstOrDefaultAsync(a => a.UserName == authorUsername) ?? throw new Exception("Author does not exist");
        Like? like = await _db.Likes.FirstOrDefaultAsync(like => like.CheepId == cheepId && like.AuthorId == authorModel.Id);
        return like != null;
    }

    private async Task<Author> FindAuthorModelByName(string author)
    {
        Author? authorModel = await _db.Authors
        .Include(a => a.Cheeps)
        .Include(a => a.Following)
        .Include(a => a.Liked)
        .AsSplitQuery()
        .FirstOrDefaultAsync(a => a.UserName == author) ?? throw new Exception("Author does not exist");
        return authorModel;
    }
}