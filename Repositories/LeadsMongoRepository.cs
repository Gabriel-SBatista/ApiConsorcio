using ApiConsorcio.Context;
using ApiConsorcio.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiConsorcio.Repositories;

public class LeadsMongoRepository
{
    private readonly MongoDBContext _dbContext;

    public LeadsMongoRepository(MongoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Lead>> GetAll()
    {
        return await _dbContext.Leads.Find(l => true).ToListAsync();
    }

    public async Task<IEnumerable<Lead>> GetByDate(DateTime initialDate, DateTime finalDate, bool? exported)
    {
        if (exported == true)
        {
            var leads = await _dbContext.Leads.Find(l => l.DateLead >= initialDate && l.DateLead <= finalDate && l.Exported == true).ToListAsync();
            return leads;
        }
        else if (exported == false)
        {
            var leads = await _dbContext.Leads.Find(l => l.DateLead >= initialDate && l.DateLead <= finalDate && l.Exported == false).ToListAsync();
            return leads;
        }
        else
        {
            var leads = await _dbContext.Leads.Find(l => l.DateLead >= initialDate && l.DateLead <= finalDate).ToListAsync();
            return leads;
        }
    }

    public async Task Create(Lead lead)
    {
        await _dbContext.Leads.InsertOneAsync(lead);
    }

    public async Task Update(ObjectId id, Lead lead)
    {
        await _dbContext.Leads.ReplaceOneAsync(l =>  l.LeadId == id, lead);
    }

    public async Task Delete(ObjectId id)
    {
        await _dbContext.Leads.DeleteOneAsync(l => l.LeadId == id);
    }
}
