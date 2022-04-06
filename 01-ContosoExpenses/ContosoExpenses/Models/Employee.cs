
using System.Collections.Generic;

namespace ContosoExpenses.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
