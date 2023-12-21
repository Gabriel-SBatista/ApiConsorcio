using ApiConsorcio.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiConsorcio.Controllers;

[ApiController]
public class ExportsController : ControllerBase
{
    private readonly ExportService _service;

    public ExportsController(ExportService service)
    {
        _service = service;
    }

    [HttpGet("exports")]
    public async Task<ActionResult> Get()
    {
        var exports = await _service.SearchExports();

        if (exports == null)
        {
            return NotFound();
        }

        return Ok(exports);
    }
}
