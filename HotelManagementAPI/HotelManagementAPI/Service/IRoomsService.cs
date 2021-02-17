using HotelManagementAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementAPI.Service
{
    public interface IRoomsService
    {
        bool DeleteRoom(int roomNo);
        List<Room> GetAllRooms();
        List<Room> GetRoomsByBookedName(string username);
        List<Room> GetRoomsAvailability(DateTime FromDate, DateTime ToDate);
        Room AddRoom(Room room);
        int FindMaxId(string userId);
        bool checkifRoomNoExists(string roomNo);
        bool AddReservation(string roomNo, Reserved reserved);
        bool DeleteBooking(string roomNo, Reserved res);
        bool IsBookingExists(string roomNo, Reserved res);
    }
}
