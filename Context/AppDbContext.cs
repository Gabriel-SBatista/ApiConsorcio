using ApiConsorcio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConsorcio.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Lead> Leads { get; set; }
    public DbSet<Export> Exports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var typesToRegister = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => typeof(IEntityConfig).IsAssignableFrom(x) && !x.IsAbstract).ToList();

        foreach (var type in typesToRegister)
        {
            dynamic configurationInstance = Activator.CreateInstance(type);
            modelBuilder.ApplyConfiguration(configurationInstance);
        }
    }
}
