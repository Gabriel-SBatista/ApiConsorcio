using ApiConsorcio.Context;
using ApiConsorcio.Models;

namespace ApiConsorcio.Repositories;

public class ExportRepository
{
    private readonly AppDbContext _context;

    public ExportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Export export)
    {
        await _context.Exports.AddAsync(export);
        await _context.SaveChangesAsync();
    }
}
