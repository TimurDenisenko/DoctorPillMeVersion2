
using DoctorPillMe.Models;
using System.ComponentModel;

namespace DoctorPillMe.ViewModels
{
    public class ReminderViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        RemindersListViewModel rlvm;
        public Reminder Reminder { get; set; }
        public ReminderViewModel()
        {
            Reminder = new Reminder();
        }
        public RemindersListViewModel RemindersListViewModel
        {
            get { return rlvm; }
            set
            {
                if (rlvm == value) return;
                rlvm = value;
                OnPropertyChanged("RemindersListViewModel");
            }
        }
        public string User
        {
            get { return Reminder.User; }
            set
            {
                if (Reminder.User == value) return;
                Reminder.User = value;
                OnPropertyChanged("User");
            }
        }
        public string Pill
        {
            get { return Reminder.Pill; }
            set
            {
                if (Reminder.Pill == value) return;
                Reminder.Pill = value;
                OnPropertyChanged("Pill");
            }
        }
        public int Count
        {
            get { return Reminder.Count; }
            set
            {
                if (Reminder.Count == value) return;
                Reminder.Count = value;
                OnPropertyChanged("Count");
            }
        }
        public TimeSpan Time
        {
            get { return Reminder.Time; }
            set
            {
                if (Reminder.Time == value) return;
                Reminder.Time = value;
                OnPropertyChanged("Time");
            }
        }
        public string Day
        {
            get { return Reminder.Day; }
            set
            {
                if (Reminder.Day == value) return;
                Reminder.Day = value;
                OnPropertyChanged("Day");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { User, Pill, Count == 0 ? "" : "fill"}.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
