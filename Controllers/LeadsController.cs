using ApiConsorcio.Filters;
using ApiConsorcio.Models;
using ApiConsorcio.Services;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace ApiConsorcio.Controllers;

[ApiController]
public class LeadsController : ControllerBase
{
    private readonly LeadsService _leadsService;
    private readonly ExcelService _excelService;

    public LeadsController(LeadsService leadsService, ExcelService excelService)
    {
        _leadsService = leadsService;
        _excelService = excelService;
    }

    [HttpGet("leads")]
    [AuthorizationFilter("Admin")]
    public async Task<ActionResult> Get()
    {

        var leads = await _leadsService.SearchLeads();

        if(leads == null || !leads.Any())
            return NotFound();

        return Ok(leads);
    }

    [HttpGet("company/leads")]
    [AuthorizationFilter("Company")]
    public async Task<ActionResult> GetByCompany()
    {
        var userCompany = HttpContext.Items["UserCompany"] as string;
        var leads = await _leadsService.SerachLeadsByCompany(userCompany);

        if (leads == null || !leads.Any())
        {
            return NotFound();
        }

        return Ok(leads);
    }

    [HttpGet("leads/exportar")]
    [AuthorizationFilter("Company")]
    public async Task<ActionResult> GetLeadExcel(DateTime initialDate, DateTime finalDate, bool? exported)
    {
        var userCompany = HttpContext.Items["UserCompany"] as string;
        var leads = await _leadsService.SearchLeadsForExport(initialDate, finalDate, exported, userCompany);

        var excelFile = _excelService.ExportLeadsToExcel(leads);
        var userId = HttpContext.Items["UserId"] as string;
        await _leadsService.UpdateExported(leads, int.Parse(userId));

        return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "leads_exportados.xlsx");
    }

    [HttpPost("lead")]
    public async Task<ActionResult> Post(Lead lead)
    {
        var error = await _leadsService.Create(lead);

        if (error != null)
        {
            return BadRequest(error);
        }

        return Ok();
    }
}
