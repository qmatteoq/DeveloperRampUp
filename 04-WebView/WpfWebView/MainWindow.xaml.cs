using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;
using Windows.UI.Notifications;

namespace WpfWebView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private async void CoreWebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string tag = "report-progress";
            string group = "downloads";

            // Construct the toast content with data bound fields
            new ToastContentBuilder()
                .AddText("Report generation in progress...")
                .AddVisualChild(new AdaptiveProgressBar()
                {
                    Title = "Report generation",
                    Value = new BindableProgressBarValue("progressValue"),
                    ValueStringOverride = new BindableString("progressValueString"),
                    Status = "Generating report..."
                })
                .Show(toast =>
                {
                    toast.Data = new NotificationData();
                    toast.Data.Values["progressValue"] = "0.0";
                    toast.Data.Values["progressValueString"] = "0 %";

                    toast.Data.SequenceNumber = 1;

                    toast.Tag = tag;
                    //toast.Group = group;
                });

            for (uint cont = 0; cont <= 10; cont++)
            {
                // Construct a NotificationData object;
                await Task.Delay(1000);

                // Create NotificationData and make sure the sequence number is incremented
                // since last update, or assign 0 for updating regardless of order
                var data = new NotificationData
                {
                    SequenceNumber = cont
                };

                // Assign new values
                // Note that you only need to assign values that changed. In this example
                // we don't assign progressStatus since we don't need to change it
                IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");

                double progressValue = (double)cont / 10;
                string progressValueConverted = $"{(progressValue * 100).ToString(provider)} %";

                data.Values["progressValue"] = progressValue.ToString(provider);
                data.Values["progressValueString"] = progressValueConverted;

                // Update the existing notification's data by using tag/group
                ToastNotificationManagerCompat.CreateToastNotifier().Update(data, tag);
            }

            new ToastContentBuilder()
                .AddText("Report generation completed")
                .AddText("The report has been generated successfully!")
                .Show(toast =>
                {
                    toast.Tag = tag;
                    //toast.Group = group;
                });
        }
    }
}
