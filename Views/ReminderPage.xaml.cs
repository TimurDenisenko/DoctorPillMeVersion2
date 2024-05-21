using DoctorPillMe.ViewModels;
using DoctorPillMe.Models;

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

    private void SaveReminder(object sender, EventArgs e)
    {
        Reminder Reminder = (Reminder)BindingContext;
        if (new string[] { Reminder.User, Reminder.Pill, Reminder.Count == 0 ? "" : "fill" }.All(x => !string.IsNullOrEmpty(x)))
            App.Database.SaveReminder(Reminder);
        Navigation.PopAsync();
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