# xamarin-forms-birthday
Never miss a birthday again! This educational app was created as part of a Xamarin Forms course.

## Show mock-up of the app to be created

<img src="mockup.png" height="677" width="376" />

### Key takeaway

Always mock-up your app before starting to code to avoid unecessary coding.

## Create new solution

Use `Blank Forms App` template. Update packages, build and run, it should work.

### Key takeaway

Know the difference between the different projects and understand how this can run on your phone.

## Navigation and Tab bar

Delete the `MainPage`.

Create a new folder `Views`.

Add a new `MainPage` with XAML, inheriting from `ContenPage`. Change the inheritance to `TabbedPage`. The XAML should look like this:

```XAML
<?xml version="1.0" encoding="UTF-8"?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:views="clr-namespace:Birthdays.Views" x:Class="Birthdays.Views.MainPage">
    <TabbedPage.Children>
        <NavigationPage Title="Birthdays">
            <x:Arguments>
                <views:BirthdaysPage />
            </x:Arguments>
        </NavigationPage>
        <NavigationPage Title="Settings">
            <x:Arguments>
                <views:SettingsPage />
            </x:Arguments>
        </NavigationPage>
    </TabbedPage.Children>
</TabbedPage>
```

Three problems now: 

1. MainPage has a new namespace
2. `BirthdaysPage` doesn't exist
3. `SettingsPage`doesn't exist

To fix this: 

1. Add `using Birthdays.Views;` to `App.xaml.cs`.
2. Create a `ContentPage` with XAML called `BirthdaysPage`.
3. Create a `ContentPage` with XAML called `SettingsPage`.

Build the app and run it.

### Key takeaway

Creating a skeleton of the app first makes it easier to add inn details later. XAML is used to describe the user interface in Xamarin Forms.

## Add titles to the new pages

Set the property `Title` of the two new `ContentPages`.

### Key takeaway

Navigation bars inherit the title of their active child. Each tab has its own navigation stack.

## Create a Grid for the Birthdays page

Create a simple grid consisting of two eqaul vertical parts like this:

```XAML
<ContentPage.Content>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <ContentView BackgroundColor="Red" />
        <ContentView Grid.Row="1" BackgroundColor="Green" />
    </Grid>
</ContentPage.Content>
```

### Key takeaway

Grid is an excellent way to lay out a small number of items on the screen. Verify your design using simple techniques before proceeding with details.

## Create view for closest birthday

Create a `ContentView` with XAML which will contain the closest birthday, `ClosestBirthdayView`:

```XAML
<ContentView.Content>
    <StackLayout>
        <Label HorizontalTextAlignment="Center" FontSize="24" VerticalOptions="Start" Margin="0,30,0,0" Text="Birthday Boi" />
        <Label HorizontalTextAlignment="Center" FontSize="32" VerticalOptions="CenterAndExpand" Text="35 years" />
        <Label HorizontalTextAlignment="Center" FontSize="24" VerticalOptions="End" Margin="0,0,0,30" Text="6.2.1984" />
    </StackLayout>
</ContentView.Content>
```

```XAML
xmlns:views="clr-namespace:Birthdays.Views"
...
<views:ClosestBirthdayView />
```

### Key takeway

Create small components to build up your UI. These must be referenced in XAML using the correct namespace. Know and utilize different layout options.

## Create all birthdays view

Create a `ContentView` with XAML which will show a list of all birthdays, `BirthdaysView`:

```XAML
<ContentView xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Birthdays.Views.BirthdaysView">
    <ContentView.Content>
        <ListView>
            <ListView.ItemsSource>
                <x:Array Type="{x:Type x:String}">
                    <x:String>Paul</x:String>
                    <x:String>Leto</x:String>
                    <x:String>Vladimir</x:String>
                    <x:String>Jessica</x:String>
                    <x:String>Duncan</x:String>
                </x:Array>
            </ListView.ItemsSource>
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding}" Detail="Bursdag" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentView.Content>
</ContentView>
```

### Key takeaway

