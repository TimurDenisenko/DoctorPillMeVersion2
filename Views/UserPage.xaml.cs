using DoctorPillMe.Models;
using DoctorPillMe.ViewModels;

namespace DoctorPillMe.Views;

public partial class UserPage : ContentPage
{
    public UserViewModel? ViewModel { get; private set; }
    public UserPage()
    {
        InitializeComponent();
    }
    public UserPage(UserViewModel pm)
    {
        InitializeComponent();
        ViewModel = pm;
        BindingContext = ViewModel;
    }
    [Obsolete]
    private async void SaveUser(object sender, EventArgs e)
    {
        try
        {
            User User = (User)BindingContext;
            if (new string[] { User.Name, User.HashPassword }.All(x => !string.IsNullOrEmpty(x)))
            {
                Tuple<string, byte[]> password = PasswordSecurity.HashPassword(User.HashPassword);
                User.HashPassword = password.Item1;
                User.Salt = password.Item2;
                User.Role = "User";
                App.Database.SaveUser(User);
            }
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Viga","Midagi on vale","Tühista");
        }
    }

    private async void DeleteUser(object sender, EventArgs e)
    {
        User User = (User)BindingContext;
        App.Database.DeleteUser(User.Id);
        await Navigation.PopAsync();
    }

    private async void Cancel(object sender, EventArgs e) =>
        await Navigation.PopAsync();
}