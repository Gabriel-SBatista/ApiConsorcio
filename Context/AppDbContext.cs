using ApiConsorcio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConsorcio.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Lead> Leads { get; set; }
    public DbSet<Company> Companys { get; set; }
    public DbSet<Export> Exports { get; set; }
}