A `ListView` is useful to show lists. `DataTemplates` defines the UI of items in lists or tables. `Bindings` sets vales in the UI based on other data.

## A short stylistic detour

Replace `Application.Resources` in `App.xaml` with:

```XAML
<Application.Resources>
    <ResourceDictionary>
        <Style TargetType="NavigationPage">
            <Setter Property="BarBackgroundColor" Value="#F9F9F9" />
        </Style>
    </ResourceDictionary>
</Application.Resources>
```

This will make the navigation bar be less ugly, at least on iOS.

### Key takeaway

Global styles are necessary and can be specified in `App.xaml`.

## Use MVVM and DataBinding to populate the UI

Add a `Models` folder and the class `Person`:

```csharp
using System;

namespace Birthdays.Models {
    public class Person {
        public Person(string name, DateTime date) {
            Name = name;
            Birthday = date.ToShortDateString();
            Age = GetAge(date);
        }

        public string Name { get; }
        public string Birthday { get; }
        public uint Age { get; }

        static uint GetAge(DateTime birthDate) {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) {
                age--;
            }

            return (uint)age;
        }
    }
}

```

Add a `ViewModels` folder and the class `BirthdaysViewModel`:

```csharp
using System;
using Birthdays.Models;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        public BirthdaysViewModel() {
            ClosestBirthDay = new Person("Birthday Boi", new DateTime(1984, 2, 6));
        }

        public Person ClosestBirthDay { get; }
    }
}
```

Update `ClosestBirthdayView` to bind to a people object:

```xaml
<Label Text="{Binding ClosestBirthDay.Name}" ...
<Label Text="{Binding ClosestBirthDay.Age}" ...
<Label Text="{Binding ClosestBirthDay.Birthday}" ...
```

And set the `BindingContext` of `BirthdaysPage.xaml.cs`:

```csharp
using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class BirthdaysPage : ContentPage {
        public BirthdaysPage() {
            InitializeComponent();
            BindingContext = new BirthdaysViewModel();
        }
    }
}
```

Run the app and success means it looks *kind-of the same*!

### Key takeaway

**MVVM** and **data binding** are the two most important aspects of any Xamarin Forms app. They enable decoupling, easy state managment and makes the app testable! If you remember only two things, remember these patterns.

## Bind the list too

Add an `ObserveableCollection` to `BirthdaysViewModel` and populate it with dummy data:

```csharp
using System;
using System.Collections.ObjectModel;
using Birthdays.Models;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        public BirthdaysViewModel() {
            ClosestBirthDay = new Person("Birthday Boi", new DateTime(1984, 2, 6));
            FutureBirthdays = new ObservableCollection<Person> {
                new Person("Paul", new DateTime(1983, 3, 7)),
                new Person("Leto", new DateTime(1982, 4, 8)),
                new Person("Vladimir", new DateTime(1981, 5, 9)),
                new Person("Jessica", new DateTime(1980, 6, 10)),
                new Person("Duncan", new DateTime(1979, 7, 11))
            };
        }

        public Person ClosestBirthDay { get; }
        public ObservableCollection<Person> FutureBirthdays { get; }
    }
}
```

Update the `BirthdaysView` to use bindings fitting the ViewModel:

```xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentView xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Birthdays.Views.BirthdaysView">
    <ContentView.Content>
        <ListView ItemsSource="{Binding FutureBirthdays}">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding Name}" Detail="{Binding Birthday}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentView.Content>
</ContentView>
```

### Key takeaway

**MVVM** and **data binding** are the two most important aspects of any Xamarin Forms app. They enable decoupling, easy state managment and makes the app testable! If you remember only two things, remember these patterns.

## Using a service to populate the app

Add the `NuGet-package` `Newtonsoft.Json` to the `Birthdays` project.

Create a folder called `Services` and add the class `BirthdayService`:

```csharp
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService {
        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri("http://178.62.18.75:8081")
            }) {
                var json = await httpClient.GetStringAsync("api/birthday/bodø");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }
    }
}
```

