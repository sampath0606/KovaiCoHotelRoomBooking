using HotelManagementAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementAPI.Repository
{

    public interface IRoomsRepository
    {
        Room AddRoom(Room room);

        List<Room> GetAllRoomDetails();

        List<Room> GetRoomsByBookedName(string username);

        List<Room> GetRoomsAvailability(DateTime FromDate, DateTime ToDate);

        List<Room> GetRoomsNonAvailabiliteRooms(DateTime FromDate, DateTime ToDate);

        bool IsDuplicateRoomNoExists(string username);

        bool DeleteLog(int roomId);
        int FindMaxId(string roomId);

        bool DeleteBooking(string roomNo,Reserved reserved);

        bool AddReservation(string roomNo, Reserved reserved);

        bool IsBookingExists(string roomNo, Reserved res);

    }
}
