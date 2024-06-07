using System;

namespace dotnetapp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string EventName { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
