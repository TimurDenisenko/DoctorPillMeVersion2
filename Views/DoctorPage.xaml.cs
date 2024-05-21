using DoctorPillMe.Models;
using DoctorPillMe.ViewModels;

namespace DoctorPillMe.Views;

public partial class DoctorPage : ContentPage
{
    public UserViewModel ViewModel { get; private set; }
    public DoctorPage()
    {
        InitializeComponent();
    }
    public DoctorPage(UserViewModel pm)
    {
        InitializeComponent();
        ViewModel = pm;
        BindingContext = ViewModel;
        
    }
    [Obsolete]
    private void SaveUser(object sender, EventArgs e)
    {
        User User = (User)BindingContext;
        if (new string[] { User.Name, User.HashPassword }.All(x => !string.IsNullOrEmpty(x)))
        {
            Tuple<string, byte[]> password = PasswordSecurity.HashPassword(User.HashPassword);
            User.HashPassword = password.Item1;
            User.Salt = password.Item2;
            User.Role = "Doctor";
            App.Database.SaveUser(User);
        }
        Navigation.PopAsync();
    }

    private void DeleteUser(object sender, EventArgs e)
    {
        User User = (User)BindingContext;
        App.Database.DeletePill(User.Id);
        Navigation.PopAsync();
    }

    private void Cancel(object sender, EventArgs e) =>
        Navigation.PopAsync();
}