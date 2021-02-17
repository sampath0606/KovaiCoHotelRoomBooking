using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementAPI.Model;
using HotelManagementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Controllers
{
    [Route("api/RoomBooking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IRoomsService service;
        private string Internal_error = "Something went wrong, please contact admin!";


        public BookingController(IRoomsService _service)
        {
            this.service = _service;
        }

        [HttpGet]
        [Route("CheckAvailability/{FromDate}/{ToDate}")]
        public List<Room> Get(DateTime FromDate, DateTime ToDate, string roomType)
        {
            return service.GetRoomsAvailability(FromDate, ToDate);
        }

        [HttpGet]
        [Route("ReserveRoom/{RoomNo}/{FrmDate}/{Tdate}/{username}")]
        public ActionResult ReserveRoom(string RoomNo, string FrmDate,string Tdate, string username)
        {
            RoomNo = RoomNo.Trim();
            Reserved reserved = new Reserved();

            reserved.FromDate = Convert.ToDateTime(FrmDate);
            reserved.ToDate = Convert.ToDateTime(Tdate);
            reserved.BookedBy = username;

            if (RoomNo != null && reserved != null && ModelState.IsValid)
            {
                if (!service.IsBookingExists(RoomNo, reserved))
                {
                    if (service.AddReservation(RoomNo, reserved))
                    {

                        return StatusCode(200, "Room Reservation Succssfull");
                    }
                    else
                    {
                        return StatusCode(401, $"Something wrong with the reservation , contact admin");
                    }

                }
                else
                {
                    return StatusCode(499, $"A Booking Alreay Exists with the Room No : {RoomNo}");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetRoomsByBookedName/{username}")]
        public ActionResult<List<UserBookedRooms>> Get(string username)
        {
           
            if (username != null)
            {
                var roomsBooked =  service.GetRoomsByBookedName(username);

                List<UserBookedRooms> userBookedRooms = new List<UserBookedRooms>();

                foreach (var roomsItem in roomsBooked)
                {
                    if (roomsItem.Reserved != null)
                    {
                        foreach (var roomsReserved in roomsItem.Reserved)
                        {
                            if(roomsReserved.BookedBy.Equals(username,StringComparison.InvariantCultureIgnoreCase))
                            {
                                UserBookedRooms bookedRoom = new UserBookedRooms();
                                bookedRoom.RoomNo = roomsItem.RoomNo;
                                bookedRoom.Type = roomsItem.Type;
                                bookedRoom.FromDate = roomsReserved.FromDate.ToShortDateString();
                                bookedRoom.ToDate = roomsReserved.ToDate.ToShortDateString();

                                if (Convert.ToDateTime(roomsReserved.FromDate.ToShortDateString()) > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                                {
                                    bookedRoom.canDeleteBooking = true;
                                }
                                else
                                {
                                    bookedRoom.canDeleteBooking = false;
                                }
                                userBookedRooms.Add(bookedRoom);
                            }
                        }
                    }
                }

                return userBookedRooms;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("DeleteBooking/{RoomNo}/{FrmDate}/{Tdate}/{username}")]
        public ActionResult DeleteBooking(string RoomNo, string FrmDate, string Tdate, string username)
        {
            RoomNo = RoomNo.Trim();
            Reserved reserved = new Reserved();

            reserved.FromDate = Convert.ToDateTime(FrmDate);
            reserved.ToDate = Convert.ToDateTime(Tdate);
            reserved.BookedBy = username;

            if (reserved != null && ModelState.IsValid)
            {
                service.DeleteBooking(RoomNo, reserved);

                return StatusCode(202);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}