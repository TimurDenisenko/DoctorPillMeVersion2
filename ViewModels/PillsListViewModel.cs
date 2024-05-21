using DoctorPillMe.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DoctorPillMe.ViewModels
{
    public class PillsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PillViewModel> Pills { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreatePillCommand { get; protected set; }
        public ICommand DeletePillCommand { get; protected set; }
        public ICommand SavePillCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        PillViewModel selectedPill;
        public INavigation Navigation { get; set; }
        public PillsListViewModel()
        {
            Pills = new ObservableCollection<PillViewModel>();
            CreatePillCommand = new Command(CreatePill);
            DeletePillCommand = new Command(DeletePill);
            SavePillCommand = new Command(SavePill);
            BackCommand = new Command(Back);
        }
        public PillViewModel SelectedPill
        {
            get { return selectedPill; }
            set
            {
                if (selectedPill == value) return;
                PillViewModel temp = value;
                selectedPill = null;
                OnPropertyChanged("SelectedPill");
                Navigation.PushAsync(new PillPage(temp));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void CreatePill() => Navigation.PushAsync(new PillPage(new PillViewModel() { PillsListViewModel = this }));
        private void Back() => Navigation.PopAsync();
        private void SavePill(object PillObj)
        {
            if (PillObj is not PillViewModel Pill || Pill == null || !Pill.IsValid || Pills.Contains(Pill)) return;
            Pills.Add(Pill);
            Back();
        }
        private void DeletePill(object PillObj)
        {
            if (PillObj is not PillViewModel Pill || Pill == null) return;
            Pills.Remove(Pill);
            Back();
        }

    }
}
