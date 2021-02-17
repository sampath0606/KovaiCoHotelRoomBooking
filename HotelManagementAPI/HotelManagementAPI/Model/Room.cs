using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementAPI.Model
{
    public class Room
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("RoomNo")]
        public string RoomNo { get; set; }

        [BsonElement("Number")]
        public int Number { get; set; }
        
        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Cost")]
        public double Cost { get; set; }

        [BsonElement("Occupancy")]
        public int Occupancy { get; set; }

        [BsonElement("AddedBy")]
        public string AddedBy { get; set; }

        [BsonIgnore]
        public string Status { get; set; }

        [BsonElement("Reserved")]
        public List<Reserved> Reserved { get; set; }
    }

    public class Reserved
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local,DateOnly =true)]
        public DateTime  FromDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime  ToDate { get; set; }

        public string BookedBy { get; set; }
    }

    public class UserBookedRooms
    {
        
        public string RoomNo { get; set; }

        public string Type { get; set; }
    

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool canDeleteBooking { get; set; }
    }
}
