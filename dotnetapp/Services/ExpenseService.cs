// Services/ExpenseService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models; // Adjust namespace to match your project structure

namespace dotnetapp.Services
{
    public class ExpenseService
    {
        private readonly List<Expense> _expenses;

        public ExpenseService()
        {
            _expenses = new List<Expense>
            {
                new Expense { ExpenseId = 1, Description = "Lunch", Amount = 15.00m, Date = DateTime.Now.AddDays(-7), Category = "Food" },
                new Expense { ExpenseId = 2, Description = "Groceries", Amount = 50.00m, Date = DateTime.Now.AddDays(-14), Category = "Food" },
                new Expense { ExpenseId = 3, Description = "Bus Ticket", Amount = 2.50m, Date = DateTime.Now.AddDays(-21), Category = "Transport" }
            };
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _expenses;
        }

        public Expense GetExpenseById(int expenseId)
        {
            return _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
        }

        public void CreateExpense(Expense newExpense)
        {
            newExpense.ExpenseId = _expenses.Count > 0 ? _expenses.Max(e => e.ExpenseId) + 1 : 1;
            _expenses.Add(newExpense);
        }

        public void UpdateExpense(int expenseId, Expense updatedExpense)
        {
            var existingExpense = _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
            if (existingExpense != null)
            {
                existingExpense.Description = updatedExpense.Description;
                existingExpense.Amount = updatedExpense.Amount;
                existingExpense.Date = updatedExpense.Date;
                existingExpense.Category = updatedExpense.Category;
            }
        }

        public void DeleteExpense(int expenseId)
        {
            var existingExpense = _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
            if (existingExpense != null)
            {
                _expenses.Remove(existingExpense);
            }
        }
    }
}
