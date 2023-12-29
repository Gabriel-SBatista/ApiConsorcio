﻿using ApiConsorcio.Context;
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

    public async Task<IEnumerable<Lead>> GetAll()
    {
        var leads = await _context.Leads.AsNoTrackingWithIdentityResolution().ToListAsync();
        return leads;
    }

    public async Task<IEnumerable<Lead>> GetByDate(DateTime initialDate, DateTime finalDate, bool? exported)
    {
        if (exported == true)
        {
            var leads = await _context.Leads.Where(l => l.DateLead >= initialDate && l.DateLead <= finalDate && l.Exported == true).AsNoTrackingWithIdentityResolution().ToListAsync();
            return leads;
        }
        else if (exported == false)
        {
            var leads = await _context.Leads.Where(l => l.DateLead >= initialDate && l.DateLead <= finalDate && l.Exported == false).AsNoTrackingWithIdentityResolution().ToListAsync();
            return leads;
        }
        else
        {
            var leads = await _context.Leads.Where(l => l.DateLead >= initialDate && l.DateLead <= finalDate).AsNoTrackingWithIdentityResolution().ToListAsync();
            return leads;
        }       
    }

    public async Task UpdateExported(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        lead.Exported = true;
        _context.Update(lead);
        await _context.SaveChangesAsync();
    }
}
