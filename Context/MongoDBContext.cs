using ApiConsorcio.Models;
using MongoDB.Driver;

namespace ApiConsorcio.Context;

public class MongoDBContext
{
    public static string ConnectionString { get; set; }
    public static string DatabaseName { get; set; }
    public static bool IsSSl { get; set; }
    public IMongoDatabase _dataBase { get; set; }

    public MongoDBContext()
    {
        try
        {
            MongoClientSettings setting = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

            if(IsSSl)
            {
                setting.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                };
            }

            var mongoClient = new MongoClient(setting);
            _dataBase = mongoClient.GetDatabase(DatabaseName);
        }
        catch (Exception)
        {
            throw new Exception("Não foi possivel conectar");
        }
    }

    public IMongoCollection<Lead> Leads
    {
        get
        {
            return _dataBase.GetCollection<Lead>("Leads");
        }
    }
}
