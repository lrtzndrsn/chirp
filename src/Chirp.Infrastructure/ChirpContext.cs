using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;

namespace Chirp.Infrastructure;
public class ChirpContext : ApiAuthorizationDbContext<Author>
{
    public DbSet<Cheep> Cheeps => Set<Cheep>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Like> Likes => Set<Like>();

    public ChirpContext(DbContextOptions<ChirpContext> options) : base(options, CreateOperationalStoreOptions())
    {
    }

    private static IOptions<OperationalStoreOptions> CreateOperationalStoreOptions()
    {
        var operationalStoreOptions = new OperationalStoreOptions();
        return Options.Create(operationalStoreOptions);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cheep>().Property(cheep => cheep.Text).HasMaxLength(160);
    }
}