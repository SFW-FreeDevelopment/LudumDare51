using LudumDare51.API.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LudumDare51.API.Database.Repositories;

public class BaseRepository<T> where T : BaseResource
{
    private readonly IMongoClient _mongoClient;
    protected string CollectionName;

    protected BaseRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public virtual async Task<List<T>> Get()
    {
        try
        {
            var items = await GetCollection().AsQueryable().ToListAsync();
            return items;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<T> Get(string id)
    {
        try
        {
            var item = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(w => w.Id.Equals(id));
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public virtual async Task<T> Create(T data)
    {
        try
        {
            data.Id ??= Guid.NewGuid().ToString();
            data.Version = 1;
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = data.CreatedAt;
            await GetCollection().InsertOneAsync(data);
            var items = await GetCollection().AsQueryable().ToListAsync();
            return items?.FirstOrDefault(x => x.Id.Equals(data.Id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<T> Update(string id, T data)
    {
        data.UpdatedAt = DateTime.UtcNow;
        data.Version++;
        await GetCollection().ReplaceOneAsync(x => x.Id.Equals(id), data);
        return data;
    }

    public virtual Task Delete(string id)
    {
        throw new NotImplementedException();
    }

    protected IMongoCollection<T> GetCollection()
    {
        var database = _mongoClient.GetDatabase("main");
        var collection = database.GetCollection<T>(CollectionName);
        return collection;
    }
}