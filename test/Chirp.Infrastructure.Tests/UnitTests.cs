using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core;
using Chirp.Infrastructure;
using Bogus;

namespace Chirp.Web.Tests;

public class UnitTests
{
    [Fact]
    public async void CheepRepositoryGetAllCheepsTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep() { 
            CheepId = Guid.NewGuid().ToString(), 
            Author = new Author() { 
                Id = authorGuid, 
                UserName = "Anton", 
                Email = "anlf@itu.dk" }, 
            Text = "Hej, velkommen til kurset.", 
            TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        List<CheepDTO> result = await repository.GetAllCheeps();

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }

    [Fact]
    public async void CheepRepositoryGetCheepsTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep() { 
            CheepId = Guid.NewGuid().ToString(), 
            Author = new Author() { 
                Id = authorGuid, 
                UserName = "Anton", 
                Email = "anlf@itu.dk" }, 
            Text = "Hej, velkommen til kurset.", 
            TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        List<CheepDTO> result = await repository.GetCheeps(1, 1);

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }

    [Fact]
    public async void CheepRepositoryGetCheepsFromAuthorTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep() { 
            CheepId = Guid.NewGuid().ToString(), 
            Author = new Author() { 
                Id = authorGuid, 
                UserName = "Anton", 
                Email = "anlf@itu.dk" }, 
            Text = "Hej, velkommen til kurset.", 
            TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        List<CheepDTO> result = await repository.GetCheepsFromAuthor("Anton", 1, 1);

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }

    [Fact]
    public async void CheepRepositoryGetTotalCheepCountTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep() { 
            CheepId = Guid.NewGuid().ToString(), 
            Author = new Author() { 
                Id = authorGuid, 
                UserName = "Anton",
                Email = "anlf@itu.dk" }, 
            Text = "Hej, velkommen til kurset.", 
            TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        int result = await repository.GetTotalCheepCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async void AuthorRepositoryGetTotalCheepCountFromAuthorTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new AuthorRepository(context);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep()
        {
            CheepId = Guid.NewGuid().ToString(),
            Author = new Author()
            {
                Id = authorGuid,
                UserName = "Anton",
                Email = "anlf@itu.dk"
            },
            Text = "Hej, velkommen til kurset.",
            TimeStamp = DateTime.Parse("2023-08-01 13:08:28")
        });
        context.SaveChanges();

