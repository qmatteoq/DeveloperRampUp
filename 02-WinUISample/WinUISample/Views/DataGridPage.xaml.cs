using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using WinUISample.ViewModels;

namespace WinUISample.Views
{
    // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on DataGridPage.xaml.
    // For more details see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid
    public sealed partial class DataGridPage : Page
    {
        public DataGridViewModel ViewModel { get; }

        public DataGridPage()
        {
            ViewModel = Ioc.Default.GetService<DataGridViewModel>();
            InitializeComponent();
        }

        private void OnShowTeachingTip(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            AutoSaveTip.IsOpen = true;

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            File.WriteAllText(Path.Combine(path, "MyDocument.txt"), "This is my document");
        }
    }
}
