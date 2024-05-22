using DoctorPillMe.Models;
using DoctorPillMe.ViewModels;

namespace DoctorPillMe.Views;

public partial class PillPage : ContentPage
{
    public PillViewModel ViewModel { get; private set; }
    public PillPage()
    {
        InitializeComponent();
        Units();
    }
    public PillPage(PillViewModel pm)
    {
        InitializeComponent();
        Units();
        ViewModel = pm;
        BindingContext = ViewModel;
    }
    private void Units()
    {
        listUnits.ItemsSource = new string[] { "Tabletid", "Kapslid", "Ampullid", "Viaalid", "Rektaalsuposiidid", "Salvid", "Geelid", "Aerosoolid", "Pihustid", "Süstelahused", "Infusioonilahused" };
        listUnits.ItemSelected += ListUnits_ItemSelected;
    }

    private void ListUnits_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        entryUnit.Text = e.SelectedItem.ToString();
    }

    private void SavePill(object sender, EventArgs e)
    {
        Pill Pill = (Pill)BindingContext;
        if (new string[] { Pill.Name, Pill.Description, Pill.Amount == 0 ? "" : "fill", Pill.Unit, Pill.Price == 0 ? "" : "fill" }.All(x => !string.IsNullOrEmpty(x)))
            App.Database.SavePill(Pill);
        Navigation.PopAsync();
    }

    private void DeletePill(object sender, EventArgs e)
    {
        Pill Pill = (Pill)BindingContext;
        App.Database.DeletePill(Pill.Id);
        Navigation.PopAsync();
    }

    private void Cancel(object sender, EventArgs e) =>
        Navigation.PopAsync();
}