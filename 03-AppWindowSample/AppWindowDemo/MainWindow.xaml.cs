using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AppWindowDemo
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow appWindow;
        private bool m_isBrandedTitleBar;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hWnd = new WindowInteropHelper(this).EnsureHandle();
            WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            appWindow = AppWindow.GetFromWindowId(myWndId);
        }

        private void OnSetTitleBar(object sender, RoutedEventArgs e)
        {
            appWindow.Title = "This is a custom title";
        }

        private void TitlebarBrandingBtn_Click(object sender, RoutedEventArgs e)
        {
            appWindow.TitleBar.ResetToDefault();

            m_isBrandedTitleBar = !m_isBrandedTitleBar;
            if (m_isBrandedTitleBar)
            {
                appWindow.Title = "Default titlebar with custom color customization";
                appWindow.TitleBar.ForegroundColor = Colors.White;
                appWindow.TitleBar.BackgroundColor = Colors.DarkOrange;
                appWindow.TitleBar.InactiveBackgroundColor = Colors.Blue;
                appWindow.TitleBar.InactiveForegroundColor = Colors.White;

                //Buttons
                appWindow.TitleBar.ButtonBackgroundColor = Colors.DarkOrange;
                appWindow.TitleBar.ButtonForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Blue;
                appWindow.TitleBar.ButtonInactiveForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonHoverBackgroundColor = Colors.Green;
                appWindow.TitleBar.ButtonHoverForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonPressedBackgroundColor = Colors.DarkOrange;
                appWindow.TitleBar.ButtonPressedForegroundColor = Colors.White;
            }
            else
            {
                appWindow.Title = "Default titlebar";
            }
            MyTitleBar.Visibility = Visibility.Collapsed;
        }

        private void ResetTitlebarBtn_Click(object sender, RoutedEventArgs e)
        {
            MyTitleBar.Visibility = Visibility.Collapsed;
            appWindow.TitleBar.ResetToDefault();
            appWindow.Title = "MainWindow";
        }

        private void TitlebarCustomBtn_Click(object sender, RoutedEventArgs e)
        {
            appWindow.TitleBar.ExtendsContentIntoTitleBar = !appWindow.TitleBar.ExtendsContentIntoTitleBar;

            if (appWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                // Show the custom titlebar
                MyTitleBar.Visibility = Visibility.Visible;

                // Set Button colors to match the custom titlebar
                appWindow.TitleBar.ButtonBackgroundColor = Colors.LightBlue;
                appWindow.TitleBar.ButtonForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.LightBlue;
                appWindow.TitleBar.ButtonInactiveForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonHoverBackgroundColor = Colors.Green;
                appWindow.TitleBar.ButtonHoverForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonPressedBackgroundColor = Colors.Green;
                appWindow.TitleBar.ButtonPressedForegroundColor = Colors.White;

                //Infer titlebar height
                int titleBarHeight = appWindow.TitleBar.Height;
                this.MyTitleBar.Height = titleBarHeight;
            }
            else
            {
                // Bring back the default titlebar
                MyTitleBar.Visibility = Visibility.Collapsed;
                appWindow.TitleBar.ResetToDefault();
            }
        }

        private void OnGoFullScreen(object sender, RoutedEventArgs e)
        {
            if (appWindow.Presenter.Kind is not AppWindowPresenterKind.FullScreen)
            {
                appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            }
            else
            {
                appWindow.SetPresenter(AppWindowPresenterKind.Default);
            }
        }

        private void OnGoCompactOverlay(object sender, RoutedEventArgs e)
        {
            if (appWindow.Presenter.Kind is not AppWindowPresenterKind.CompactOverlay)
            {
                appWindow.SetPresenter(AppWindowPresenterKind.CompactOverlay);
            }
            else
            {
                appWindow.SetPresenter(AppWindowPresenterKind.Default);
            }
        }
    }
}
