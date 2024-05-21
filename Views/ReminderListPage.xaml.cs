using DoctorPillMe.Models;

namespace DoctorPillMe.Views;

public partial class ReminderListPage : ContentPage
{
    public ReminderListPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        RemindersList.ItemsSource = App.Database.GetReminders();
        base.OnAppearing();
    }
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Reminder selectedReminder = e.SelectedItem as Reminder;
        ReminderPage ReminderPage = new ReminderPage();
        ReminderPage.BindingContext = selectedReminder;
        await Navigation.PushAsync(ReminderPage);
    }
    private async void CreateReminder(object sender, EventArgs e)
    {
        Reminder Reminder = new Reminder();
        ReminderPage ReminderPage = new ReminderPage();
        ReminderPage.BindingContext = Reminder;
        await Navigation.PushAsync(ReminderPage);
    }
}