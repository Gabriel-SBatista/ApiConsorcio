using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiConsorcio.Models;

public class Company
{
    public Company()
    {
        Leads = new Collection<Lead>();
    }

    public int CompanyId { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
    [JsonIgnore]
    public ICollection<Lead>? Leads { get; set; }
}
