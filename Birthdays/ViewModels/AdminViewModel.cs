using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Birthdays.ViewModels {
    public class AdminViewModel : INotifyPropertyChanged {
        string name;
        DateTime birthday;
        bool showButton;

        public AdminViewModel() {
            SaveCommand = new Command(async () => await Save(), () => !string.IsNullOrEmpty(Name));
            Today = Birthday = DateTime.Today;
            ShowButton = true;
        }

        public DateTime Today { get; }

        public string Name {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public DateTime Birthday {
            get { return birthday; }
            set {
                birthday = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public bool ShowButton {
            get { return showButton; }
            set { showButton = value; OnPropertyChanged(); }
        }

        public Command SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        async Task Save() {
            try {
                ShowButton = false;
                // TODO: Use a real service
                await Task.Delay(3000);
                ShowButton = true;
            } catch (Exception) {
                // TODO: Error handling
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
