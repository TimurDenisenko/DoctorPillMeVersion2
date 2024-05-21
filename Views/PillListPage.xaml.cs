using DoctorPillMe.Models;

namespace DoctorPillMe.Views;

public partial class PillListPage : ContentPage
{
    public PillListPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        PillsList.ItemsSource = App.Database.GetPills();
        base.OnAppearing();
    }
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Pill selectedPill = e.SelectedItem as Pill;
        PillPage PillPage = new PillPage();
        PillPage.BindingContext = selectedPill;
        await Navigation.PushAsync(PillPage);
    }
    private async void CreatePill(object sender, EventArgs e)
    {
        Pill pill = new Pill();
        PillPage pillPage = new PillPage();
        pillPage.BindingContext = pill;
        await Navigation.PushAsync(pillPage);
    }
}