using ApiConsorcio.Context;
using ApiConsorcio.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Export>> GetAll()
    {
        var exports = await _context.Exports.AsNoTrackingWithIdentityResolution().ToListAsync();
        return exports;
    }
}
