using ApiConsorcio.Models;
using ApiConsorcio.Repositories;

namespace ApiConsorcio.Services;

public class CompaniesService
{
    private readonly CompaniesRepository _companiesRepository;

    public CompaniesService(CompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async Task<IEnumerable<Company>> SearchCompanies()
    {
        var companies = await _companiesRepository.GetAll();
        return companies;
    }

    public async Task<Company?> SearchCompany(int id)
    {
        var company = await _companiesRepository.Get(id);
        return company;
    }

    public async Task Create(Company company)
    {
        await _companiesRepository.Add(company);
    }

    public async Task<Company?> Edit(int id, Company newCompany)
    {
        var company = await _companiesRepository.Update(id, newCompany);
        return company;
    }

    public async Task<Company?> Delete(int id)
    {
        var company = await _companiesRepository.Remove(id);
        return company;
    }
}
