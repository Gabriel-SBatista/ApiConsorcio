using ApiConsorcio.Filters;
using ApiConsorcio.Models;
using ApiConsorcio.Services;
using Microsoft.AspNetCore.Mvc;

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
    [TypeFilter(typeof(AuthorizationFilterAdmin))]
    public async Task<ActionResult> Get()
    {

        var leads = await _leadsService.SearchLeads();

        if(leads == null || !leads.Any())
            return NotFound();

        return Ok(leads);
    }

    [HttpGet("leads/buscar")]
    [TypeFilter(typeof(AuthorizationFilterUserCompany))]
    public async Task<ActionResult> GetByCompany(DateTime? initialDate, DateTime? finalDate, bool? exported)
    {
        var userCompanyName = HttpContext.Items["UserCompanyName"] as string;
        var leads = await _leadsService.SearchLeadsByCompany(initialDate, finalDate, exported, userCompanyName);

        if (leads == null || !leads.Any())
        {
            return NotFound();
        }

        return Ok(leads);
    }

    [HttpGet("leads/exportar")]
    [TypeFilter(typeof(AuthorizationFilterUserCompany))]
    public async Task<ActionResult> GetLeadExcel(DateTime? initialDate, DateTime? finalDate, bool? exported)
    {
        var userCompanyName = HttpContext.Items["UserCompanyName"] as string;
        var leads = await _leadsService.SearchLeadsByCompany(initialDate, finalDate, exported, userCompanyName);

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
