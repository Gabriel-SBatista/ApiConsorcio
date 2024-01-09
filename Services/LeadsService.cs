using ApiConsorcio.Models;
using ApiConsorcio.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace ApiConsorcio.Services;

public class LeadsService
{
    private readonly LeadsRepository _leadsRepository;
    private readonly IValidator<Lead> _leadsValidator;
    private readonly ExportService _exportService;
    private readonly EmailService _emailService;

    public LeadsService(LeadsRepository leadsRepository, IValidator<Lead> leadsValidator, ExportService exportService, EmailService emailService)
    {
        _leadsRepository = leadsRepository;
        _leadsValidator = leadsValidator;
        _exportService = exportService;
        _emailService = emailService;
    }

    public async Task<IEnumerable<string>?> Create(Lead lead)
    {
        ValidationResult result = _leadsValidator.Validate(lead);

        if(!result.IsValid)
        {
            var message = result.Errors.Select(e => e.ErrorMessage);
            return message;
        }

        await _leadsRepository.Add(lead);
        await _emailService.SendEmailAsync(lead.Email);

        return null;
    }

    public async Task<IEnumerable<Lead>> SearchLeads()
    {
        var leads = await _leadsRepository.GetAll();
        return leads;
    }

    public async Task<IEnumerable<Lead>> SerachLeadsByCompany(string company)
    {
        var leads = await _leadsRepository.GetByCompany(company);
        return leads;
    }

    public async Task<IEnumerable<Lead>> SearchLeadsForExport(DateTime initialDate, DateTime finalDate, bool? exported, string userCompany)
    {
        return await _leadsRepository.GetByDate(initialDate, finalDate, exported, userCompany);
    }

    public async Task UpdateExported(IEnumerable<Lead> leads, int userId)
    {
        foreach (var lead in leads)
        {
            var export = new Export();
            export.DateExport = DateTime.UtcNow;
            export.ExportedBy = userId;
            export.LeadId = lead.LeadId;
            

            await _exportService.Create(export);
            
            lead.Exported = true;
            await _leadsRepository.UpdateExported(lead.LeadId);
        }
    }
}
