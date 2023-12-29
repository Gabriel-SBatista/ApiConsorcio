using FluentValidation;
using MongoDB.Bson;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiConsorcio.Models;

public class Lead
{
    public Lead()
    {
        DateLead = DateTime.UtcNow;
        Exported = false;
        Exports = new Collection<Export>();
    }

    public ObjectId LeadId { get; set; }
    [Required]
    [MaxLength(20)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Email { get; set; }
    [Required]
    public long Telephone { get; set; }
    [Required]
    [MaxLength(30)]
    public string? City { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Source { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Campaign { get; set; }
    [Required]
    public DateTime? DateLead { get; set; }
    [Required]
    public bool Exported { get; set; }
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company? Company { get; set; }
    [JsonIgnore]
    public ICollection<Export>? Exports { get; set; }

    public bool VerifyTelephone()
    {
        var telephone = Telephone.ToString();
        int maximumTelephoneLength = 11;

        if (telephone.Length > maximumTelephoneLength)
            return false;

        return true;
    }
}

public class LeadValidator : AbstractValidator<Lead>
{
    public LeadValidator()
    {
        RuleFor(l => l.Email).EmailAddress().WithMessage("Email invalido!");
        RuleFor(l => l.Name).NotEmpty().MaximumLength(20);
        RuleFor(l => l.VerifyTelephone()).Must(v => v == true).NotEmpty().WithMessage("Telefone invalido!");
        RuleFor(l => l.City).NotEmpty().MaximumLength(30);
    }
}
