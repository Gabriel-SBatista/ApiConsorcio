using System.ComponentModel.DataAnnotations;

namespace ApiConsorcio.Models;

public class Export
{
    public int ExportId { get; set; }
    [Required]
    public int? ExportedBy { get; set; }
    [Required]
    public DateTime? DateExport { get; set; }
    public int LeadId { get; set; }
    public Lead? Lead { get; set; }
}
