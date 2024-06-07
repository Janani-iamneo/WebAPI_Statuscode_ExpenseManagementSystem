// Services/BookingService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models; // Adjust namespace to match your project structure

namespace dotnetapp.Services
{
    public class BookingService
    {
        private readonly List<Booking> _bookings;

        public BookingService()
        {
            _bookings = new List<Booking>
            {
                new Booking { BookingId = 1, CustomerName = "John Doe", EventName = "Concert A", BookingDate = DateTime.Now.AddDays(7), NumberOfTickets = 2 },
                new Booking { BookingId = 2, CustomerName = "Jane Smith", EventName = "Theater B", BookingDate = DateTime.Now.AddDays(14), NumberOfTickets = 4 },
                new Booking { BookingId = 3, CustomerName = "Bob Johnson", EventName = "Festival C", BookingDate = DateTime.Now.AddDays(21), NumberOfTickets = 1 }
            };
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookings;
        }

        public Booking GetBookingById(int bookingId)
        {
            return _bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        public void AddBooking(Booking newBooking)
        {
            newBooking.BookingId = _bookings.Count > 0 ? _bookings.Max(b => b.BookingId) + 1 : 1;
            _bookings.Add(newBooking);
        }

        public void UpdateBooking(int bookingId, Booking updatedBooking)
        {
            var existingBooking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (existingBooking != null)
            {
                existingBooking.CustomerName = updatedBooking.CustomerName;
                existingBooking.EventName = updatedBooking.EventName;
                existingBooking.BookingDate = updatedBooking.BookingDate;
                existingBooking.NumberOfTickets = updatedBooking.NumberOfTickets;
            }
        }

        public void DeleteBooking(int bookingId)
        {
            var existingBooking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (existingBooking != null)
            {
                _bookings.Remove(existingBooking);
            }
        }
    }
}
