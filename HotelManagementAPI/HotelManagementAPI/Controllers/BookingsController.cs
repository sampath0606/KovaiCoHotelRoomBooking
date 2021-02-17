using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementAPI.Model;
using HotelManagementAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Controllers
{
    [Authorize]
    [Route("api/Bookings")]
    public class BookingsController : ControllerBase
    {
        // GET: api/Room

        private readonly IRoomsService service;
        private string Internal_error = "Something went wrong, please contact admin!";


        public BookingsController(IRoomsService _service)
        {
            this.service = _service;
        }


        [HttpGet]
        [Route("GetAllRoomInfo/{FromDate}/{ToDate}")]

        public List<Room> GetAllRoomInfo(DateTime FromDate, DateTime ToDate)
        {
            List<Room> allrooms = new List<Room>();

            List<Room> avialblerooms = service.GetRoomsAvailability(FromDate, ToDate);

            avialblerooms.ForEach(x => x.Status = "Available");

            List<Room> totalRooms = service.GetAllRooms();

            totalRooms.RemoveAll(x => avialblerooms.Any(y => y.Number == x.Number));

            totalRooms.ForEach(x => x.Status = "Booked");

            allrooms.AddRange(avialblerooms);

            allrooms.AddRange(totalRooms);



            return allrooms;
        }

        [HttpGet]
        [Route("GetRoomsByBookedName/{username}")]
        public ActionResult<List<Room>> Get(string username)
        {
            if (username != null)
            {
                return service.GetRoomsByBookedName(username);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("CheckAvailability/{FromDate}/{ToDate}")]
        public List<Room> Get(DateTime FromDate, DateTime ToDate)
        {
            return service.GetRoomsAvailability(FromDate, ToDate);
        }

        // POST: api/Room
        [HttpPost]
        [Route("AddRoom")]
        public IActionResult Post([FromBody] Room value)
        {
            if (value != null && ModelState.IsValid)
            {
                if (!service.checkifRoomNoExists(value.RoomNo))
                {
                    service.AddRoom(value);
                    return Created("", value);
                }
                else
                {
                    return StatusCode(499, $"A Room Alreay Exists with the Same Room No : {value.RoomNo}");
                }
            }
            else
            {
                var val = ModelState.ErrorCount;
                return BadRequest();
            }

        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        //[Route("RemoveLog")]
        public void Delete(int id)
        {

            service.DeleteRoom(id);
        }

        [HttpDelete]
        [Route("DeleteBooking/{RoomNo}")]
        public ActionResult DeleteBooking(string RoomNo, Reserved reserved)
        {
            RoomNo = RoomNo.Trim();
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

        [HttpPost]
        [Route("ReserveRoom/{RoomNo}")]
        public ActionResult ReserveRoom(string RoomNo, Reserved reserved)
        {
            RoomNo = RoomNo.Trim();
            if (RoomNo!=null && reserved != null && ModelState.IsValid)
            {
                if (!service.IsBookingExists(RoomNo,reserved))
                {   
                    service.AddReservation(RoomNo, reserved);

                    return Created("Booked", reserved);
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
    }
}
