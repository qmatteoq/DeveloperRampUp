using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using WinUISample.ViewModels;

namespace WinUISample.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            ViewModel = Ioc.Default.GetService<MainViewModel>();
            InitializeComponent();
        }

        private async void OnUseDispatcher(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {


            //do some heavy work
            string result = await DispatcherQueue.EnqueueAsync<string>(() =>
            {
                return "Hello world";
            });

            var output = $"Result: {result}";


        }
    }
}
