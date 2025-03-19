using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/expenses")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private static List<Expense> Expenses = new List<Expense>();

    [HttpPost]
    public IActionResult AddExpense([FromBody] Expense expense)
    {
        expense.Id = Expenses.Count + 1;
        Expenses.Add(expense);
        return Ok(expense);
    }

    [HttpGet("{groupId}")]
    public IActionResult GetExpenses(int groupId)
    {
        var groupExpenses = Expenses.Where(e => e.GroupId == groupId).ToList();
        return Ok(groupExpenses);
    }

    [HttpGet("balances/{groupId}")]
    public IActionResult GetBalances(int groupId)
    {
        var balances = new Dictionary<int, decimal>();
        foreach (var expense in Expenses.Where(e => e.GroupId == groupId))
        {
            foreach (var share in expense.Splits)
            {
                if (!balances.ContainsKey(share.UserId))
                    balances[share.UserId] = 0;

                balances[share.UserId] -= share.Amount;
            }

            if (!balances.ContainsKey(expense.PaidBy))
                balances[expense.PaidBy] = 0;

            balances[expense.PaidBy] += expense.Amount;
        }
        return Ok(balances);
    }
}