using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementAPI.Model
{
    public class RoomsManagementContext : IRoomsManagementContext
    {
        private readonly IMongoDatabase database;

        MongoClient client;

        public RoomsManagementContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }

        public IMongoCollection<Room> rooms => database.GetCollection<Room>("RoomManagement");
    }
}
