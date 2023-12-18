using ApiConsorcio.Filters;
using ApiConsorcio.Models;
using ApiConsorcio.Services;
using Microsoft.AspNetCore.Authorization;
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
    [AuthorizationFilter]
    public async Task<ActionResult> Get()
    {

        var leads = await _leadsService.SearchLeads();

        if(leads == null)
            return NotFound();

        return Ok(leads);
    }

    [HttpGet("leads/exportar")]
    [Authorize] 
    public async Task<ActionResult> GetLeadExcel(DateTime initialDate, DateTime finalDate, bool notExported)
    {
        var leads = await _leadsService.SearchLeadsForExcel(initialDate, finalDate, notExported);

        var excelFile = _excelService.ExportLeadsToExcel(leads);
        var userId = int.Parse(HttpContext.Items["UserId"].ToString());
        await _leadsService.UpdateExported(leads, userId);

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
