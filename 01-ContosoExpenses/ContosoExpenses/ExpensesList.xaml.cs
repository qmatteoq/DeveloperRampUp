// ******************************************************************

// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.

// ******************************************************************

using System;
using System.Windows;
using System.Windows.Controls;
using ContosoExpenses.Models;
using ContosoExpenses.Services;

namespace ContosoExpenses
{
    /// <summary>
    /// Interaction logic for ExpensesList.xaml
    /// </summary>
    public partial class ExpensesList : Window
    {
        public int EmployeeId { get; set; }

        private Employee selectedEmployee;

        private DatabaseService db;

        public ExpensesList()
        {
            InitializeComponent();
            db = (Application.Current as App).DbService;
        }

        private void OnSelectedExpense(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var expense = e.AddedItems[0] as Expense;
                if (expense != null && expense.ExpenseId != 0)
                {
                    ExpenseDetail detail = new ExpenseDetail();
                    detail.SelectedExpense = expense;
                    detail.ShowDialog();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectedEmployee = db.GetEmployee(EmployeeId);

            txtEmployeeId.Text = selectedEmployee.EmployeeId.ToString();
            txtFullName.Text = $"{selectedEmployee.FirstName} {selectedEmployee.LastName}";
            txtEmail.Text = selectedEmployee.Email;
        }

        private void OnAddNewExpense(object sender, RoutedEventArgs e)
        {
            AddNewExpense newExpense = new AddNewExpense();
            newExpense.EmployeeId = EmployeeId;
            newExpense.ShowDialog();
        }

        public void LoadData()
        {
            ExpensesGrid.ItemsSource = db.GetExpenses(EmployeeId);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
