using ApiConsorcio.Context;
using ApiConsorcio.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ApiConsorcio.Repositories;

public class LeadsRepository
{
    private readonly AppDbContext _context;

    public LeadsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Lead lead)
    {
        await _context.Leads.AddAsync(lead);
        await _context.SaveChangesAsync();
    }

    public async Task<Lead?> GetEmail(string email)
    {
        Lead? lead = await _context.Leads.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(l => l.Email == email);
        return lead;
    }

    public async Task<Lead?> GetTelephone(long telephone)
    {
        Lead? lead = await _context.Leads.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(l => l.Telephone == telephone);
        return lead;
    }

    public async Task<IEnumerable<Lead>> GetAll()
    {
        var leads = await _context.Leads.AsNoTrackingWithIdentityResolution().ToListAsync();
        return leads;
    }

    public async Task<IEnumerable<Lead>> GetByDate(DateTime initialDate, DateTime finalDate)
    {
        var leads = await _context.Leads.Where(l => l.DateLead >= initialDate && l.DateLead <= finalDate).AsNoTrackingWithIdentityResolution().ToListAsync();
        return leads;
    }
    public async Task<IEnumerable<Lead>> GetByDateNotExported(DateTime initialDate, DateTime finalDate)
    {
        var leads = await _context.Leads.Where(l => l.DateLead >= initialDate && l.DateLead <= finalDate && l.Exported == false).AsNoTrackingWithIdentityResolution().ToListAsync();
        return leads;
    }

    public async Task UpdateExported(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        lead.Exported = true;
        _context.Update(lead);
        await _context.SaveChangesAsync();
    }
}
