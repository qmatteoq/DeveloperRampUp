using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;

namespace WinUISample.ViewModels
{
    public class MainViewModel : ObservableRecipient
    {
        public MainViewModel()
        {

            DoSomething = new RelayCommand(() =>
            {
            var dispatcher = DispatcherQueue.GetForCurrentThread();
            Task.Run(() =>
            {
                dispatcher.TryEnqueue(() =>
                {
                    Name = "Matteo";
                });
            });
            });
        }

        private string _name;

        public string Name
        {
            get {  return _name; }
            set {  SetProperty(ref _name, value); }
        }

        public RelayCommand DoSomething { get; set; }
    }
}
