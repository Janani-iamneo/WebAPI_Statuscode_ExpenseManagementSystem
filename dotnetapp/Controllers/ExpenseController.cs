// Controllers/ExpenseController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models; // Adjust namespace to match your project structure
using dotnetapp.Services;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetAllExpenses()
        {
            var expenses = _expenseService.GetAllExpenses();
            if (expenses == null || !expenses.Any())
            {
                return NoContent(); // HTTP 204
            }
            return Ok(expenses); // HTTP 200
        }

        [HttpGet("{expenseId}")]
        public ActionResult<Expense> GetExpenseById(int expenseId)
        {
            var expense = _expenseService.GetExpenseById(expenseId);
            if (expense == null)
            {
                return NotFound(); // HTTP 404
            }
            return Ok(expense); // HTTP 200
        }

        [HttpPost]
        public ActionResult<Expense> CreateExpense(Expense newExpense)
        {
            if (newExpense == null)
            {
                return BadRequest(); // HTTP 400
            }
            _expenseService.CreateExpense(newExpense);
            return CreatedAtAction(nameof(GetExpenseById), new { expenseId = newExpense.ExpenseId }, newExpense); // HTTP 201
        }

        [HttpPut("{expenseId}")]
        public ActionResult UpdateExpense(int expenseId, Expense updatedExpense)
        {
            var existingExpense = _expenseService.GetExpenseById(expenseId);
            if (existingExpense == null)
            {
                return NotFound(); // HTTP 404
            }
            _expenseService.UpdateExpense(expenseId, updatedExpense);
            return NoContent(); // HTTP 204
        }

        [HttpDelete("{expenseId}")]
        public ActionResult DeleteExpense(int expenseId)
        {
            var existingExpense = _expenseService.GetExpenseById(expenseId);
            if (existingExpense == null)
            {
                return NotFound(); // HTTP 404
            }
            _expenseService.DeleteExpense(expenseId);
            return NoContent(); // HTTP 204
        }
    }
}
https://github.com/Janani-iamneo/MobilePhone_Management_Swagger.git