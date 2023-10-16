using Microsoft.EntityFrameworkCore;
namespace CharacterApi;

public class CharacterDbContext : DbContext
{
    public CharacterDbContext(DbContextOptions<CharacterDbContext> options) : base(options) { }

    public DbSet<CharacterDbEntity> Characters => Set<CharacterDbEntity>();

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();
}
