
using DoctorPillMe.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DoctorPillMe.ViewModels
{
    public class RemindersListViewModel
    {
        public ObservableCollection<ReminderViewModel> Reminders { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateReminderCommand { get; protected set; }
        public ICommand DeleteReminderCommand { get; protected set; }
        public ICommand SaveReminderCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        ReminderViewModel selectedReminder;
        public INavigation Navigation { get; set; }
        public RemindersListViewModel()
        {
            Reminders = new ObservableCollection<ReminderViewModel>();
            CreateReminderCommand = new Command(CreateReminder);
            DeleteReminderCommand = new Command(DeleteReminder);
            SaveReminderCommand = new Command(SaveReminder);
            BackCommand = new Command(Back);
        }
        public ReminderViewModel SelectedReminder
        {
            get { return selectedReminder; }
            set
            {
                if (selectedReminder == value) return;
                ReminderViewModel temp = value;
                selectedReminder = null;
                OnPropertyChanged("SelectedReminder");
                Navigation.PushAsync(new ReminderPage(temp));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void CreateReminder() => Navigation.PushAsync(new ReminderPage(new ReminderViewModel() { RemindersListViewModel = this }));
        private void Back() => Navigation.PopAsync();
        private void SaveReminder(object ReminderObj)
        {
            if (ReminderObj is not ReminderViewModel Reminder || Reminder == null || !Reminder.IsValid || Reminders.Contains(Reminder)) return;
            Reminders.Add(Reminder);
            Back();
        }
        private void DeleteReminder(object ReminderObj)
        {
            if (ReminderObj is not ReminderViewModel Reminder || Reminder == null) return;
            Reminders.Remove(Reminder);
            Back();
        }
    }
}
