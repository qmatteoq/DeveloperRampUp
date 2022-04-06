using ContosoExpenses.Services;
using System.Windows;

namespace ContosoExpenses
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public DatabaseService DbService { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbService = new DatabaseService();
        }
    }
}
