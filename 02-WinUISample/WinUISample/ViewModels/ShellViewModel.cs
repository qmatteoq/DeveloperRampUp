using System;

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

using WinUISample.Contracts.Services;
using WinUISample.Views;

namespace WinUISample.ViewModels
{
    public class ShellViewModel : ObservableRecipient
    {
        private bool _isBackEnabled;
        private object _selected;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IConfiguration _configuration;

        public INavigationService NavigationService { get; }

        public INavigationViewService NavigationViewService { get; }

        public bool IsBackEnabled
        {
            get { return _isBackEnabled; }
            set { SetProperty(ref _isBackEnabled, value); }
        }

        public object Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, IThemeSelectorService themeSelectorService, IConfiguration configuration)
        {
            NavigationService = navigationService;
            NavigationService.Navigated += OnNavigated;
            NavigationViewService = navigationViewService;
            _themeSelectorService = themeSelectorService;
            _configuration = configuration;
        }

        private async void OnNavigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;
            if (e.SourcePageType == typeof(SettingsPage))
            {
                Selected = NavigationViewService.SettingsItem;
                return;
            }

            var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
            if (selectedItem != null)
            {
                Selected = selectedItem;
            }

            var appConfigurationBuilder = new ConfigurationBuilder().AddAzureAppConfiguration(_configuration["AppConfig"]);
            var appConfiguration = appConfigurationBuilder.Build();

            var setting = appConfiguration["WinUIApp:Settings:Theme"].ToString();

            var theme = (setting == "Light") ? ElementTheme.Light : ElementTheme.Dark;
            await _themeSelectorService.SetThemeAsync(theme);
        }
    }
}
