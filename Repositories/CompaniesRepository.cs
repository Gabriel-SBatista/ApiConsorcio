using ApiConsorcio.Context;
using ApiConsorcio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConsorcio.Repositories;

public class CompaniesRepository
{
    private readonly AppDbContext _context;

    public CompaniesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAll()
    {
        var companies = await _context.Companys.AsNoTrackingWithIdentityResolution().ToListAsync();
        return companies;
    }

    public async Task<Company?> Get(int id)
    {
        var company = await _context.Companys.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(c => c.CompanyId == id);
        return company;
    }

    public async Task Add(Company company)
    {
        await _context.Companys.AddAsync(company);
        await _context.SaveChangesAsync();
    }

    public async Task<Company?> Update(int id, Company newCompany)
    {
        var actualCompany = await _context.Companys.FindAsync(id);

        if (actualCompany != null)
        {
            actualCompany.Name = newCompany.Name;
            _context.Update(actualCompany);
            await _context.SaveChangesAsync();
            return actualCompany;
        }

        return null;
    }

    public async Task<Company?> Remove(int id)
    {
        var company = _context.Companys.Find(id);
        if (company != null)
        {
            _context.Companys.Remove(company);
            await _context.SaveChangesAsync();
            return company;
        }

        return null;
    }
}
