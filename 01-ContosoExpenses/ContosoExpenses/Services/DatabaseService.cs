using System.Collections.Generic;
using System.Linq;
using Bogus;
using LiteDB;
using ContosoExpenses.Models;

namespace ContosoExpenses.Services
{
    public class DatabaseService
    {
        readonly int numberOfEmployees = 10;
        readonly int numberOfExpenses = 5;

        private List<Employee> employees;

        public Employee GetEmployee(int employeeId)
        {
            return employees.FirstOrDefault(x => x.EmployeeId == employeeId);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public List<Expense> GetExpenses(int employeedId)
        {
            return employees.FirstOrDefault(x => x.EmployeeId == employeedId).Expenses;
        }

        public void SaveExpense(Expense expense, int employeeId)
        {
            employees.FirstOrDefault(x => x.EmployeeId == employeeId).Expenses.Add(expense);
        }

        public void InitializeDatabase()
        {
            employees = new List<Employee>();
            GenerateFakeData(numberOfEmployees, numberOfExpenses);
        }

        public void InitializeDatabase(int numEmployees)
        {
            employees = new List<Employee>();
            GenerateFakeData(numEmployees, numberOfExpenses);
        }

        private void GenerateFakeData(int numberOfEmployees, int numberOfExpenses)
        {
            for (int cont = 0; cont < numberOfEmployees; cont++)
            {
                var employee = new Faker<Employee>()
                    .RuleFor(x => x.FirstName, (f, u) => f.Name.FirstName())
                    .RuleFor(x => x.LastName, (f, u) => f.Name.LastName())
                    .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName, "contoso.com"))
                    .Generate();

                employee.EmployeeId = cont + 1;
                employee.Expenses = new List<Expense>();

                for (int contExpenses = 0; contExpenses < numberOfExpenses; contExpenses++)
                {   
                    var expense = new Faker<Expense>()
                   .RuleFor(x => x.Description, (f, u) => f.Commerce.ProductName())
                   .RuleFor(x => x.Type, (f, u) => f.Finance.TransactionType())
                   .RuleFor(x => x.Cost, (f, u) => (double)f.Finance.Amount())
                   .RuleFor(x => x.Address, (f, u) => f.Address.FullAddress())
                   .RuleFor(x => x.City, (f, u) => f.Address.City())
                   .RuleFor(x => x.Date, (f, u) => f.Date.Past())
                   .Generate();

                    employee.Expenses.Add(expense);
                }

                employees.Add(employee);
            }
        }
    }
}