Since the service doesn't use HTTPS, iOS at least will block the connection. We need to enable unsecure communication to this service. Open `Info.plist` from the `Birthdays.iOS` project and add the following:

```XML
<dict>
    <key>NSExceptionDomains</key>
    <dict>
        <key>178.62.18.75</key>
        <dict>
            <key>NSIncludesSubdomains</key>
            <true/>
            <key>NSExceptionAllowsInsecureHTTPLoads</key>
            <true/>
            <key>NSExceptionMinimumTLSVersion</key>
            <string>TLSv1.2</string>
        </dict>
    </dict>
</dict>
```

Use the service in `BirthdaysViewModel`:

```csharp
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel {
        readonly BirthdayService birthdayService;

        public BirthdaysViewModel() {
            birthdayService = new BirthdayService();
        }

        public Person ClosestBirthDay { get; private set; }
        public ObservableCollection<Person> FutureBirthdays { get; private set; }


        public async Task FetchBirthdays() {
            try {
                var birthdays = await birthdayService.GetBirthdays();
                ClosestBirthDay = birthdays[0];
                FutureBirthdays = new ObservableCollection<Person>(birthdays.Skip(1));
            } catch (Exception) {
                // TODO: Do some error handling
            }
        }
    }
}
```

And to fetch birthdays every time `BirthdaysPage` comes into view, change `BirthdaysPage.xaml.cs` to:

```csharp
using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class BirthdaysPage : ContentPage {
        readonly BirthdaysViewModel birthdaysViewModel;

        public BirthdaysPage() {
            InitializeComponent();
            BindingContext = birthdaysViewModel = new BirthdaysViewModel();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            await birthdaysViewModel.FetchBirthdays();
        }
    }
}
```

And no data should be shown on screen. One piece is cleary missing from the data binding puzzle.

### Key takeaway

`JSON` and `HTTP` is the universal language of online services. `Postman` can be used to explore APIs. `HttpClient` the C# way of making HTTP-request, `Newtonsoft.Json` is the most used JSON-parser. `async` and `await` is the best thing since slices bread. `async void` is an anti-pattern and usage should be kept to a minimum. And sometimes you need to tweak your model to incorporate the real world.

## Proper data binding

Implement `INotifyPropertyChanged` in the `BirthdaysViewModel`: 

```csharp
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Birthdays.ViewModels {
    public class BirthdaysViewModel : INotifyPropertyChanged {
        readonly BirthdayService birthdayService;

        public BirthdaysViewModel() {
            birthdayService = new BirthdayService();
        }

        public Person ClosestBirthDay { get; private set; }
        public ObservableCollection<Person> FutureBirthdays { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task FetchBirthdays() {
            try {
                var birthdays = await birthdayService.GetBirthdays();
                ClosestBirthDay = birthdays[0];
                FutureBirthdays = new ObservableCollection<Person>(birthdays.Skip(1));
                OnPropertyChanged(nameof(ClosestBirthDay));
                OnPropertyChanged(nameof(FutureBirthdays));
            } catch (Exception) {
                // TODO: Do some error handling
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Run the app and everything should work!

### Key takeaway

`INotifyPropertyChanged` is your new favourite interface. It binds the MVVM world together and is your bridge from ViewModel to View. Understand how this interface contains the event which makes the UI ask the ViewModel for new information!

## Create a screen to add birthdays

Create `ValueConverters` folder in the `Views` folder, and add a class `NegateBooleanConverter`: 

```csharp
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Birthdays.Views.ValueConverters {
    public class NegateBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;
    }
}
```

Change `SettingsPage` to:

```xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:valueConverters="clr-namespace:Birthdays.Views.ValueConverters" x:Class="Birthdays.Views.SettingsPage" Title="Settings">
    <ContentPage.Resources>
        <ResourceDictionary>
            <valueConverters:NegateBooleanConverter x:Key="negateBooleanConverter" />
        </ResourceDictionary>
    </ContentPage.Resources>
    <ContentPage.Content>
        <StackLayout VerticalOptions="Center">
            <Label Text="Add new birthday" FontSize="32" FontAttributes="Bold" Margin="15,30,15,15" />
            <Grid Margin="15,0,15,0">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="Auto" />
                    <RowDefinition Height="Auto" />
                </Grid.RowDefinitions>
                <Label Text="Name" VerticalOptions="Center" HorizontalOptions="End" />
                <Entry Grid.Column="1" Placeholder="Name" Text="{Binding Name}" VerticalOptions="Center" />
                <Label Grid.Row="1" Text="Birthday" VerticalOptions="Center" HorizontalOptions="End" />
                <DatePicker Grid.Row="1" Grid.Column="1" MinimumDate="01/01/1900" MaximumDate="{Binding Today}" Date="{Binding Birthday}" VerticalOptions="Center" />
            </Grid>
            <ActivityIndicator HeightRequest="50" IsRunning="true" IsVisible="{Binding ShowButton, Converter={StaticResource negateBooleanConverter}}}" />
            <Button HeightRequest="50" Text="Save" Command="{Binding SaveCommand}" IsVisible="{Binding ShowButton}" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

