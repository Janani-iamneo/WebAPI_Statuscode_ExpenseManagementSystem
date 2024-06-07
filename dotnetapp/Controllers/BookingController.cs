// Controllers/BookingController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAllBookings()
        {
            var bookings = _bookingService.GetAllBookings();
            if (bookings == null || !bookings.Any())
            {
                return NoContent(); // HTTP 204
            }
            return Ok(bookings); // HTTP 200
        }

        [HttpGet("{bookingId}")]
        public ActionResult<Booking> GetBookingById(int bookingId)
        {
            var booking = _bookingService.GetBookingById(bookingId);
            if (booking == null)
            {
                return NotFound(); // HTTP 404
            }
            return Ok(booking); // HTTP 200
        }

        [HttpPost]
        public ActionResult<Booking> CreateBooking(Booking newBooking)
        {
            if (newBooking == null)
            {
                return BadRequest(); // HTTP 400
            }
            _bookingService.AddBooking(newBooking);
            return CreatedAtAction(nameof(GetBookingById), new { bookingId = newBooking.BookingId }, newBooking); // HTTP 201
        }

        [HttpPut("{bookingId}")]
        public ActionResult UpdateBooking(int bookingId, Booking updatedBooking)
        {
            var existingBooking = _bookingService.GetBookingById(bookingId);
            if (existingBooking == null)
            {
                return NotFound(); // HTTP 404
            }
            _bookingService.UpdateBooking(bookingId, updatedBooking);
            return NoContent(); // HTTP 204
        }

        [HttpDelete("{bookingId}")]
        public ActionResult DeleteBooking(int bookingId)
        {
            var existingBooking = _bookingService.GetBookingById(bookingId);
            if (existingBooking == null)
            {
                return NotFound(); // HTTP 404
            }
            _bookingService.DeleteBooking(bookingId);
            return NoContent(); // HTTP 204
        }
    }
}
