using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementAPI.Exceptions;
using HotelManagementAPI.Model;
using HotelManagementAPI.Repository;
using MongoDB.Driver;

namespace HotelManagementAPI.Service
{
    public class RoomsService : IRoomsService
    {
        private readonly IRoomsRepository roomsRepository;

        public RoomsService(IRoomsRepository _roomsRepository)
        {
            roomsRepository = _roomsRepository;
        }


        public Room AddRoom(Room room)
        {
            try
            {
                room.Number = FindMaxId(room.AddedBy) +1;
                return roomsRepository.AddRoom(room);
            }
            catch (MongoWriteConcernException ex)
            {
                if (ex.Code == 11000)
                {
                    throw new DuplicateException("User already exists for username and/or email");
                }
                else
                {
                    throw ex;
                }
            }
        }

       public bool DeleteRoom(int LogNumber)
        {
            try
            {
                return roomsRepository.DeleteLog(LogNumber);
            }
            catch
            {
                throw new NotFoundException($"This Note id not found");
            }
        }

        public List<Room> GetAllRooms()
        {
            try
            {
                List<Room> rooms = roomsRepository.GetAllRoomDetails();
                if (rooms != null)
                {
                    return rooms;
                }
                else
                {
                    throw new NotFoundException($"This Note id not found");
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<Room> GetRoomsByBookedName(string username)
        {
            try
            {
                List<Room> rooms = roomsRepository.GetRoomsByBookedName(username);
                if (rooms != null)
                {
                    return rooms;
                }
                else
                {
                    throw new NotFoundException($"This Note id not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int FindMaxId(string userId)
        {
            return roomsRepository.FindMaxId(userId);
        }

        public List<Room> GetRoomsAvailability(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                List<Room> rooms = roomsRepository.GetRoomsAvailability(FromDate,ToDate);
                if (rooms != null)
                {
                    return rooms;
                }
                else
                {
                    throw new NotFoundException($"This Note id not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool checkifRoomNoExists(string roomNo)
        {
            return roomsRepository.IsDuplicateRoomNoExists(roomNo);
        }

        public bool DeleteBooking(string roomNo, Reserved res)
        {
            return roomsRepository.DeleteBooking(roomNo, res); 
        }

        public bool AddReservation(string roomNo, Reserved res)
        {
            return roomsRepository.AddReservation(roomNo, res);
        }

        public bool IsBookingExists(string roomNo, Reserved res)
        {
            return roomsRepository.IsBookingExists(roomNo, res);
        }

        //public List<Room> GetRoomsNonAvailabiliteRooms(DateTime FromDate, DateTime ToDate)
        //{
        //   return roomsRepository.GetRoomsNonAvailabiliteRooms(FromDate, ToDate);
        //}
    }
}
