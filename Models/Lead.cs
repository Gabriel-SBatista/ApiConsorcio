using FluentValidation;
using MongoDB.Bson;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiConsorcio.Models;

public class Lead : Entity
{
    public Lead()
    {
        DateLead = DateTime.UtcNow;
        Exported = false;
        Exports = new Collection<Export>();
    }

    public string? Name { get; set; }
    public string? Email { get; set; }
    public long Telephone { get; set; }
    public string? City { get; set; }
    public string? Source { get; set; }
    public string? Campaign { get; set; }
    public DateTime? DateLead { get; set; }
    public bool Exported { get; set; }
    public string Company { get; set; }
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
        RuleFor(l => l.Name).NotEmpty().MaximumLength(30);
        RuleFor(l => l.VerifyTelephone()).Must(v => v == true).NotEmpty().WithMessage("Telefone invalido!");
        RuleFor(l => l.City).NotEmpty().MaximumLength(40);
    }
}
