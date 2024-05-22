using DoctorPillMe.ViewModels;
using DoctorPillMe.Models;
using Plugin.LocalNotification;

namespace DoctorPillMe.Views;

public partial class ReminderPage : ContentPage
{
    public ReminderViewModel ViewModel { get; private set; }
    public ReminderPage()
    {
        InitializeComponent();
        ListLoad();
    }
    public ReminderPage(ReminderViewModel pm)
    {
        InitializeComponent();
        ListLoad();
        ViewModel = pm;
        BindingContext = ViewModel;
    }
    private void ListLoad()
    {
        weeksList.ItemsSource = new string[] { "Esmaspäev", "Teisipäev", "Kolmapäev", "Neljapäev", "Reede", "Laupäev", "Pühapäev" };
        weeksList.ItemSelected += (s, e) => weekEntry.Text = e.SelectedItem.ToString();
        usersList.ItemsSource = App.Database.GetUsers().Select(x => x.Name);
        usersList.ItemSelected += (s, e) => userEntry.Text = e.SelectedItem.ToString();
        pillsList.ItemsSource = App.Database.GetPills().Select(x => x.Name);
        pillsList.ItemSelected += (s, e) => pillEntry.Text = e.SelectedItem.ToString();
    }

    private async void SaveReminder(object sender, EventArgs e)
    {
        Reminder Reminder = (Reminder)BindingContext;
        if (new string[] { Reminder.User, Reminder.Pill, Reminder.Count == 0 ? "" : "fill" }.All(x => !string.IsNullOrEmpty(x)))
        {
            App.Database.SaveReminder(Reminder);

            NotificationRequest request = new NotificationRequest
            {
                NotificationId = Reminder.Id,
                Title = "DoctorPillMe",
                Subtitle = "Aeg võtta pill",
                Description = Reminder.Pill,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = new DateTime(GetNextWeekday(ChangeLanguage(Reminder.Day), DateTime.Now), new TimeOnly(Reminder.Time.Ticks)),
                    RepeatType = NotificationRepeat.Weekly
                },
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                {
                    Priority = Plugin.LocalNotification.AndroidOption.AndroidPriority.Max,
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }
        await Navigation.PopAsync();
    }
    private DayOfWeek ChangeLanguage(string week)
    {
        switch (week)
        {
            case "Esmaspäev":
                return DayOfWeek.Monday;
            case "Teisipäev":
                return DayOfWeek.Tuesday;
            case "Kolmapäev":
                return DayOfWeek.Wednesday;
            case "Neljapäev":
                return DayOfWeek.Thursday;
            case "Reede":
                return DayOfWeek.Friday;
            case "Laupäev":
                return DayOfWeek.Saturday;
            case "Pühapäev":
                return DayOfWeek.Sunday;
            default:
                return DayOfWeek.Sunday;
        }
    }
    private static DateOnly GetNextWeekday(DayOfWeek day, DateTime start)
    {
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        DateTime dt = start.AddDays(daysToAdd);
        return new DateOnly(dt.Year,dt.Month,dt.Day);
    }

    private void DeleteReminder(object sender, EventArgs e)
    {
        Reminder Reminder = (Reminder)BindingContext;
        App.Database.DeleteReminder(Reminder.Id);
        Navigation.PopAsync();
    }

    private void Cancel(object sender, EventArgs e) =>
        Navigation.PopAsync();
}