        // Act
        int result = await repository.GetTotalCheepCountFromAuthor("Anton");

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async void FindAuthorByNameTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var authorRepository = new AuthorRepository(context);
        var cheepRepository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep()
        {
            CheepId = Guid.NewGuid().ToString(),
            Author = new Author()
            {
                Id = authorGuid,
                UserName = "Anton",
                Email = "anlf@itu.dk"
            },
            Text = "Hej, velkommen til kurset.",
            TimeStamp = DateTime.Parse("2023-08-01 13:08:28")
        });
        context.SaveChanges();

        // Act
        AuthorDTO? result = await authorRepository.FindAuthorByName("Anton");
        if (result == null)
        {
            Assert.True(false);
        }
        List<CheepDTO> authorCheepsList = await cheepRepository.GetCheepsFromAuthor("Anton", 1, 1);

        // Assert
        Assert.Equal("Anton", result.name);
        Assert.Equal("anlf@itu.dk", result.email);
        Assert.Single(authorCheepsList);
    }

    [Fact]
    public async void FindAuthorByEmailTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var authorRepository = new AuthorRepository(context);
        var cheepRepository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid().ToString(), Author = new Author() { Id = authorGuid, UserName = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        AuthorDTO? result = await authorRepository.FindAuthorByEmail("anlf@itu.dk");
        if (result == null)
        {
            Assert.True(false);
        }
        List<CheepDTO> authorCheepsList = await cheepRepository.GetCheepsFromAuthor("Anton", 1, 1);

        // Assert
        Assert.Equal("Anton", result.name);
        Assert.Equal("anlf@itu.dk", result.email);
        Assert.Single(authorCheepsList);
    }

    [Fact]
    public async void CreateAuthorTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var authorRepository = new AuthorRepository(context);
        var cheepRepository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.SaveChanges();

        // Act
        authorRepository.CreateAuthor("Anton", "anlf@itu.dk");
        AuthorDTO? result = await authorRepository.FindAuthorByEmail("anlf@itu.dk");
        if (result == null)
        {
            Assert.True(false);
        }
        List<CheepDTO> authorCheepsList = await cheepRepository.GetCheepsFromAuthor("Anton", 1, 1);

        // Assert
        Assert.Equal("Anton", result.name);
        Assert.Equal("anlf@itu.dk", result.email);
        Assert.Empty(authorCheepsList);
    }

    [Fact]
    public async void CreateCheepTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        var validator = new CheepValidator();
        await context.Database.EnsureCreatedAsync();
        var authorRepository = new AuthorRepository(context);
        var cheepRepository = new CheepRepository(context, validator);
        string authorGuid = Guid.NewGuid().ToString();
        context.SaveChanges();

        // Act
        authorRepository.CreateAuthor("Anton", "anlf@itu.dk");
        AuthorDTO? author = await authorRepository.FindAuthorByName("Anton");
        if (author == null)
        {
            Assert.True(false);
        }
        var cheep = new CheepDTO(Guid.NewGuid().ToString(), "Clara er sej", author.name, "2023-08-01 13:08:28");
        await cheepRepository.CreateCheep(cheep);
        List<CheepDTO> result = await cheepRepository.GetCheeps(1, 1);

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Clara er sej", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
        Assert.Single(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public async void NewlyCreatedAuthorIsFollowingGivenAmount(int followingAmount)
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var cheepRepo = new CheepRepository(context, new CheepValidator());
        var authorRepo = new AuthorRepository(context);
        UserFaker faker = new();
        faker.Init(followingAmount);
        var author = new Author() { UserName = "Anna", Email = "anna@itu.dk", Following = faker.authors };
        context.Authors.Add(author);
        context.SaveChanges();

        // Act
        Author result = await authorRepo.FindAuthorModelByName("Anna");

        // Assert
        Assert.Equal(followingAmount, result.Following.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public async void NewlyCreatedAuthorHasGivenAmountOfFollowers(int followersAmount)
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var cheepRepo = new CheepRepository(context, new CheepValidator());
        var authorRepo = new AuthorRepository(context);
        UserFaker faker = new();
        faker.Init(followersAmount);
        var author = new Author() { UserName = "Anna", Email = "anna@itu.dk", Followers = faker.authors };
        context.Authors.Add(author);
        context.SaveChanges();

        // Act
        Author result = await authorRepo.FindAuthorModelByName("Anna");

        // Assert
        Assert.Equal(followersAmount, result.Followers.Count);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(1, 5)]
    [InlineData(2, 10)]
    public async void NewlyCreatedAuthorHasAGivenAmountOfCheepsWithGivenAmountofLikes(int cheeps, int likes)
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var cheepRepo = new CheepRepository(context, new CheepValidator());
        var authorRepo = new AuthorRepository(context);
        UserFaker userFaker = new();
        CheepFaker cheepFaker = new();
        userFaker.Init(likes);
        cheepFaker.Init(cheeps);
        var author = new Author() { Cheeps = cheepFaker.cheeps, UserName = "Anna", Email = "anna@itu.dk" };
        context.Authors.Add(author);
        context.Authors.AddRange(userFaker.authors);
        context.SaveChanges();

        // Act
        author.Cheeps.ForEach(async c =>
        {
            for (int i = 0; i < likes; i++) await cheepRepo.Like(c.CheepId, userFaker.authors[i].UserName ?? throw new Exception());
        });

        Author result = await authorRepo.FindAuthorModelByName("Anna");

        // Assert
        Assert.Equal(cheeps, result.Cheeps.Count);
        Assert.Equal(likes, result.Cheeps.Select(c => c.Likes.Count).FirstOrDefault());
    }

    [Fact]
    public async void AuthorHasLikedCheep()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var cheepRepo = new CheepRepository(context, new CheepValidator());
        var authorRepo = new AuthorRepository(context);
        UserFaker userFaker = new();
        CheepFaker cheepFaker = new();
        cheepFaker.Init(1);
        var author = new Author() { Cheeps = cheepFaker.cheeps, UserName = "Anna", Email = "anna@itu.dk" };
        var author2 = new Author() { UserName = "Anton", Email = "anto@itu.dk" };
        context.Authors.Add(author);
        context.Authors.Add(author2);
        context.SaveChanges();


        // Act
        await cheepRepo.Like(author.Cheeps[0].CheepId, author2.UserName ?? throw new Exception());
        Author resultAuthor = await authorRepo.FindAuthorModelByName("Anna");
        bool result = await cheepRepo.HasUserLikedCheep(author.Cheeps[0].CheepId, author2.UserName ?? throw new Exception());

        // Assert
        Assert.True(result);
    }


    public class UserFaker
    {
        public List<Author> authors = new();

        public void Init(int count)
        {
            Faker<Author> authorFaker = new Faker<Author>()
            .RuleFor(a => a.UserName, u => u.Person.UserName)
            .RuleFor(a => a.Email, e => e.Person.Email);

            List<Author> users = authorFaker.Generate(count);

            authors.AddRange(users);
        }
    }
    public class CheepFaker
    {
        public List<Cheep> cheeps = new();

        public void Init(int count)
        {
            Faker<Cheep> cheepFaker = new Faker<Cheep>()
            .RuleFor(c => c.CheepId, c => c.Random.Guid().ToString())
            .RuleFor(c => c.Text, t => t.Lorem.Sentence())
            .RuleFor(c => c.TimeStamp, t => t.Date.Past());

            List<Cheep> fakeCheeps = cheepFaker.Generate(count);

            cheeps.AddRange(fakeCheeps);
        }
    }
}