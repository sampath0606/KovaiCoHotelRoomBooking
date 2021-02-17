using HotelManagementAPI.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementAPI.Repository
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly IRoomsManagementContext roomsManagementContext;

        public RoomsRepository(IRoomsManagementContext _roomsManagementContext)
        {
            roomsManagementContext = _roomsManagementContext;
        }

        public Room AddRoom(Room room)
        {
           
            room.RoomNo = room.RoomNo.ToUpper();
            roomsManagementContext.rooms.InsertOne(room);
            
            return room;

        }

        public bool DeleteLog(int roomid)
        {
            FilterDefinition<Room> filterDefinition = Builders<Room>.Filter.Eq(usr => usr.Number, roomid);
            return roomsManagementContext.rooms.DeleteOne(filterDefinition).IsAcknowledged;
        }

        public List<Room> GetAllRoomDetails()
        {
            
            return roomsManagementContext.rooms.Find(_ => true).ToList();
        }

        public List<Room> GetRoomsByBookedName(string username)
        {
            username = username.ToUpper().Trim();
            FilterDefinition<Reserved> filter = Builders<Reserved>.Filter.Where(x=>x.BookedBy.Equals(username));

            FilterDefinition<Room> filterDefinition = Builders<Room>.Filter.ElemMatch(x => x.Reserved, filter);
        
            return roomsManagementContext.rooms.Find(filterDefinition).ToList();
        }

        public int FindMaxId(string userId)
        {
            
            List<Room> lstrooms = roomsManagementContext.rooms.Find(_=>true).ToList<Room>();
            if (lstrooms.Count > 0)
            {
                return lstrooms.Max(lst => lst.Number);
            }
            else
            {
                return 0;
            }
        }

        List<Room> IRoomsRepository.GetRoomsAvailability(DateTime FrmDat, DateTime ToDat)
        {

            TimeSpan Tots = new TimeSpan(00, 00, 00);
            TimeSpan Fromts = new TimeSpan(00, 00, 00);
            ToDat = ToDat + Tots;
            FrmDat = FrmDat + Fromts;


            var fromfilter = Builders<Reserved>.Filter.Lte(x => x.FromDate, ToDat);

            var tofilter = Builders<Reserved>.Filter.Gte(x => x.ToDate, FrmDat);

            FilterDefinition<Room> filterDefinition = Builders<Room>.Filter.ElemMatch(x => x.Reserved, fromfilter & tofilter);
    
            FilterDefinition<Room> filterDefinitionfinal = Builders<Room>.Filter.Not(filterDefinition);


            return roomsManagementContext.rooms.Find(filterDefinitionfinal).ToList();
        }

        List<Room> IRoomsRepository.GetRoomsNonAvailabiliteRooms(DateTime FrmDat, DateTime ToDat)
        {

            TimeSpan Tots = new TimeSpan(00, 00, 00);
            TimeSpan Fromts = new TimeSpan(00, 00, 00);
            ToDat = ToDat + Tots;
            FrmDat = FrmDat + Fromts;


            var fromfilter = Builders<Reserved>.Filter.Lte(x => x.FromDate, ToDat);

            var tofilter = Builders<Reserved>.Filter.Gte(x => x.ToDate, FrmDat);

            FilterDefinition<Room> filterDefinition = Builders<Room>.Filter.ElemMatch(x => x.Reserved, fromfilter & tofilter);



            return roomsManagementContext.rooms.Find(filterDefinition).ToList();
        }

        public bool IsDuplicateRoomNoExists(string roomNo)
        {
            var filter = Builders<Room>.Filter.Eq(x => x.RoomNo, roomNo);

            return roomsManagementContext.rooms.Find(filter).CountDocuments() > 0;
        }

        public bool IsBookingExists(string roomNo, Reserved res)
        {
            TimeSpan Tots = new TimeSpan(00, 00, 00);
            TimeSpan Fromts = new TimeSpan(00, 00, 00);
            res.ToDate = res.ToDate + Tots;
            res.FromDate = res.FromDate + Fromts;
            res.BookedBy = res.BookedBy.ToUpper().Trim();

            var Roomfilter = Builders<Room>.Filter.Eq(x => x.RoomNo, roomNo);

            var fromfilter = Builders<Reserved>.Filter.Lte(x => x.FromDate, res.ToDate);

            var tofilter = Builders<Reserved>.Filter.Gte(x => x.ToDate, res.FromDate);

            FilterDefinition<Room> filters = Builders<Room>.Filter.ElemMatch(x => x.Reserved, fromfilter & tofilter);

            filters &= Roomfilter;

            var val= roomsManagementContext.rooms.Find(filters).CountDocuments();

            return val > 0;
        }

        public bool AddReservation(string roomNo, Reserved res)
        {

            TimeSpan Tots = new TimeSpan(00, 00, 00);
            TimeSpan Fromts = new TimeSpan(00, 00, 00);
            res.ToDate = res.ToDate + Tots;
            res.FromDate = res.FromDate + Fromts;
            res.BookedBy = res.BookedBy.ToUpper().Trim();

            
            var Roomfilter = Builders<Room>.Filter.Eq(x => x.RoomNo, roomNo);

            var update = Builders<Room>.Update.AddToSet(x => x.Reserved, res);

            var push = roomsManagementContext.rooms.UpdateOne(Roomfilter, update);

            return push.IsAcknowledged;
        }


        public bool DeleteBooking(string roomNo, Reserved res)
        {
            TimeSpan Tots = new TimeSpan(00, 00, 00);
            TimeSpan Fromts = new TimeSpan(00, 00, 00);
            res.ToDate = res.ToDate + Tots;
            res.FromDate = res.FromDate + Fromts;
            res.BookedBy = res.BookedBy.ToUpper().Trim();

            var Roomfilter = Builders<Room>.Filter.Eq(x => x.RoomNo, roomNo);


            var update = Builders<Room>.Update.Pull(x => x.Reserved, res);

            roomsManagementContext.rooms.FindOneAndUpdate(Roomfilter, update);

            return true;
        }

        
    }
}
