using ApiConsorcio.Models;
using ApiConsorcio.Repositories;

namespace ApiConsorcio.Services;

public class ExportService
{
    private readonly ExportRepository _repository;
    public ExportService(ExportRepository repository)
    {
        _repository = repository;
    }

    public async Task Create(Export export)
    {
        await _repository.Add(export);
    }
}