### Key takeaway

Different layout options can be combined to get the desired view. Use a `ValueConverter` to change how a value is interpreted in XAML.

## Bind the settings view

Create a new ViewModel, `AdminViewModel`:

```csharp
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
```

And bind the view, creating the view model in `SettingsPage.xaml.cs`:

```csharp
using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {
            InitializeComponent();
            BindingContext = new AdminViewModel();
        }
    }
}
```

### Key takeaway

Use commands to bind actions to the view. All commands using external sources must be async. Give visual feedback both during progress and on completion. Disable or hide controls that should not be used at a given time.

## Use a real service

Check Swagger for the required format and try it using Postman before implementing the API.

The update the `BirthdayService`, it should look like this now:

```csharp
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService {
        const string BaseAddress = "http://178.62.18.75:8081";

        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri(BaseAddress)
            }) {
                var json = await httpClient.GetStringAsync("api/birthday/bodø/10");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        public async Task SaveBirthday(Person person) {
            using (var httpClient = new HttpClient {
                BaseAddress = new Uri(BaseAddress)
            }) {
                var personWithLocation = new PersonWithLocation(person);
                var json = JsonConvert.SerializeObject(personWithLocation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("api/birthday", content);
                result.EnsureSuccessStatusCode();
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }

        class PersonWithLocation {
            public PersonWithLocation(Person person) {
                Name = person.Name;
                Date = DateTime.Parse(person.Birthday);
            }

            public string Name { get; }
            public DateTime Date { get; }
            public string Location { get; } = "bodø";
        }
    }
}
```

Then use the service in the view model:

```csharp
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using Xamarin.Forms;

namespace Birthdays.ViewModels {
    public class AdminViewModel : INotifyPropertyChanged {
        readonly BirthdayService birthdayService;

        string name;
        DateTime birthday;
        bool showButton;

        public AdminViewModel() {
            birthdayService = new BirthdayService();
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
                var person = new Person(Name, Birthday);
                await birthdayService.SaveBirthday(person);
                Name = "";
                Birthday = DateTime.Today;
            } catch (Exception e) {
                // TODO: Error handling
            } finally {
                ShowButton = true;
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

You can now create as many birthdays as you wish.

### Key takeaway

Your app's model should be perfect for your need, but again, sometimes you need to tweak the service layer to fit a reality that you have no control over.

## Support deleting birthdays

Expand `Person` to contain the `Id` property needed to support deletion:

```csharp
using System;

namespace Birthdays.Models {
    public class Person {
        public Person(string name, DateTime date, int id) {
            Name = name;
            Birthday = date.ToShortDateString();
            Age = GetAge(date);
            Id = id;
        }

        public string Name { get; }
        public string Birthday { get; }
        public uint Age { get; }
        public int Id { get; }

        static uint GetAge(DateTime birthDate) {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) {
                age--;
            }

