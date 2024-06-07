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
        public async Task GetAllStudents_ReturnsListOfStudents()
        {
            // No need for explicit data creation
            var response = await _httpClient.GetAsync("api/Student");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var students = JsonConvert.DeserializeObject<Student[]>(content);

            Assert.IsNotNull(students);
            Assert.IsTrue(students.Length > 0);
        }

       [Test]
        public async Task GetStudentById_ReturnsStudent()
        {
            // No need for explicit data creation
            var studentId = 3;
            var response = await _httpClient.GetAsync($"api/Student/{studentId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var @student = JsonConvert.DeserializeObject<Student>(content);

            Assert.IsNotNull(@student);
            Assert.AreEqual(studentId, @student.StudentId);
        }


        [Test]
        public async Task GetStudentById_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var studentId = 999;
            var response = await _httpClient.GetAsync($"api/Student/{studentId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task CreateStudent_ReturnsCreatedResponse()
        {
            var newStudent = new Student
            {
                Name = "New Student",
                Age = 20,
                Grade = "A"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Student", content);
            response.EnsureSuccessStatusCode();

            var createdStudent = JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdStudent);
            Assert.AreEqual(newStudent.Name, createdStudent.Name);
        }

        [Test]
        public async Task UpdateStudent_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var studentId = 2;
            var newStudent = new Student
            {
                Name = "New Student",
                Age = 20,
                Grade = "A"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Student/{studentId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateStudent_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var studentId = 999;
            var newStudent = new Student
            {
                Name = "New Student",
                Age = 20,
                Grade = "A"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Student/{studentId}", content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteStudent_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var newStudent = new Student
            {
                Name = "New Student",
                Age = 20,
                Grade = "A"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var createResponse = await _httpClient.PostAsync("api/Student", content);
            createResponse.EnsureSuccessStatusCode();

            var createdStudent = JsonConvert.DeserializeObject<Student>(await createResponse.Content.ReadAsStringAsync());
            var studentId = createdStudent.StudentId;

            var response = await _httpClient.DeleteAsync($"api/Student/{studentId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteStudent_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var studentId = 999;

            var response = await _httpClient.DeleteAsync($"api/Student/{studentId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
