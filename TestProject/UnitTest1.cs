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
        public async Task GetAllExpenses_ReturnsListOfExpenses()
        {
            // No need for explicit data creation
            var response = await _httpClient.GetAsync("api/Expense");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var expenses = JsonConvert.DeserializeObject<Expense[]>(content);

            Assert.IsNotNull(expenses);
            Assert.IsTrue(expenses.Length > 0);
        }

       [Test]
        public async Task GetExpenseById_ReturnsExpense()
        {
            // No need for explicit data creation
            var expenseId = 3;
            var response = await _httpClient.GetAsync($"api/Expense/{expenseId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var @expense = JsonConvert.DeserializeObject<Expense>(content);

            Assert.IsNotNull(@expense);
            Assert.AreEqual(expenseId, @expense.ExpenseId);
        }


        [Test]
        public async Task GetExpenseById_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var expenseId = 999;
            var response = await _httpClient.GetAsync($"api/Expense/{expenseId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task CreateExpense_ReturnsCreatedResponse()
        {
            var newExpense = new Expense
            {
                Description = "New Expense",
                Amount = 100.00m,
                Date = DateTime.Now,
                Category = "Miscellaneous"
            };

            var json = JsonConvert.SerializeObject(newExpense);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Expense", content);
            response.EnsureSuccessStatusCode();

            var createdExpense = JsonConvert.DeserializeObject<Expense>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdExpense);
            Assert.AreEqual(newExpense.Description, createdExpense.Description);
        }

        [Test]
        public async Task UpdateExpense_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var expenseId = 2;
            var newExpense = new Expense
            {
                Description = "New Expense",
                Amount = 100.00m,
                Date = DateTime.Now,
                Category = "Miscellaneous"
            };

            var json = JsonConvert.SerializeObject(newExpense);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Expense/{expenseId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateExpense_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var expenseId = 999;
            var newExpense = new Expense
            {
                Description = "New Expense",
                Amount = 100.00m,
                Date = DateTime.Now,
                Category = "Miscellaneous"
            };

            var json = JsonConvert.SerializeObject(newExpense);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Expense/{expenseId}", content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteExpense_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var newExpense = new Expense
            {
                Description = "New Expense",
                Amount = 100.00m,
                Date = DateTime.Now,
                Category = "Miscellaneous"
            };

            var json = JsonConvert.SerializeObject(newExpense);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var createResponse = await _httpClient.PostAsync("api/Expense", content);
            createResponse.EnsureSuccessStatusCode();

            var createdExpense = JsonConvert.DeserializeObject<Expense>(await createResponse.Content.ReadAsStringAsync());
            var expenseId = createdExpense.ExpenseId;

            var response = await _httpClient.DeleteAsync($"api/Expense/{expenseId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteExpense_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var expenseId = 999;

            var response = await _httpClient.DeleteAsync($"api/Expense/{expenseId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