            return (uint)age;
        }
    }
}
```

This will break the app as the save functionality knows nothing of Ids. Create a new model class to better support saving and call it the confusing name `Birthday`:

```csharp
using System;

namespace Birthdays.Models {
    public class Birthday {
        public Birthday(string name, DateTime birthdate) {
            Name = name;
            Birthdate = birthdate;
        }

        public string Name { get; }
        public DateTime Birthdate { get; }
    }
}
```

Update `AdminViewModel` to use `Birthday` instead of Person in the `Save` method.

Utilize `Birthday` in the `BirthdayService` and add support for the deletion operation:

```csharp
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService {
        const string BaseAddress = "http://178.62.18.75:8081";
        const string BirthdayAPI = "api/birthday";
        const string DefaultLocation = "bodø";

        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                const int MaxResults = 10;
                var json = await httpClient.GetStringAsync($"{BirthdayAPI}/{DefaultLocation}/{MaxResults}");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        public async Task SaveBirthday(Birthday birthday) {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                var personWithLocation = new BirthdayWithLocation(birthday);
                var json = JsonConvert.SerializeObject(personWithLocation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(BirthdayAPI, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteBirthday(Person person) {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                await httpClient.DeleteAsync($"{BirthdayAPI}/{person.Id}");
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }

        class BirthdayWithLocation {
            readonly Birthday birthday;

            public BirthdayWithLocation(Birthday birthday) {
                this.birthday = birthday;
            }

            public string Name { get { return birthday.Name; } }
            public string Date { get { return birthday.Birthdate.ToShortDateString(); } }
            public string Location { get { return DefaultLocation; } }
        }

        static class HttpClientFactory {
            public static HttpClient CreateClient()
                => new HttpClient {
                    BaseAddress = new Uri(BaseAddress)
                };
        }
    }
}
```

To add support for deletion in `BirthdaysViewModel`, add the followwing method:

```csharp
public async Task RemoveBirthday(Person person) {
    try {
        FutureBirthdays.Remove(person);
        await birthdayService.DeleteBirthday(person);
    } catch (Exception) {
        // TODO: Do some error handling
    }
}
```

Add support for delete context action in `BirthdaysView`, aka swipe to delete on iOS:

```xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentView xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Birthdays.Views.BirthdaysView">
    <ContentView.Content>
        <ListView ItemsSource="{Binding FutureBirthdays}">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding Name}" Detail="{Binding Birthday}">
                        <TextCell.ContextActions>
                            <MenuItem Clicked="OnDelete" CommandParameter="{Binding .}" Text="Delete" IsDestructive="True" />
                        </TextCell.ContextActions>
                    </TextCell>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentView.Content>
</ContentView>
```

Finally, since this functionality in Xamarin Forms has no binding support out of the box, we need to manually notify the view model of the wanted delition. `BirthdaysView.xaml.cs` thus looks like this:

```csharp
using System;
using Birthdays.Models;
using Birthdays.ViewModels;
using Xamarin.Forms;

namespace Birthdays.Views {
    public partial class BirthdaysView : ContentView {
        public BirthdaysView() {
            InitializeComponent();
        }

        public async void OnDelete(object sender, EventArgs e) {
            var menuItem = (MenuItem)sender;
            var person = (Person)menuItem.CommandParameter;
            var birthdaysViewModel = (BirthdaysViewModel)BindingContext;
            await birthdaysViewModel.RemoveBirthday(person);
        }
    }
}
```

Phew.

### Key takeaway

A design is good if existing code needs small to no changes in order to add new functionality. If you repeat yourself too much, or the code doesn't express its intent clearly, take a breather to refactor. Refactoring prevents code-rot.

## Adding unit tests

Create a new `xUnit Test Project` named `Tests`.

Update the nuget packages to the latest versions.

Add a reference to the .Net Standard Library of the app, the `Birthdays` project.

Person contains a cryptic `GetAge` method, so this class is a good first case. Rename `UnitTest1` to `PersonTests`, and add a test:

```csharp
using System;
using Birthdays.Models;
using Xunit;

namespace Tests {
    public class PersonTests {
        [Fact]
        public void VerifyPerson() {
            var birthdate = new DateTime(1983, 9, 8);

            var runar = new Person("Runar", birthdate, 0);

            Assert.Equal("Runar", runar.Name);
            Assert.Equal(birthdate, DateTime.Parse(runar.Birthday));
            Assert.Equal((uint)35, runar.Age);
            Assert.Equal(0, runar.Id);
        }
    }
}
```

This should run green and increase our confidence that our code is correct.

But the test has one obvious flaw. What happens on 8.9.2019? We've create the yearly failed test. Time needs to be considered an external dependency and treated as such.

We need to create our own `DateTime` static class to control time. From the test, we must change what *Now* means.

Add the following classes, `DateTimeProvider` and `DateTimeProviderContext`, to a `Helpers` folder in the `Birthdays` project:

```csharp
using System;
namespace Birthdays.Helpers {
    public class DateTimeProvider {
        static Lazy<DateTimeProvider> instance = new Lazy<DateTimeProvider>(() => new DateTimeProvider());

        DateTimeProvider() {
        }

        public static DateTimeProvider Instance {
            get { return instance.Value; }
        }

        Func<DateTime> _defaultCurrentFunction = () => DateTime.UtcNow;

        public DateTime Now {
            get {
                return DateTimeProviderContext.Current == null
                     ? _defaultCurrentFunction.Invoke()
                     : DateTimeProviderContext.Current.ContextDateTimeNow;
            }
        }
    }
}
```

```csharp
using System;
using System.Collections;
using System.Threading;

namespace Birthdays.Helpers {
    public class DateTimeProviderContext : IDisposable {
        readonly Stack contextStack = new Stack();

        static ThreadLocal<Stack> ThreadScopeStack = new ThreadLocal<Stack>(() => new Stack());

        public DateTime ContextDateTimeNow;

        public DateTimeProviderContext(DateTime contextDateTimeNow) {
            ContextDateTimeNow = contextDateTimeNow;
            ThreadScopeStack.Value.Push(this);
        }

        public static DateTimeProviderContext Current {
            get {
                if (ThreadScopeStack.Value.Count == 0) {
                    return null;
                }

                return (DateTimeProviderContext)ThreadScopeStack.Value.Peek();
            }
        }

        public void Dispose() => ThreadScopeStack.Value.Pop();
    }
}
```

Use it in `Person`:

```csharp
...
var now = DateTimeProvider.Instance.Now;
...
```

And in the tests:

```csharp
using System;
using Birthdays.Helpers;
using Birthdays.Models;
using Xunit;

namespace Tests {
    public class PersonTests {
        [Fact]
        public void VerifyPerson() {
            var currentDate = new DateTime(2019, 2, 6);
            using (var context1 = new DateTimeProviderContext(currentDate)) {
                var birthdate = new DateTime(1983, 9, 8);

                var runar = new Person("Runar", birthdate, 0);

                Assert.Equal("Runar", runar.Name);
                Assert.Equal(birthdate, DateTime.Parse(runar.Birthday));
                Assert.Equal((uint)35, runar.Age);
                Assert.Equal(0, runar.Id);
            }
        }
    }
}
```

### Key takeaway

Write unit test for the parts of your app that you need to work. Control for all external factors to make your tests solid. If you write tests after the production code, always verify that the tests will fail for the right reasons.

## Testing a view model

Create `BirthdaysViewModelTests`:

```csharp
using System.Threading.Tasks;
using Birthdays.ViewModels;
using Xunit;

namespace Tests {
    public class BirthdaysViewModelTests {
        [Fact]
        public async Task FetchBirthdays() {
            var birthdayViewModel = new BirthdaysViewModel();

            await birthdayViewModel.FetchBirthdays();

            Assert.NotNull(birthdayViewModel.ClosestBirthDay);
            Assert.True(birthdayViewModel.FutureBirthdays.Count > 0);
        }
    }
}
```

Verify that the test works.

This is a good start, but it does not check the property changed notifications are fired. Let's fix that. Create a `Helper` folder in the test project and add the class `PropertyChangedTracker`.

```csharp
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace Tests.Helpers {
    public class PropertyChangeTracker : IDisposable {
        readonly INotifyPropertyChanged changer;
        readonly List<string> notifications;

        public PropertyChangeTracker(INotifyPropertyChanged changer) {
            this.changer = changer;
            notifications = new List<string>();
            changer.PropertyChanged += Changer_PropertyChanged;
        }

        void Changer_PropertyChanged(object sender, PropertyChangedEventArgs e)
            => notifications.Add(e.PropertyName);

        public void Dispose()
            => changer.PropertyChanged -= Changer_PropertyChanged;

        public void VerifyNumberOfNotifications(int expected)
            => Assert.Equal(expected, notifications.Count);

        public void VerifyNotificationOfName(string propertyName, int times = 1)
            => Assert.Equal(times, notifications.Count(p => propertyName == p));
    }
}
```

Use it in the test:

```csharp
using System.Threading.Tasks;
using Birthdays.ViewModels;
using Tests.Helpers;
using Xunit;

namespace Tests {
    public class BirthdaysViewModelTests {
        [Fact]
        public async Task FetchBirthdays() {
            var birthdayViewModel = new BirthdaysViewModel();
            using (var propertyChangedTracker = new PropertyChangeTracker(birthdayViewModel)) {
                await birthdayViewModel.FetchBirthdays();

                Assert.NotNull(birthdayViewModel.ClosestBirthDay);
                Assert.True(birthdayViewModel.FutureBirthdays.Count > 0);
                propertyChangedTracker.VerifyNumberOfNotifications(2);
                propertyChangedTracker.VerifyNotificationOfName("ClosestBirthDay");
                propertyChangedTracker.VerifyNotificationOfName("FutureBirthdays");
            }
        }
    }
}
```

### Key takeaway

MVVM is a really testable pattern and unit tests can help you verify even your UI.

## Dependency Injection

This naive test still has one fatal flaw: we use the production service in our tests! To remedy this, we can create a simple mock and avoiding the network call altogether.

Start with creating an interface for the service, `IBirthdayService`:

```csharp
using System.Threading.Tasks;
using Birthdays.Models;

namespace Birthdays.Services {
    public interface IBirthdayService {
        Task<Person[]> GetBirthdays();
        Task SaveBirthday(Birthday birthday);
        Task DeleteBirthday(Person person);
    }
}
```

`BirthdayService` needs to implement it:

```csharp
...
public class BirthdayService : IBirthdayService {
...
```

Use depency injection to inject `BirthdayService` using the constructor in `BirthdaysViewModel`: 

```csharp
...
readonly IBirthdayService birthdayService;

public BirthdaysViewModel(IBirthdayService birthdayService) {
    this.birthdayService = birthdayService;
}
...
```

Update `BirthdaysPage.xaml.cs` to create `BirthdayService`:

```csharp
...
BindingContext = birthdaysViewModel = new BirthdaysViewModel(new BirthdayService());
...
```

Create the stub and do the same in the tests:

```csharp
using System;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using Birthdays.ViewModels;
using Tests.Helpers;
using Xunit;

namespace Tests {
    public class BirthdaysViewModelTests {
        [Fact]
        public async Task FetchBirthdays() {
            var birthdayViewModel = new BirthdaysViewModel(new BirthdayServiceFake());
            using (var propertyChangedTracker = new PropertyChangeTracker(birthdayViewModel)) {
                await birthdayViewModel.FetchBirthdays();

                Assert.NotNull(birthdayViewModel.ClosestBirthDay);
                Assert.True(birthdayViewModel.FutureBirthdays.Count > 0);
                propertyChangedTracker.VerifyNumberOfNotifications(2);
                propertyChangedTracker.VerifyNotificationOfName("ClosestBirthDay");
                propertyChangedTracker.VerifyNotificationOfName("FutureBirthdays");
            }
        }

        class BirthdayServiceFake : IBirthdayService {
            public Task DeleteBirthday(Person person) {
                throw new System.NotImplementedException();
            }

            public Task<Person[]> GetBirthdays() {
                return Task.FromResult(new[] {
                    new Person("Runar", new DateTime(1983, 9, 8), 1),
                    new Person("Anders", new DateTime(1960, 12, 24), 2)
                });
            }

            public Task SaveBirthday(Birthday birthday) {
                throw new System.NotImplementedException();
            }
        }
    }
}
```

### Key takeaway

Use dependency inject to control your dependencies. ViewModel contains logic for the view and should be tested thusly.

## Adding an integrated test

Services should be tested using an integrated test, using the real implementation. These tests will be slower and more vulnerable, but are invaluable to ensure correctness and to detect breaking changes. 

Update `BirthdayService` to take the location as a parameter. We'll use this to create a list just for the tests. *bodø* is still default.

```csharp
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Birthdays.Models;
using Newtonsoft.Json;

namespace Birthdays.Services {
    public class BirthdayService : IBirthdayService {
        const string BaseAddress = "http://178.62.18.75:8081";
        const string BirthdayAPI = "api/birthday";

        readonly string location;

        public BirthdayService(string location = "bodø") {
            this.location = location;
        }

        public async Task<Person[]> GetBirthdays() {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                const int MaxResults = 10;
                var json = await httpClient.GetStringAsync($"{BirthdayAPI}/{location}/{MaxResults}");
                var birthdays = JsonConvert.DeserializeObject<Birthdays>(json);
                var persons = birthdays.TodaysBirthdays.Concat(birthdays.NextBirthdays).ToArray();
                return persons;
            }
        }

        public async Task SaveBirthday(Birthday birthday) {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                var personWithLocation = new BirthdayWithLocation(birthday, location);
                var json = JsonConvert.SerializeObject(personWithLocation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(BirthdayAPI, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteBirthday(Person person) {
            using (var httpClient = HttpClientFactory.CreateClient()) {
                await httpClient.DeleteAsync($"{BirthdayAPI}/{person.Id}");
            }
        }

        class Birthdays {
            public Person[] TodaysBirthdays { get; set; }
            public Person[] NextBirthdays { get; set; }
        }

        class BirthdayWithLocation {
            readonly Birthday birthday;
            readonly string location;

            public BirthdayWithLocation(Birthday birthday, string location) {
                this.birthday = birthday;
                this.location = location;
            }

            public string Name { get { return birthday.Name; } }
            public string Date { get { return birthday.Birthdate.ToShortDateString(); } }
            public string Location { get { return location; } }
        }

        static class HttpClientFactory {
            public static HttpClient CreateClient()
                => new HttpClient {
                    BaseAddress = new Uri(BaseAddress)
                };
        }
    }
}
```

Create `BirthdayServiceTests`:

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using Birthdays.Models;
using Birthdays.Services;
using Xunit;

namespace Tests {
    public class BirthdayServiceTests {
        [Fact]
        public async Task VerifyBirthdayService() {
            var uniqueLocation = Guid.NewGuid().ToString("N");
            var birthdayService = new BirthdayService(uniqueLocation);

            var birthdate = DateTime.Today + TimeSpan.FromDays(1);
            var uniqueName = Guid.NewGuid().ToString("N");
            var birthday = new Birthday(uniqueName, birthdate);

            await birthdayService.SaveBirthday(birthday);

            var persons = await birthdayService.GetBirthdays();

            Assert.True(persons.Any());
            Assert.Equal(birthday.Name, persons[0].Name);

            await birthdayService.DeleteBirthday(persons[0]);

            persons = await birthdayService.GetBirthdays();

            Assert.False(persons.Any());
        }
    }
}
```

### Key takeaway

Use integrated tests where necessary. Do not repeat logic from unit tests. Remember to clean up after each test.
