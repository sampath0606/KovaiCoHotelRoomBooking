using MongoDB.Driver;

namespace HotelManagementAPI.Model
{
    public interface IRoomsManagementContext
    {
        IMongoCollection<Room> rooms { get; }
    }
}
