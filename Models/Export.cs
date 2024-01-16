using System.ComponentModel.DataAnnotations;

namespace ApiConsorcio.Models;

public class Export : Entity
{
    public Export()
    {
        DateExport = DateTime.UtcNow;
    }

    public int? ExportedBy { get; set; }
    public DateTime? DateExport { get; set; }
    public Lead? Lead { get; set; }
}
