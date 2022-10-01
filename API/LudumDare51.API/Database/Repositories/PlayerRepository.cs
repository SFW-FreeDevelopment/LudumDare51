using LudumDare51.API.Models;
using MongoDB.Driver;

namespace LudumDare51.API.Database.Repositories;

public class PlayerRepository : BaseRepository<Player>
{
    public PlayerRepository(IMongoClient mongoClient) : base(mongoClient)
    {
        CollectionName = "players";
    }
}