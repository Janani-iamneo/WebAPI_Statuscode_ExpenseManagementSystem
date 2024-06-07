using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class dotnetappApplicationTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
        }

        [Test]
        public async Task GetAllBookings_ReturnsListOfBookings()
        {
            // No need for explicit data creation
            var response = await _httpClient.GetAsync("api/Booking");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<Booking[]>(content);

            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Length > 0);
        }

       [Test]
        public async Task GetBookingById_ReturnsBooking()
        {
            // No need for explicit data creation
            var bookingId = 3;
            var response = await _httpClient.GetAsync($"api/Booking/{bookingId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var @booking = JsonConvert.DeserializeObject<Booking>(content);

            Assert.IsNotNull(@booking);
            Assert.AreEqual(bookingId, @booking.BookingId);
        }


        [Test]
        public async Task GetBookingById_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var bookingId = 999;
            var response = await _httpClient.GetAsync($"api/Booking/{bookingId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task CreateBooking_ReturnsCreatedResponse()
        {
            var newBooking = new Booking
            {
                CustomerName = "New Customer",
                EventName = "New Event",
                BookingDate = DateTime.Now.AddDays(30),
                NumberOfTickets = 2
            };

            var json = JsonConvert.SerializeObject(newBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Booking", content);
            response.EnsureSuccessStatusCode();

            var createdBooking = JsonConvert.DeserializeObject<Booking>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdBooking);
            Assert.AreEqual(newBooking.CustomerName, createdBooking.CustomerName);
        }

        [Test]
        public async Task UpdateBooking_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var bookingId = 2;
            var newBooking = new Booking
            {
                CustomerName = "New Customer",
                EventName = "New Event",
                BookingDate = DateTime.Now.AddDays(30),
                NumberOfTickets = 2
            };

            var json = JsonConvert.SerializeObject(newBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Booking/{bookingId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateBooking_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var bookingId = 999;
            var newBooking = new Booking
            {
                CustomerName = "New Customer",
                EventName = "New Event",
                BookingDate = DateTime.Now.AddDays(30),
                NumberOfTickets = 2
            };

            var json = JsonConvert.SerializeObject(newBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Booking/{bookingId}", content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteBooking_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var newBooking = new Booking
            {
                CustomerName = "New Customer",
                EventName = "New Event",
                BookingDate = DateTime.Now.AddDays(30),
                NumberOfTickets = 2
            };

            var json = JsonConvert.SerializeObject(newBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var createResponse = await _httpClient.PostAsync("api/Booking", content);
            createResponse.EnsureSuccessStatusCode();

            var createdBooking = JsonConvert.DeserializeObject<Booking>(await createResponse.Content.ReadAsStringAsync());
            var bookingId = createdBooking.BookingId;

            var response = await _httpClient.DeleteAsync($"api/Booking/{bookingId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteBooking_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var bookingId = 999;

            var response = await _httpClient.DeleteAsync($"api/Booking/{bookingId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
