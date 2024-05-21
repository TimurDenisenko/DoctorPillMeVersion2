using DoctorPillMe.Models;
using Plugin.LocalNotification;

namespace DoctorPillMe.Views;

public partial class MainPage : ContentPage
{
    Entry password, login;
    CheckBox passwordVisibleBox;
    Label passwordVisible, passwordLabel, loginLabel;
    Button sisse;
    User user;

    [Obsolete]
    public MainPage()
    {
        loginLabel = new Label { Text = "Kasutaja nimi"};
        passwordLabel = new Label{ Text = "Parool"};
        passwordVisible = new Label { Text = "Näita parool" };
        login = new Entry();
        password = new Entry { IsPassword = true };
        passwordVisibleBox = new CheckBox { };
        passwordVisibleBox.CheckedChanged += PasswordVisibleBox_CheckedChanged;
        sisse = new Button { Text = "Logi sisse" };
        sisse.Clicked += Sisse_Clicked;
        Content = new StackLayout
        {
            Children = { loginLabel, login, passwordLabel, password, new HorizontalStackLayout { Children = { passwordVisible, passwordVisibleBox } }, sisse }
        };
	}

    [Obsolete]
    private async void Sisse_Clicked(object sender, EventArgs e)
    {
        while(!await LocalNotificationCenter.Current.AreNotificationsEnabled())
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }
        NotificationRequest request = new NotificationRequest
        {
            Title = "Test Notification",
            Description = "This is a test notification",
            ReturningData = "Dummy data",
        };
        await LocalNotificationCenter.Current.Show(request);
    }

    private void PasswordVisibleBox_CheckedChanged(object sender, CheckedChangedEventArgs e) => password.IsPassword = !passwordVisibleBox.IsChecked;

    [Obsolete]
    private async void Pages()
	{
        StackLayout st = new StackLayout();
        List<Button> pages = new List<Button>();
        if (user.Role == "Doctor" || user.Role == "Admin")
        {
            Button users = new Button { Text = "Kasutajad" };
            users.Clicked += async (s, e) => await Navigation.PushAsync(new UserListPage());
            pages.Add(users);
            Button pills = new Button { Text = "Tabletid" };
            pills.Clicked += async (s, e) => await Navigation.PushAsync(new PillListPage());
            pages.Add(pills);
            Button Reminder = new Button { Text = "Teatis" };
            Reminder.Clicked += async (s, e) => await Navigation.PushAsync(new ReminderListPage());
            pages.Add(Reminder);
            if (user.Role == "Admin")
            {
                Button doctors = new Button { Text = "Arstid" };
                doctors.Clicked += async (s, e) => await Navigation.PushAsync(new DoctorListPage()); 
                pages.Add(doctors);
            }
        }
        else
        {
            await DisplayAlert("Hoiatus", "See rakendus pole mõeldud kasutajatele, vaid arstidele. Laadige alla DoctorPillMe.","Tühista");
            return;
        }
        foreach (Button item in pages)
        {
            item.Margin = 30;
            st.Children.Add(item);
        }
        Content = st;
    }
}