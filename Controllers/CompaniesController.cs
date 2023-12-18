using ApiConsorcio.Models;
using ApiConsorcio.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiConsorcio.Controllers;

[ApiController]
[Route("companies")]
public class CompaniesController : ControllerBase
{
    private readonly CompaniesService _companiesService;

    public CompaniesController(CompaniesService companiesService)
    {
        _companiesService = companiesService;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var companies = await _companiesService.SearchCompanies();

        if (companies == null)
        {
            return NotFound();
        }

        return Ok(companies);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> Get(int id)
    {
        var company = await _companiesService.SearchCompany(id);

        if (company == null)
        {
            return NotFound();
        }

        return Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Company company)
    {
        await _companiesService.Create(company);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Company newCompany)
    {
        var company = await _companiesService.Edit(id, newCompany);
        
        if (company == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var company = await _companiesService.Delete(id);

        if (company == null)
        {
            return NotFound();
        }

        return Ok();
    }
}
